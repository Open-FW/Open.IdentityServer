import { Component, APP_ID } from '@angular/core'
import { AuthService } from 'src/app/service/auth.service'
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment'

@Component({
    selector: 'is-admin-default-layout',
    templateUrl: './admin-default.layout.html'
})
export class AdminDefaultLayout {
    constructor(
        private readonly http: HttpClient,
        private readonly authService: AuthService
    ) { }

    logout() {
        this.authService.signout()
    }

    getAuthData() {
        this.http.get(`${environment.uri}/api/value/authdata`, { responseType: 'text' }).subscribe(s => console.log(s))
    }

    getAdminData() {
        this.http.get(`${environment.uri}/api/value/admindata`, { responseType: 'text' }).subscribe(s => console.log(s))
    }

    getUserData() {
        this.http.get(`${environment.uri}/api/value/userdata`, { responseType: 'text' }).subscribe(s => console.log(s))
    }

    getCreate() {
        this.http.get(`${environment.uri}/api/auth/account/create`).subscribe(s => console.log(s))
    }
}
