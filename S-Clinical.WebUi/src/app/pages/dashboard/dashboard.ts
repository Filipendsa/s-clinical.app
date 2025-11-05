import { Component } from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { Toolbar } from '../../shared/components/toolbar/toolbar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  imports: [ButtonModule, Toolbar],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {
  constructor(private _router: Router) {  }

  public routerTo(path: string): void {
    this._router.navigate([path]);
  }
}
