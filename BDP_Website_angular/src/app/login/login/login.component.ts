import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from 'src/app/shared/service/login.service';
import {firstValueFrom} from 'rxjs';

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
    private loginService: LoginService
  ) {
    this.formBuilder = formBuilder;
    this.router = router
    this.spinner = spinner
    this.loginService = loginService
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ["", [Validators.required]],
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
         this.router.navigate(['/admin/user']);
         this.spinner.hide();
       }
     )
     .catch(function aa(a:any){
      console.log(a)
      alert(a.error)
     })
     this.spinner.hide();
     }
  }


