import { Component, OnInit } from '@angular/core'
import { Router } from '@angular/router'
import { AuthService } from 'src/app/service/auth.service'

@Component({
    selector: 'is-auth-signout-callback',
    templateUrl: './signout-callback.component.html'
})
export class SignoutCallbackComponent implements OnInit {
    constructor(
        private readonly router: Router,
        private readonly authService: AuthService
    ) { }

    async ngOnInit(): Promise<void> {
        await this.authService.signoutComplete()
        // ToDo: Redirect to default page
    }
}
