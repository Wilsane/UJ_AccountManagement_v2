import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransactionsComponent } from './Components/transactions/transactions.component';
import { AccountsMainComponent } from './Components/Accounts/accounts-main/accounts-main.component';

const routes: Routes = [
  { path: 'transactions', component: TransactionsComponent },
  { path: 'accounts', component: AccountsMainComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
