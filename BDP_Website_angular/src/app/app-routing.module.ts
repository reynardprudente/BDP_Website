import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login/login.component';
import { AdminlayoutComponent } from './shared/component/adminlayout/adminlayout.component';
import { UserComponent } from './user/user/user.component';

const routes: Routes = [
  { path:'login', component: LoginComponent},


  {path:'', redirectTo:'/login', pathMatch:'full'},
  {path:'',
    component:AdminlayoutComponent,
    children:[
      {path:'admin/user', component: UserComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
