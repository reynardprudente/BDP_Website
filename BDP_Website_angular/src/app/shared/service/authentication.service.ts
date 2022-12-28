import { Injectable } from '@angular/core';
import * as jwt_decode from "jwt-decode";
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor() { }

  getUser(): any {
   // let user = jwt_decode(localStorage.getItem("token"));
   // return user;
  }
}

