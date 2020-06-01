import { NgModule } from '@angular/core'

import { FlexLayoutModule } from '@angular/flex-layout'

import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input'

const ACCOUNT_MATERIAL_MODULES = [
    FlexLayoutModule,

    MatFormFieldModule,
    MatInputModule
]

@NgModule({
    imports: ACCOUNT_MATERIAL_MODULES,
    exports: ACCOUNT_MATERIAL_MODULES
})
export class AccountMaterialModule { }
