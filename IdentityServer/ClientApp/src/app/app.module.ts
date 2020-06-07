import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

import { AppRoutingModule } from './app.routing'
import { RootComponent } from './root.component'
import { GraphQLModule } from './graphql.module'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { AuthInterceptor } from './interceptors/auth.interceptor'
import { HeaderHttpInterceptor } from './interceptors/header.interceptor'

@NgModule({
    declarations: [
        RootComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,

        GraphQLModule,
        HttpClientModule,

        AppRoutingModule
    ],
    providers: [{
        provide: HTTP_INTERCEPTORS,
        useClass: HeaderHttpInterceptor,
        multi: true
    }, {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true
    }],
    bootstrap: [RootComponent]
})
export class AppModule { }
