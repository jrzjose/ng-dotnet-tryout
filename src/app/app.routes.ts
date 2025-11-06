import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login';
import { SignupComponent } from './components/signup/signup';
import { TransactionListComponent } from './components/transaction-list/transaction-list';
import { TransactionFormComponent } from './components/transaction-form/transaction-form';
import { authGuard } from './guards/authguard';
 
export const routes: Routes = [
    {
        path:'login',
        component: LoginComponent
    },
    {
        path:'signup',
        component: SignupComponent
    },
    {
        path:'transactions',
        component:TransactionListComponent,
        canActivate: [authGuard]
    },
    {
        path:'add',
        component: TransactionFormComponent,
        canActivate: [authGuard]
    },
    {
        path:'edit/:id',
        component: TransactionFormComponent,
        canActivate: [authGuard]
    },
    // {
    //     path:'',
    //     redirectTo:'/transactions',
    //     pathMatch:'full',
    //     canActivate: [authGuard]
    // },
    {
        path:'**',
        redirectTo:'/transactions'
    }
];