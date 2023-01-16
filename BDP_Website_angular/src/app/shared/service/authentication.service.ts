import { Injectable } from '@angular/core';
import  jwt_decode from "jwt-decode";
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor() { }

  getUser(): any {
    let user = jwt_decode(localStorage.getItem("token"));
    return user;
  }

  isLoggeIn(){
    let token = localStorage.getItem('token')
    if( token != undefined){
      let dateToken = this.getTokenExpirationDate(token);
      let dateNow = new Date();
      if(dateToken === undefined && dateNow > dateNow){
        return false;
      }
      else{
        return true;
      }
    }

    return false;
  }

  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode<any>(token);

    if (decoded.exp === undefined) return null;

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }
}

