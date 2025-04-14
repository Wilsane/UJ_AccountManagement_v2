import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TransactionsComponent } from './Components/transactions/transactions.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AgGridModule } from 'ag-grid-angular';


import { ActionButtonsComponent } from './Components/action-buttons/action-buttons.component';
import { AddTransactionsComponent } from './Components/add-transactions/add-transactions.component';
import { DeleteTransactionComponent } from './Components/delete-transaction/delete-transaction.component';
import { AlertNotificationComponent } from './Components/notifications/alert-notification/alert-notification.component';
import { FilterComponent } from './Components/Accounts/filter/filter.component';
import { NewAccountComponent } from './Components/Accounts/new-account/new-account.component';
import { AccountsTableComponent } from './Components/Accounts/accounts-table/accounts-table.component';
import { AccountsMainComponent } from './Components/Accounts/accounts-main/accounts-main.component';




@NgModule({
  declarations: [
    AppComponent,
    TransactionsComponent,
    ActionButtonsComponent,
    AddTransactionsComponent,
    DeleteTransactionComponent,
    AlertNotificationComponent,
    FilterComponent,
    NewAccountComponent,
    AccountsTableComponent,
    AccountsMainComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    AgGridModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
