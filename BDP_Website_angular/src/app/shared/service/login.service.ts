import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient)
  {
    this.http = http
  }

  public login(emailaddress: string, password:string){

   let headers = new HttpHeaders().set("Content-Type","application/json")

       return this.http
       .post<any>(
         `${environment.backendApiUrl}/api/login`,
         {emailaddress: emailaddress, password: password},
         {headers: headers}
       )


  }
}
