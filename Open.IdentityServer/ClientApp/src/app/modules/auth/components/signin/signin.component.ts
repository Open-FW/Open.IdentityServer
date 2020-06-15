import { Component, OnInit } from '@angular/core'
import { AuthService } from 'src/app/service/auth.service'
import { ActivatedRoute } from '@angular/router'

@Component({
    selector: 'is-auth-signin',
    templateUrl: './signin.component.html'
})
export class SigninComponent implements OnInit {
    constructor(
        private readonly authService: AuthService,
        private readonly activeRoute: ActivatedRoute) { }

    async ngOnInit(): Promise<void> {
        await this.authService.signin(this.activeRoute.snapshot.queryParamMap.get('redirect_url'))
    }
}
