import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

import { AdminDefaultLayout } from './layouts/admin-default/admin-default.layout'

import { AuthGuard } from 'src/app/guards/auth.guard'

const ADMIN_ROUTES: Routes = [
    { path: '', component: AdminDefaultLayout, canActivate: [AuthGuard] }
]

@NgModule({
    imports: [RouterModule.forChild(ADMIN_ROUTES)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
