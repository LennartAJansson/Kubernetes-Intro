import { Injectable, inject } from '@angular/core';
import { IBuildVersionsApiService } from '../abstract/ibuild-versions-api.service';
import { ApiService } from './api-service';
import { BuildVersion } from '../models/build-version.model';
import { firstValueFrom, lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BuildVersionsApiService implements IBuildVersionsApiService {
  apiSvc = inject(ApiService);

  async getById(id: number): Promise<BuildVersion> {
    return await firstValueFrom(this.apiSvc.getById(id));
  }
  async getByName(name: string): Promise<BuildVersion> {
    return await firstValueFrom(this.apiSvc.getByName(name));
  }
  async getAll(): Promise<BuildVersion[]> {
    return await lastValueFrom(this.apiSvc.getAll());
  }
  async update(buildVersion: BuildVersion): Promise<BuildVersion> {
    return await firstValueFrom(this.apiSvc.update(buildVersion));
  }
  async add(buildVersion: BuildVersion): Promise<BuildVersion> {
    return await firstValueFrom(this.apiSvc.add(buildVersion));
  }
  async delete(id: number): Promise<BuildVersion> {
    return await firstValueFrom(this.apiSvc.delete(id));
  }
}
