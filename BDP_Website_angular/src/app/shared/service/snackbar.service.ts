import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(
    private snackBar : MatSnackBar
  ) { 
    this.snackBar = snackBar
  }

  openSnackBar(message: string, action:string, panelClass:string){
    this.snackBar.open(message, action, {
      panelClass,
      duration: 2000,
      horizontalPosition : 'right',
      verticalPosition : 'top'
    })}

    openSuccess(message: string, action: string) {
      this.openSnackBar(message, action, 'success-message');
    }
  
    openError(message: string, action: string ) {
      this.openSnackBar(message, action, 'error-message');
  }
}

