import { Routes } from '@angular/router';
import { DevicesListComponent } from './components/devices-list/devices-list.component';
import { DeviceDetailsComponent } from './components/device-details/device-details.component';

export const routes: Routes = [
  { path: '', component: DevicesListComponent },
  { path: 'details/:id', component: DeviceDetailsComponent },
  { path: '**', redirectTo: '' }
];