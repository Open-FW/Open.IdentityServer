import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'

import { AccountServiceModule } from './../account-service.module'
import { Login } from '../models/login'
import { Observable } from 'rxjs'

@Injectable({ providedIn: AccountServiceModule })
export class AccountService {
    constructor(private readonly http: HttpClient) { }

    login(login: Login): Observable<string> {
        return this.http.post('auth/account/login', login, { responseType: 'text' })
    }

    logout(logoutId: string): Observable<string> {
        return this.http.get(`auth/account/logout?logoutId=${logoutId}`, { responseType: 'text' })
    }
}
