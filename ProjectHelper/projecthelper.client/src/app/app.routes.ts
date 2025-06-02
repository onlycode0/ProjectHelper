import { Routes } from '@angular/router';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { CreateCompanyComponent } from './pages/create-company/create-company.component';
import { AccountLayoutComponent } from './layouts/account-layout/account-layout.component';
import { ProjectsComponent } from './pages/projects/projects.component';
import { DevelopersComponent } from './pages/developers/developers.component';
import { StatisticsComponent } from './pages/statistics/statistics.component';

export const routes: Routes = [
  { path: '', redirectTo: '/welcome', pathMatch: 'full' },
  { path: 'welcome', component: WelcomeComponent },
  { path: 'create-company', component: CreateCompanyComponent },
  {
    path: 'account',
    component: AccountLayoutComponent,
    children: [
      { path: '', redirectTo: 'projects', pathMatch: 'full' },
      { path: 'projects', component: ProjectsComponent },
      { path: 'developers', component: DevelopersComponent },
      { path: 'statistics', component: StatisticsComponent }
    ]
  }
]; 