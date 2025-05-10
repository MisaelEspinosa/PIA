import { Routes } from '@angular/router';
import { provideRouter } from '@angular/router';

export const routes: Routes = [
  // Ruta predeterminada, redirige al login
  {
    path: '',
    loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent),
  },
  // Ruta para login
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'home-cliente',
    loadComponent: () => import('./pages/cliente/home-cliente/home-cliente.component').then(m => m.HomeClienteComponent)
  },
  {
    path: 'admin',
    loadComponent: () => import('./pages/admin/dashboard-admin/dashboard-admin.component').then(m => m.DashboardAdminComponent)
  },
];

export const appRouterProviders = [
  provideRouter(routes)
];
