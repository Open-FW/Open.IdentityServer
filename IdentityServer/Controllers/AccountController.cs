using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using IdentityServer.Domain.Identity;
using IdentityServer.Model;
using IdentityServer.Models;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IIdentityServerInteractionService interactionService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IIdentityServerInteractionService interactionService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.interactionService = interactionService;
        }

        [HttpGet]
        [Route("{action}")]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpPost]
        [Route("{action}")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var context = await this.interactionService.GetAuthorizationContextAsync(model.ReturnUrl);

                var user = await this.userManager.FindByNameAsync(model.UserName);
                if (user != null && await this.userManager.CheckPasswordAsync(user, model.Password))
                {
                    AuthenticationProperties? properties = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberMe)
                    {
                        properties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.Now.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    }

                    await this.signInManager.SignInAsync(user, properties);

                    if (context != null)
                    {
                        return Ok(model.ReturnUrl);
                    }
                    else
                    {
                        throw new Exception("invalid return URL");
                    }
                }

                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [Route("external-login")]
        public async Task<IActionResult> ExternalLogin(string json)
        {
            var externalProvider = JsonConvert.DeserializeObject<ExternalProvider>(json);

            var context = await this.interactionService.GetAuthorizationContextAsync(externalProvider.ReturnUrl);
            if (context == null)
            {
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                await this.signInManager.SignOutAsync();

                return BadRequest();
            }

            var prop = this.signInManager.ConfigureExternalAuthenticationProperties(externalProvider.Provider, Url.Action(nameof(HandleExternalLogin), new { externalProvider.ReturnUrl }));

            return Challenge(prop, externalProvider.Provider);
        }

        [Route("{action}")]
        public async Task<IActionResult> HandleExternalLogin(string returnUrl)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var context = await this.interactionService.GetAuthorizationContextAsync(returnUrl);
            if (context == null)
            {
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                await this.signInManager.SignOutAsync();

                return BadRequest();
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (!result.Succeeded)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var user = await this.userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };

                    var createResult = await userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                        throw new Exception(createResult.Errors.Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));
                }


                await userManager.AddLoginAsync(user, info);
                await signInManager.SignInAsync(user, isPersistent: false);
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }

            return Redirect(returnUrl);
        }

        [HttpGet]
        [Authorize]
        [Route("{action}")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var context = await this.interactionService.GetLogoutContextAsync(logoutId);
            if (context != null)
            {
                await this.signInManager.SignOutAsync();

                return Ok(context.PostLogoutRedirectUri);
            }

            return BadRequest();
        }
    }
}
