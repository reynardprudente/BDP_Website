import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/service/user.service';

export interface User {
  role: string;
  firstName: string;
  middleName: string;
  lastName: string;
  emailAddress:string;
}

const ELEMENT_DATA: User[] = [
  {role: 'admin', firstName: 'Reynard', middleName: 'Perez', lastName: 'Prudente', emailAddress:'Reynard@gmail.com'},
  {role: 'customer', firstName: 'Grace', middleName: 'Prudente',  lastName: 'De Jesus', emailAddress:'Grace@gmail.com'},
  {role: 'customer', firstName: 'Dorothy', middleName: 'Perez',  lastName: 'Prudente', emailAddress:'Dorothy@gmail.com'}
];
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  displayedColumns: string[] = ['role', 'firstname', 'middleName', 'lastName', 'emailAddress', 'actions'];
  dataSource = ELEMENT_DATA;

  constructor(private userService: UserService){
    this.userService = userService;
  }

  async ngOnInit() {
    
    let a = await this.userService.getUser()
    // .subscribe((response )=>{
    //   console.log(response.json())
    // })
    console.log(a)
  }
}

