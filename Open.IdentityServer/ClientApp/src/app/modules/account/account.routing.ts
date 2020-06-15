import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

import { AccountDefaultLayout } from './layouts/account-default/account-default.layout'

import { LogoutComponent } from './components/logout/logout.components'
import { LoginComponent } from './components/login/login.component'

const ACCOUNT_ROUTES: Routes = [
    {
        path: '', component: AccountDefaultLayout, children: [
            { path: 'login', component: LoginComponent },
            { path: 'logout', component: LogoutComponent }
        ]
    }
]

@NgModule({
    imports: [RouterModule.forChild(ACCOUNT_ROUTES)],
    exports: [RouterModule]
})
export class AccountRoutingModule { }
