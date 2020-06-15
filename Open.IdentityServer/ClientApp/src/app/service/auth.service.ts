import { Injectable } from '@angular/core'

import { User, UserManager } from 'oidc-client'

import { BehaviorSubject } from 'rxjs'
import { environment } from 'src/environments/environment'

@Injectable({ providedIn: 'root' })
export class AuthService {
    private authStateSource = new BehaviorSubject<boolean>(false)
    public authState = this.authStateSource.asObservable()

    private manager = new UserManager({
        authority: environment.uri,
        client_id: environment.client_id,
        redirect_uri: `${environment.uri}/auth/signin-callback`,
        post_logout_redirect_uri: `${environment.uri}/auth/signout-callback`,
        response_type: 'code',
        scope: 'openid profile email is.admin',
        filterProtocolClaims: true,
        loadUserInfo: true
    })

    private user: User | null

    constructor() {
        this.manager.getUser().then(user => {
            this.user = user
            this.authStateSource.next(this.isAuthenticated)
        })
    }

    public signin(redirectUri: string): Promise<void> {
        return this.manager.signinRedirect({
            data: {
                redirect_url: redirectUri
            }
        })
    }

    public signout(): Promise<void> {
        return this.manager.signoutRedirect()
    }

    async signinComplete(): Promise<void> {
        this.user = await this.manager.signinRedirectCallback()
        this.authStateSource.next(this.isAuthenticated)
    }

    async signoutComplete(): Promise<void> {
        await this.manager.signoutRedirectCallback()
        await this.manager.clearStaleState()

        this.authStateSource.next(this.isAuthenticated)
    }

    get state(): any {
        return this.user?.state
    }

    get isAuthenticated(): boolean {
        return this.user != null && !this.user.expired
    }

    get authorizationHeaderValue(): string {
        return this.user ? `${this.user.token_type} ${this.user.access_token}` : ''
    }

    get name(): string {
        return this.user?.profile?.name ?? ''
    }
}
