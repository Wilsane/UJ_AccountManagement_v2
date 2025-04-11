import { Component, inject, OnInit, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastService } from '../../Services/toast.service';

interface Customer {
  accountId: number;
  accountHolder: string;
}

@Component({
  selector: 'app-add-transactions',
  standalone: false,
  templateUrl: './add-transactions.component.html',
  styleUrl: './add-transactions.component.css'
})
export class AddTransactionsComponent implements OnInit {

  constructor(private toastService: ToastService) { }

  @Output() transactionAdded = new EventEmitter<void>();

  ngOnInit(): void {
    this.getAllCustomers();
  }
  httpClient = inject(HttpClient);
  customers: Customer[] = [];


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
  selectedAccountHolder: string = '';

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
        this.selectedAccountHolder = '';
        this.transactionAdded.emit(); // notify parent component
        this.toastService.showToast("success", "Transaction Added successfully", "Success");

      }
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
}

