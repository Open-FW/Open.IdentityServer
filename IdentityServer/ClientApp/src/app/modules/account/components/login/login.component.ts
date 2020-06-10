import { Component, OnInit } from '@angular/core'
import { FormGroup, FormBuilder, Validators } from '@angular/forms'
import { ActivatedRoute } from '@angular/router'

import { AccountService } from './../../services/account.service'

@Component({
    selector: 'is-login',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
    form: FormGroup

    constructor(
        private readonly fb: FormBuilder,
        private readonly service: AccountService,
        private readonly activeRoute: ActivatedRoute,
    ) { }

    ngOnInit() {
        this.form = this.fb.group({
            userName: ['', Validators.required],
            password: ['', Validators.required],
            rememberMe: [false],
            ldap: [false]
        })
    }

    login() {
        if (this.form.valid) {
            this.service.login({
                userName: this.form.value.userName,
                password: this.form.value.password,
                rememberMe: this.form.value.rememberMe,
                ldap: this.form.value.ldap,
                returnUrl: this.activeRoute.snapshot.queryParamMap.get('ReturnUrl')
            }).subscribe(s => window.location.href = s, (error) => console.log(error))
        }
    }

    external(provider: string) {
        window.location.href = this.service.loginExternal({ provider, returnUrl: this.activeRoute.snapshot.queryParamMap.get('ReturnUrl') })
    }
}
