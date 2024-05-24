import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from './environments/environment.local';
import { BuildVersion } from '../models/build-version.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.baseUrl;

  getById(id: number): Observable<BuildVersion> {
    const url = this.baseUrl + '/api/BuildVersion/ReadById/' + id +'/v1'; 
    return this.http.get<BuildVersion>(url);
  }
  getByName(name: string): Observable<BuildVersion> {
    const url = this.baseUrl + '/api/BuildVersion/ReadByName/' + name+'/v1';
    return this.http.get<BuildVersion>(url);
  }
  getAll(): Observable<BuildVersion[]> {
    console.log(this.baseUrl);
    const url = this.baseUrl + '/api/BuildVersion/ReadAll/v1';
    return this.http.get<BuildVersion[]>(url);
  }
  update(buildVersion: BuildVersion): Observable<BuildVersion> {
    const url = this.baseUrl + '/api/BuildVersion/Update/v1';
    return this.http.put<BuildVersion>(url, buildVersion);
  }
  add(buildVersion: BuildVersion): Observable<BuildVersion> {
    const url = this.baseUrl + '/api/BuildVersion/Create/v1';
    return this.http.post<BuildVersion>(url, buildVersion);
  }
  delete(name: string): Observable<BuildVersion> {
    const url = this.baseUrl + '/api/BuildVersion/Delete/' + name + '/v2';
    return this.http.delete<BuildVersion>(url);
  }
}
