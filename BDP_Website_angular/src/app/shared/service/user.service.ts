import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { promises, resolve } from "dns";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private headers = new HttpHeaders()
                .set("Content-Type","application/json")
                .set("Authorization", 'Bearer ' + localStorage.getItem('token'));

  constructor(private http: HttpClient) 
  { 
    this.http =http;
  }

  public getUser(){

    return new Promise((resolve, reject) => {
      this.http.get<any>(`${environment.backendApiUrl}/api/user`, 
      {headers: this.headers}).subscribe({
        next: res =>{
          resolve(res)
        },
        error: err => {
          reject(err);
        },
        });
    });
  }
}

