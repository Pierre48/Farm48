import 'hammerjs';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: 'SUPPLIER_URL', useValue: "http://localhost:8001/api/v1/" },
  { provide: 'BILLING_URL', useValue: "http://localhost:8002/api/v1/" },
  { provide: 'ANIMAL_URL', useValue: "http://localhost:8003/api/v1/" }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
