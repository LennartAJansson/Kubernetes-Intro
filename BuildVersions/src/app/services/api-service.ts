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
  private baseUrl: string = environment.baseUrl + 'BuildVersion';

  getById(id: number): Observable<BuildVersion> {
    const url = this.baseUrl + '/ReadById/' + id;
    return this.http.get<BuildVersion>(url);
  }
  getByName(name: string): Observable<BuildVersion> {
    const url = this.baseUrl + '/ReadByName/' + name;
    return this.http.get<BuildVersion>(url);
  }
  getAll(): Observable<BuildVersion[]> {
    console.log(this.baseUrl);
    const url = this.baseUrl + '/ReadAll';
    return this.http.get<BuildVersion[]>(url);
  }
  update(buildVersion: BuildVersion): Observable<BuildVersion> {
    const url = this.baseUrl + '/Update';
    return this.http.put<BuildVersion>(url, buildVersion);
  }
  add(buildVersion: BuildVersion): Observable<BuildVersion> {
    const url = this.baseUrl + '/Create';
    return this.http.post<BuildVersion>(url, buildVersion);
  }
  delete(id: number): Observable<BuildVersion> {
    const url = this.baseUrl + '/Delete/' + id;
    return this.http.delete<BuildVersion>(url);
  }
}
