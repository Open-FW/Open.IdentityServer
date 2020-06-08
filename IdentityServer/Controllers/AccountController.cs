using System.Security.Claims;
using System.Threading.Tasks;

using IdentityServer.Domain.Identity;
using IdentityServer.Domain.Modules.LdapModule;
using IdentityServer.Domain.Modules.ProviderModule;
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
        private readonly LdapService ldap;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IIdentityServerInteractionService interactionService,
            LdapService ldap)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.interactionService = interactionService;
            this.ldap = ldap;
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

                if (context == null)
                {
                    return BadRequest();
                }

                if (model.Ldap)
                {
                    var ldapUser = ldap.ValidateUser(model.UserName, model.Password);
                    if (ldapUser != null)
                    {
                        var result = await signInManager.ExternalLoginSignInAsync(nameof(Provider.LDAP), ldapUser.Id.ToString(), isPersistent: model.RememberMe);
                        if (!result.Succeeded)
                        {
                            var user = await this.userManager.FindByNameAsync(model.UserName);
                            if (user == null)
                            {
                                user = new AppUser(model.UserName) { Email = ldapUser.Email, EmailConfirmed = true };
                                var createResult = await this.userManager.CreateAsync(user);
                                if (!createResult.Succeeded)
                                {
                                    return BadRequest(createResult.Errors);
                                }
                            }
                            await this.userManager.AddLoginAsync(user, new UserLoginInfo(nameof(Provider.LDAP), ldapUser.Id.ToString(), Provider.LDAP));
                            await this.signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                        }

                        return Ok(model.ReturnUrl);
                    }
                }
                else
                {
                    var user = await this.userManager.FindByNameAsync(model.UserName);
                    if (user != null && await this.userManager.CheckPasswordAsync(user, model.Password))
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                        return Ok(model.ReturnUrl);
                    }
                }


                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [Route("external-login")]
        public async Task<IActionResult> ExternalLogin(string json)
        {
            var externalProvider = JsonConvert.DeserializeObject<ExternalProviderModel>(json);

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

                    var createResult = await this.userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                    {
                        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                        await this.signInManager.SignOutAsync();

                        return BadRequest(createResult);
                    }
                }


                await this.userManager.AddLoginAsync(user, info);
                await this.signInManager.SignInAsync(user, isPersistent: false);
                await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
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
