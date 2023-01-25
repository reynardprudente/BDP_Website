import { Component, OnInit, Inject } from '@angular/core';
import { roles } from '../../model/roles';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { UserService } from '../../service/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SnackbarService } from '../../service/snackbar.service';
import { Router } from '@angular/router';
import { MatDialogRef, MatDialog, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { User } from '../../model/user';

@Component({
  selector: 'app-add-user-dialog',
  templateUrl: './add-user-dialog.component.html',
  styleUrls: ['./add-user-dialog.component.scss']
})
export class AddUserDialogComponent implements OnInit {

  roles:roles[] = [
    {id:1, roleValue:"Admin"},
    {id:2, roleValue:"Customer"}
  ];
  public addUserForm!: FormGroup;

  constructor(
    private formbuilder:FormBuilder,
    private userService:UserService,
    private spinner: NgxSpinnerService,
    private snackBar: SnackbarService,
    public dialogRef: MatDialogRef<AddUserDialogComponent>) 
  { 
    this.formbuilder = formbuilder
    this.userService = userService
    this.spinner = spinner
    this.snackBar = snackBar
    this.dialogRef = dialogRef
  }

  ngOnInit(): void {
    this.addUserForm = this.formbuilder.group({
      firstName:["", [Validators.required]],
      middleName:["", [Validators.required]],
      lastName:["", [Validators.required]],
      emailAddress:["", [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      password:["", [Validators.required]],
      role:["", [Validators.required]],
    })
  }

   addUser(){
    this.spinner.show();
    const user = this.userService.addUser(this.addUserForm.value)
    user.then((value) => {
      this.snackBar.openSuccess(String(value), "close")
      this.dialogRef.close();
    })
    .catch((err) =>{
      this.snackBar.openError(err.error, "close")
    })
    this.spinner.hide();
  }
}
