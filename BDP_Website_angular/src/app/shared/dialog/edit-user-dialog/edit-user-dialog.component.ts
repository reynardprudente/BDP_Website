import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { SnackbarService } from '../../service/snackbar.service';
import { UserService } from '../../service/user.service';

@Component({
  selector: 'app-edit-user-dialog',
  templateUrl: './edit-user-dialog.component.html',
  styleUrls: ['./edit-user-dialog.component.scss']
})
export class EditUserDialogComponent implements OnInit {

  editUserForm: FormGroup
  constructor( private formbuilder:FormBuilder,
    private snackBar: SnackbarService,
    private spinner: NgxSpinnerService,
    private userService:UserService,
    public dialogRef: MatDialogRef<EditUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) 
  { 
    this.formbuilder = formbuilder
    this.snackBar = snackBar
    this.spinner = spinner
    this.userService = userService
    this.dialogRef = dialogRef
  }

  ngOnInit(): void {
    this.editUserForm = this.formbuilder.group({
      firstName:[this.data.firstName, [Validators.required]],
      middleName:[this.data.middleName, [Validators.required]],
      lastName:[this.data.lastName, [Validators.required]],
      emailAddress:[this.data.emailAddress],
      role:[this.data.role],
   })
  }

  editUser(){
    this.spinner.show();
   if(this.editUserForm.value.firstName === this.data.firstName && 
    this.editUserForm.value.middleName === this.data.middleName &&
    this.editUserForm.value.lastName === this.data.lastName)
    {
      this.snackBar.openError("No changes commited","close")
    }
    else{
      this.editUserForm.value.role = this.editUserForm.value.role == "Admin"? 1 : 2;
     const user = this.userService.editUser(this.editUserForm.value)
     user.then((value) => {
      this.snackBar.openSuccess(String(value), "close")
      this.dialogRef.close();
    })
    .catch((err) =>{
      this.snackBar.openError(err.error, "close")
    })
    }
    this.spinner.hide();
  }
  
}
