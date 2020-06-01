import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HttpClientModule } from '@angular/common/http'
import { ReactiveFormsModule } from '@angular/forms'

import { AccountMaterialModule } from './account-material.module'
import { AccountServiceModule } from './account-service.module'
import { AccountRoutingModule } from './account.routing'

@NgModule({
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
