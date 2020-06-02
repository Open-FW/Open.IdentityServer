import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'

import { AccountServiceModule } from './../account-service.module'
import { Login } from '../models/login'
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment'

@Injectable({ providedIn: AccountServiceModule })
export class AccountService {
    constructor(private readonly http: HttpClient) { }

    login(login: Login): Observable<string> {
        return this.http.post(`${environment.uri}/api/auth/account/login`, login, { responseType: 'text' })
    }

    logout(logoutId: string): Observable<string> {
        return this.http.get(`${environment.uri}/api/auth/account/logout?logoutId=${logoutId}`, { responseType: 'text' })
    }
}
