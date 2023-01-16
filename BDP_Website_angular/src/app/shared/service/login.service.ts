import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private headers = new HttpHeaders().set("Content-Type","application/json")
  constructor(private http: HttpClient)
  {
    this.http = http
  }

  public login(emailaddress: string, password:string){

       return this.http
       .post<any>(
         `${environment.backendApiUrl}/api/login`,
         {emailaddress: emailaddress, password: password},
         {headers: this.headers}
       )
  }
}
