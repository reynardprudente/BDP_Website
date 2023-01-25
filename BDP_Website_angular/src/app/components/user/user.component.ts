import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/service/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import {MatDialog} from '@angular/material/dialog';
import { AddUserDialogComponent } from 'src/app/shared/dialog/add-user-dialog/add-user-dialog.component';
import { SnackbarService } from 'src/app/shared/service/snackbar.service';
import { User } from 'src/app/shared/model/user';
import { EditUserDialogComponent } from 'src/app/shared/dialog/edit-user-dialog/edit-user-dialog.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  displayedColumns: string[] = ['role', 'firstname', 'middleName', 'lastName', 'emailAddress','account', 'actions'];
  dataSource:any;
  name:string = "";
  tempDatasource:any


  constructor(private userService: UserService, 
    private spinner: NgxSpinnerService,
    private dialog: MatDialog,
    private snackBar: SnackbarService)
  {
    this.userService = userService;
    this.spinner = spinner;
    this.dialog = dialog;
    this.snackBar = snackBar;
  }

  async ngOnInit() { 
    this.getUsers()
  }

  async getUsers(){
    await this.userService.getUsers()
    .then((result)=>{
      this.dataSource = this.getAllUserFinal(result);
      this.tempDatasource = this.dataSource
    })
    .catch((err) =>{
      this.snackBar.openError(err.error, "close")
    });
   }

 addUser(){
    const dialogRef = this.dialog.open(AddUserDialogComponent, {
      data :  this.dataSource
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getUsers()
  })
}

  editUser(user:User){
    const dialogRef = this.dialog.open(EditUserDialogComponent, {
      data :  user
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getUsers()
    })
  }

  async deleteUser(user:User){
    await this.userService.deleteUser(user.emailAddress)
    .then((result)=>{
      this.snackBar.openError(String(result), "close")
      this.getUsers()
    })
    .catch((err) =>{
      this.snackBar.openError(err.error, "close")
    });
  }

  search(){
    this.refresh()
    const result = this.dataSource.filter((obj:any) => {
      return obj.firstName === this.name.trim() || obj.middleName === this.name.trim()
      || obj.lastName === this.name.trim();
    });
    if(this.name != ""){
      this.dataSource = result.length == 0 ? null : result
    }
  }

  refresh(){
    this.dataSource = this.tempDatasource
  }

  getAllUserFinal(usersFromApi:any){
    let users:User[] = []
     usersFromApi.forEach(function (value: any) {
         let user:User = {
           role : value.role,
           firstName: value.firstName,
           middleName: value.middleName,
           lastName: value.lastName,
           emailAddress : value.emailAddress,
           account: value.account === null ? false : value.account.accountNumber
         }
         users.push(user);
     });
     return users;
   }
}
