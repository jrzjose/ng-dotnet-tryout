import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../models/transaction';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../services/transaction-service';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-transaction-list',
  imports: [CommonModule],
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css'
})
export class TransactionListComponent implements OnInit {
  transactions: Transaction[] = [];

  constructor(private transactionService: TransactionService, private router: Router, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    console.log("OnInit");
    this.loadTransactions();
  }

  loadTransactions(): void {
    console.log('loadTransactions');
    this.transactionService.getAll().subscribe({
      next: (data: Transaction[]) => {
        console.log(data.length);
        this.transactions = [... data];
        this.cdr.detectChanges(); 
        console.log(this.transactions);
      },
      error: (err) => {
        console.log('Error fetching students:', err);
      }
    });
  }

  getTotalIncome(): number {
    return this.transactions.filter(t => t.type === 'Income').reduce((sum, t) => sum + t.amount, 0);
  }

  getTotalExpenses(): number {
    return this.transactions.filter(t => t.type === 'Expense').reduce((sum, t) => sum + t.amount, 0);
  }

  getNetBalance(): number {
    return this.getTotalIncome() - this.getTotalExpenses();
  }

  editTransaction(transaction: Transaction) {
    if (transaction.id) {
      this.router.navigate(['/edit/', transaction.id])
    }
  }

  deleteTransaction(transaction: Transaction) {
    if (transaction.id) {
      if (confirm("Are you sure you want to delete this transaction?")) {
        this.transactionService.delete(transaction.id).subscribe({
          next: () => {
            this.loadTransactions();
          },
          error: (error) => {
            console.log('Error - ', error);
          }
        })
      }
    }
  }

}