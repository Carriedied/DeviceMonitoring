import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Device {
  id: string;
  userName: string;
}

export interface Session {
  id: string;
  name: string;
  startTime: string;
  endTime: string;
  version: string;
}

@Injectable({
  providedIn: 'root'
})

export class ApiService {
  private baseUrl = 'http://localhost:5000/api/device';

  constructor(private http: HttpClient) { }

  getDevices(): Observable<Device[]> {
    return this.http.get<Device[]>(this.baseUrl);
  }

  getSessionsForDevice(id: string): Observable<Session[]> {
    return this.http.get<Session[]>(`${this.baseUrl}/device/${id}`);
  }
}