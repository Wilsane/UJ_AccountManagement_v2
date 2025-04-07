import { Component, inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ColDef } from 'ag-grid-community';

import {
  ModuleRegistry,
  themeAlpine,
  themeBalham} from "ag-grid-community";
import { AllCommunityModule } from "ag-grid-community";

ModuleRegistry.registerModules([AllCommunityModule]);

interface IRow {
  transactionId: number;
  accountHolder: string;
  transactionType: string;
  amount: number;
  transactionDate: Date;
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
    { field: "amount"},
    { field: "transactionDate" },
  ];
  defaultColDef: ColDef = {
    flex: 1,
    filter: true,
  };


  getTransactions() {
    this.httpClient.get('https://localhost:44373/api/Transactions').subscribe((res: any) => {
      this.TransactionData = res;
      console.log("Transactions:", this.TransactionData);
    })
  }

  ngOnInit(): void {

    
    let apiUrl = 'https://localhost:44373/api/Transactions';

    this.getTransactions();

  }


  // Add Transaction
  AddTransactionData = {
    accountId: null,
    transactionType: '',
    amount: null,
  }
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



}
