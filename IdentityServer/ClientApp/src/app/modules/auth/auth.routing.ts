import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

import { AuthDefaultLayout } from '../layouts/auth-default/auth-default.layout'

import { SigninComponent } from './components/signin/signin.component'
import { SigninCallbackComponent } from './components/signin-callback/signin-callback.component'
import { SignoutCallbackComponent } from './components/signout-callback/signout-callback.component'
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component'

const AUTH_ROUTES: Routes = [
    {
        path: '', component: AuthDefaultLayout, children: [
            { path: 'signin', component: SigninComponent },
            { path: 'signin-callback', component: SigninCallbackComponent },
            { path: 'signout-callback', component: SignoutCallbackComponent },
            { path: 'unauthorized', component: UnauthorizedComponent }
        ]
    }
]

@NgModule({
    imports: [RouterModule.forChild(AUTH_ROUTES)],
    exports: [RouterModule]
})
export class AuthRoutingModule { }
