import { Component, inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ColDef } from 'ag-grid-community';

import {
  ModuleRegistry,
  themeAlpine,
  themeBalham} from "ag-grid-community";
import { AllCommunityModule } from "ag-grid-community";
import { ActionButtonsComponent } from '../action-buttons/action-buttons.component';

ModuleRegistry.registerModules([AllCommunityModule]);

interface IRow {
  transactionId: number;
  accountHolder: string;
  transactionType: string;
  amount: number;
  transactionDate: Date;
}

interface Customer {
  accountId: number;
  accountHolder: string;
}




@Component({
  selector: 'app-transactions',
  standalone: false,
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements OnInit {
  // Filter options
  selectedFilter: string = 'all'; // Default selected filter
    customers: Customer[]=[];
  onFilterChange() {
    console.log("Selected Filter:", this.selectedFilter);
  }


 

  httpClient = inject(HttpClient);

  //Get Transactions
  TransactionData: IRow[] = [];

  colDefs: ColDef<IRow>[] = [
    { field: "transactionId" },
    { field: "accountHolder" },
    { field: "transactionType" },
    { field: "amount" },
    { field: "transactionDate" },
    {
      colId: "actions",
      headerName: "Actions",
      cellStyle: { 'margin': '0 auto', 'padding-bottom': '2px' },
      cellRenderer: ActionButtonsComponent,
      filter: false,
      sortable: false,
      cellRendererParams: {
        onClick: (params: any) => this.onEditClick(params),
        onDelete: (params: any) => this.onDeleteClick(params)
      }
    } 
  ]; 

  defaultColDef: ColDef = {
    flex: 1,
    filter: true,
  };

  onEditClick(params: any): void {
    alert(`Mission Launched for ID: ${params.data.transactionId}`);
  }
  onDeleteClick(params: any): void {
    alert(`Deleting transaction with ID: ${params.data.transactionId}`);
  }

  getTransactions() {
    this.httpClient.get('https://localhost:44373/api/Transactions').subscribe((res: any) => {
      this.TransactionData = res;
      console.log("Transactions:", this.TransactionData);
    })
  }


  ngOnInit(): void {

    
    let apiUrl = 'https://localhost:44373/api/Transactions';

    this.getTransactions();
    this.getAllCustomers();
  }


  // Add Transaction
  AddTransactionData: {
    accountId: number | null;
    transactionType: string;
    amount: number | null;
  } = {
      accountId: null,
      transactionType: '',
      amount: null
    };

   onAddTransactionSubmit(): void {
   
     let httpOptions = {
       headers: new HttpHeaders({
         Authorization: 'my-auth-token',
         'Content-Type': 'application/json'
       })
     };

     let apiUrl = 'https://localhost:44373/api/Transactions';

     this.httpClient.post(apiUrl, this.AddTransactionData, httpOptions).subscribe({
       next: (response) => {
         console.log("Response:", response);
       },
       error: (error) => {
         console.error("Error:", error);
       },
       complete: () => {
         alert("Transaction Added successfully");
         console.log("Transaction Added successfully:\n" + JSON.stringify(this.AddTransactionData));
         this.AddTransactionData = {
           accountId: null,
           transactionType: '',
           amount: null,
         }
         this.getTransactions();
       }
     });
  }


  getAllCustomers() {
    this.httpClient.get<Customer[]>('https://localhost:44373/api/Transactions/Customers')
      .subscribe({
        next: (res) => {
          this.customers = res;
          console.log("Fetched Customers:", this.customers);
        },
        error: (err) => console.error("Error fetching customers:", err)
      });
  }

  onAccountHolderInput(event: Event): void {
    const input = (event.target as HTMLInputElement).value;
    const selected = this.customers.find(c => c.accountHolder === input);
    if (selected) {
      this.AddTransactionData.accountId = selected.accountId;
    }
    this.onAccountHolderChange(event);
  }
  onAccountHolderChange(event: any): void {
    const inputValue = event.target.value.trim();

    if (!inputValue) {
      this.AddTransactionData.accountId = null;
    }
  }


}
