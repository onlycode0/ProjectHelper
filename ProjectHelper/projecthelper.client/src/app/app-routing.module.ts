import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { AccountLayoutComponent } from './layouts/account-layout/account-layout.component';
import { ProjectsComponent } from './pages/projects/projects.component';
import { DevelopersComponent } from './pages/developers/developers.component';
import { StatisticsComponent } from './pages/statistics/statistics.component';
import { MainHeaderComponent } from './common/main-header/main-header.component';
import { CreateCompanyComponent } from './pages/create-company/create-company.component';
import { WelcomeComponent } from './pages/welcome/welcome.component';

const routes: Routes = [
  { path: '', component: MainPageComponent },
  {
    path: 'account',
    component: AccountLayoutComponent,
    children: [
      { path: '', redirectTo: 'projects', pathMatch: 'full' },
      { path: 'projects', component: ProjectsComponent },
      { path: 'developers', component: DevelopersComponent },
      { path: 'statistics', component: StatisticsComponent }
    ]
  },
  { path: 'create-company', component: CreateCompanyComponent },
  { path: 'welcome', component: WelcomeComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
