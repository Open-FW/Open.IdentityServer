import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HttpClientModule } from '@angular/common/http'
import { ReactiveFormsModule } from '@angular/forms'

import { AccountMaterialModule } from './account-material.module'
import { AccountServiceModule } from './account-service.module'
import { AccountRoutingModule } from './account.routing'

import { AccountDefaultLayout } from './layouts/account-default/account-default.layout'
import { LoginComponent } from './components/login/login.component'
import { LogoutComponent } from './components/logout/logout.components'

@NgModule({
    declarations: [
        AccountDefaultLayout,

        LoginComponent,
        LogoutComponent
    ],
    imports: [
        CommonModule,

        HttpClientModule,
        ReactiveFormsModule,

        AccountRoutingModule,
        AccountServiceModule,
        AccountMaterialModule
    ]
})
export class AccountModule { }
