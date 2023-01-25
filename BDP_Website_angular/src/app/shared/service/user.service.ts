import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { SnackbarService } from "./snackbar.service";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private headers = new HttpHeaders()
                .set("Content-Type","application/json")
                .set("Authorization", 'Bearer ' + localStorage.getItem('token'));

  constructor(private http: HttpClient) 
  { 
    this.http = http;
  }

  public async getUsers(){
    return await new Promise((resolve, reject) => {
      this.http.get<any>(`${environment.backendApiUrl}/api/user`, 
      {headers: this.headers}).subscribe({
        next: res =>{
          resolve(res)
        },
        error: err => {
          reject(err)
        },
        });
    });
  }

  public async addUser(user: any){
   return await new Promise((resolve, reject) => {
      this.http.post<any>(`${environment.backendApiUrl}/api/user`,
      user,
      {headers: this.headers}).subscribe({
        next: res =>{
          resolve(res.message)
        },
        error:err=>{
          reject(err)
         }
      })
   })
  }

  public async editUser(user:any){
    return await new Promise((resolve, reject) => {
      this.http.put<any>(`${environment.backendApiUrl}/api/user`,
      user,
      {headers: this.headers}).subscribe({
        next: res =>{
          resolve(res.message)
        },
        error:err=>{
          reject(err)
         }
      })
   })
  }  

  public async deleteUser(emailAddress:string){
    return await new Promise((resolve, reject) => {
      this.http.delete<any>(`${environment.backendApiUrl}/api/user?emailaddress=${emailAddress}`,
      {headers: this.headers}).subscribe({
        next: res =>{
          resolve(res.message)
        },
        error:err=>{
          reject(err)
         }
      })
   })
  }  
}

