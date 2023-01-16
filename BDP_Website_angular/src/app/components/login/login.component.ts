import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from 'src/app/shared/service/login.service';
import {firstValueFrom} from 'rxjs';
import { SnackbarService } from 'src/app/shared/service/snackbar.service';
import { AuthenticationService } from 'src/app/shared/service/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
public loginForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private spinner: NgxSpinnerService,
    private loginService: LoginService,
    private snackBar: SnackbarService,
    private authentication: AuthenticationService
  ) {
    this.formBuilder = formBuilder;
    this.router = router
    this.spinner = spinner
    this.loginService = loginService
    this.snackBar = snackBar
    this.authentication = authentication
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ["", [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      password: ["", [Validators.required]],
    })
  }

  onSubmit(){
   this.spinner.show();
    firstValueFrom(this.loginService.login(
      this.loginForm.controls["username"].value,
       this.loginForm.controls["password"].value
     ))
     .then(
       (result) => {
         localStorage.setItem("token", result.token);
         let user = this.authentication.getUser()
         let role = user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
        if(role == 'Admin'){
         this.router.navigate(['/admin/user']);
        }
        else if(role == 'Customer'){
          alert('no customer page')
        }
         this.spinner.hide();
       }
     )
     .catch((exception:any) =>{
      console.log(exception.error);
      this.snackBar.openError(exception.error, 'close')
     })
     this.spinner.hide();
     }
  }


