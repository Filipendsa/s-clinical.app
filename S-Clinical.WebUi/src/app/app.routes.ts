import { Routes } from '@angular/router';
import { NotFound } from './pages/not-found/not-found';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('./pages/main.routes').then((r) => r.MAIN_ROUTES)
    },
    { path: '**', component: NotFound }
];
