import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

import { AppRoutingModule } from './app.routing'
import { RootComponent } from './root.component'

@NgModule({
    declarations: [
        RootComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,

        AppRoutingModule
    ],
    providers: [],
    bootstrap: [RootComponent]
})
export class AppModule { }
