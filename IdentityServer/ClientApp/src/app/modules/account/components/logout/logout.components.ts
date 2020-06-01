import { Component, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router'

import { AccountService } from './../../services/account.service'

@Component({
    selector: 'is-logout',
    templateUrl: './logout.components.html'
})
export class LogoutComponent implements OnInit {
    constructor(
        private readonly service: AccountService,
        private readonly activeRoute: ActivatedRoute
    ) { }

    ngOnInit() {
        this.service.logout(this.activeRoute.snapshot.queryParamMap.get('logoutId')).subscribe(s => window.location.href = s)
    }
}
