import { NgModule } from '@angular/core'

import { FlexLayoutModule } from '@angular/flex-layout'

import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input'
import { MatCardModule } from '@angular/material/card'
import { MatButtonModule } from '@angular/material/button'
import { MatDividerModule } from '@angular/material/divider'
import { MatCheckboxModule } from '@angular/material/checkbox'
import { MatIconModule } from '@angular/material/icon'

const ACCOUNT_MATERIAL_MODULES = [
    FlexLayoutModule,

    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    MatCheckboxModule,
    MatDividerModule,
    MatIconModule,
]

@NgModule({
    imports: ACCOUNT_MATERIAL_MODULES,
    exports: ACCOUNT_MATERIAL_MODULES
})
export class AccountMaterialModule { }
