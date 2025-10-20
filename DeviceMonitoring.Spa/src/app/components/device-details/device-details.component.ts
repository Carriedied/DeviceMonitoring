import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Session, ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-device-details',
  templateUrl: './device-details.component.html',
  styleUrls: ['./device-details.component.less'],
  standalone: true,
  imports: [CommonModule]
})
export class DeviceDetailsComponent implements OnInit {
  deviceId: string = '';
  sessions: Session[] = [];
  loading = true;

  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit(): void {
    this.deviceId = this.route.snapshot.paramMap.get('id')!;
    this.loadSessions();
  }

  loadSessions(): void {
    this.api.getSessionsForDevice(this.deviceId).subscribe({
      next: (data) => {
        this.sessions = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Load failed', err);
        this.loading = false;
      }
    });
  }
}