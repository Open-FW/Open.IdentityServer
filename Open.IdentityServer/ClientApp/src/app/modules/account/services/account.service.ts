import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'

import { AccountServiceModule } from './../account-service.module'
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment'
import { Login } from '../models/login'
import { ExternalProvider } from '../models/external-provider'

@Injectable({ providedIn: AccountServiceModule })
export class AccountService {
    constructor(private readonly http: HttpClient) { }

    login(login: Login): Observable<string> {
        return this.http.post(`${environment.uri}/api/auth/account/login`, login, { responseType: 'text' })
    }

    logout(logoutId: string): Observable<string> {
        return this.http.get(`${environment.uri}/api/auth/account/logout?logoutId=${logoutId}`, { responseType: 'text' })
    }

    loginExternal(ep: ExternalProvider): string {
        const objStr = JSON.stringify(ep)
        const escapedObjStr = encodeURIComponent(objStr)
        return `${environment.uri}/api/auth/account/external-login?json=${escapedObjStr}`
    }
}
