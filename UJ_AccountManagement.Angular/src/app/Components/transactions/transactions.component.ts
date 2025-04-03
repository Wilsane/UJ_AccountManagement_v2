import { Component } from '@angular/core';


@Component({
  selector: 'app-transactions',
  standalone: false,
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent {
  selectedFilter: string = 'all'; // Default selected filter

  onFilterChange() {
    console.log("Selected Filter:", this.selectedFilter);
  }

}
