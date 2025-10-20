import { Component, OnInit } from '@angular/core';
import { Device, ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-devices-list',
  templateUrl: './devices-list.component.html',
  styleUrls: ['./devices-list.component.less'],
  standalone: true,
  imports: [CommonModule]
})

export class DevicesListComponent implements OnInit {
  devices: Device[] = [];
  loading = true;

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.api.getDevices().subscribe({
      next: (data) => {
        this.devices = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Load failed', err);
        this.loading = false;
      }
    });
  }
}