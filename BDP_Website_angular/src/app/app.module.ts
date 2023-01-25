import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule } from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {MatTableModule} from '@angular/material/table';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { AdminlayoutComponent } from './shared/component/adminlayout/adminlayout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from "ngx-spinner";  
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { UserComponent } from './components/user/user.component';
import {MatDialogModule} from '@angular/material/dialog';
import { AddUserDialogComponent } from './shared/dialog/add-user-dialog/add-user-dialog.component';
import {MatSelectModule} from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { EditUserDialogComponent } from './shared/dialog/edit-user-dialog/edit-user-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminlayoutComponent,
    UserComponent,
    AddUserDialogComponent,
    EditUserDialogComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatGridListModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatTableModule,
    MatProgressSpinnerModule,
    NgxSpinnerModule,
    HttpClientModule,
    MatSnackBarModule,
    MatDialogModule,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
