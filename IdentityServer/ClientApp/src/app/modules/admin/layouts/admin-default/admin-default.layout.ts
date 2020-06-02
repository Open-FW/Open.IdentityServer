import { Component } from '@angular/core'
import { AuthService } from 'src/app/service/auth.service'

@Component({
    selector: 'is-admin-default-layout',
    templateUrl: './admin-default.layout.html'
})
export class AdminDefaultLayout {
    constructor(private readonly authService: AuthService) { }

    logout() {
        this.authService.signout()
    }
}
