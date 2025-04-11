import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ColDef } from 'ag-grid-community';

import {
  ModuleRegistry,
  themeAlpine,
  themeBalham,
  AllCommunityModule
} from 'ag-grid-community';

import { ActionButtonsComponent } from '../action-buttons/action-buttons.component';
import { ToastService } from '../../Services/toast.service';

// Register AG Grid modules
ModuleRegistry.registerModules([AllCommunityModule]);

// Define transaction interface
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

  constructor(private toastService: ToastService) { }
  // Dependency Injection
  httpClient = inject(HttpClient);

  // Transaction Data
  TransactionData: IRow[] = [];

  // Column definitions for AG Grid
  colDefs: ColDef<IRow>[] = [
    { field: 'transactionId' },
    { field: 'accountHolder' },
    { field: 'transactionType' },
    { field: 'amount' },
    { field: 'transactionDate' },
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
    }
  ];

  // Default column settings
  defaultColDef: ColDef = {
    flex: 1,
    filter: true
  };

  // Filter
  selectedFilter: string = 'all';

  onFilterChange(): void {
    console.log('Selected Filter:', this.selectedFilter);
  }

  // Handle edit action
  onEditClick(params: any): void {
    alert(`Mission Launched for ID: ${params.data.transactionId}`);
  }

  // Handle delete action
  onDeleteClick(params: any): void {
    const id = params.data.transactionId;

    if (confirm(`Are you sure you want to delete transaction ID ${id}?`)) {
      this.httpClient.delete(`https://localhost:44373/api/Transactions/${id}`)
        .subscribe({
          next: () => {
            alert(`Transaction ID ${id} deleted successfully.`);
            this.getTransactions(); // refresh table
            this.toastService.showToast('success','Transaction deleted successfully', 'Deleted');
            
          },
          error: (err) => {
            alert(`Error deleting transaction ID ${id}: ${err.message}`);
          }
        });
    }
  }


  // Get transaction data from API
  getTransactions(): void {
    this.httpClient.get('https://localhost:44373/api/Transactions').subscribe((res: any) => {
      this.TransactionData = res;
      console.log('Transactions:', this.TransactionData);
    });
  }

  // On component init
  ngOnInit(): void {
    this.getTransactions();
  }
}
