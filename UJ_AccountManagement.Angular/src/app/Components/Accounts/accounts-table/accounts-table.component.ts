import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ColDef } from 'ag-grid-community';

import {
  ModuleRegistry,
  themeAlpine,
  themeBalham,
  AllCommunityModule
} from 'ag-grid-community';
import { ActionButtonsComponent } from '../../action-buttons/action-buttons.component';


// Register AG Grid modules
ModuleRegistry.registerModules([AllCommunityModule]);

//Account interface
interface IAccount {
  accountId: number;
  accountHolder: string;
  phone1: string;
  phone2: string;
  email: string;
  accountLimit: number;
  balance: number;
  createdDate: Date;
}


@Component({
  selector: 'app-accounts-table',
  standalone: false,
  templateUrl: './accounts-table.component.html',
  styleUrl: './accounts-table.component.css'
})
export class AccountsTableComponent implements OnInit {

  // Dependency Injection
  httpClient = inject(HttpClient);

  // Account Data
  AccountData: IAccount[] = [];

  // Column definitions for AG Grid
  colDefs: ColDef<IAccount>[] = [
    { field: 'accountId', hide:true },
    { field: 'accountHolder' },
    { field: 'phone1' },
    { field: 'phone2' },
    { field: 'email' },
    { field: 'accountLimit' },
    { field: 'balance' },
    { field: 'createdDate' },
    {
      colId: 'actions',
      headerName: 'Actions',
      cellStyle: { 'margin': '0 auto', 'padding-bottom': '2px' },
      cellRenderer: ActionButtonsComponent,
      filter: false,
      sortable: false,
      cellRendererParams: {
        onClick: (params: any) => this.onEditClick(params),
        onDelete: (params: any) => this.onDeleteClick(params)
      }
    }];

  // Default column settings
  defaultColDef: ColDef = {
    flex: 1,
    filter: true
  };

  // Handle edit action
  onEditClick(params: any): void {
    alert(`Mission Launched for ID: ${params.data.accountId}`);
  }

  onDeleteClick(params: any): void {
    alert(`Delete Mission Launched for ID: ${params.data.accountId}`);
  }

  // Get transaction data from API
  getAllAccounts(): void {
    this.httpClient.get('https://localhost:44373/api/Account').subscribe((res: any) => {
      this.AccountData = res;
      console.log('Accounts:', this.AccountData);
    });
  }

    ngOnInit(): void {
      this.getAllAccounts();
    }

}
