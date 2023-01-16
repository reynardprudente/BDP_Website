import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminlayoutComponent } from './shared/component/adminlayout/adminlayout.component';
import { UserComponent } from './components/user/user.component';
import { AuthGuard } from './shared/guard/auth.guard';
import { Role } from './shared/enum/roles';

const routes: Routes = [
  { path:'login', component: LoginComponent},


  {path:'', redirectTo:'/login', pathMatch:'full'},
  {path:'',
    component:AdminlayoutComponent,
    canActivate: [AuthGuard],
    data:{
      roles:'customer'
    },
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
