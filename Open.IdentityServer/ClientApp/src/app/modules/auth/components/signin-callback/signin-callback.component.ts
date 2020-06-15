import { Component, OnInit } from '@angular/core'
import { Router } from '@angular/router'

import { AuthService } from 'src/app/service/auth.service'

@Component({
    selector: 'is-auth-signin-callback',
    templateUrl: './signin-callback.component.html'
})
export class SigninCallbackComponent implements OnInit {
    constructor(
        private readonly router: Router,
        private readonly authService: AuthService
    ) { }

    async ngOnInit(): Promise<void> {
        await this.authService.signinComplete()
        this.router.navigate([this.authService.state.redirect_url])
    }
}
