import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TransactionsComponent } from './Components/transactions/transactions.component';
import { FormsModule } from '@angular/forms';
import { AddTransactionsComponent } from './Componets/add-transactions/add-transactions.component';


@NgModule({
  declarations: [
    AppComponent,
    TransactionsComponent,
    AddTransactionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
