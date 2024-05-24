import { BuildVersion } from '../models/build-version.model';

export interface IBuildVersionsApiService {
  getById(id: number): Promise<BuildVersion>;
  getByName(name: string): Promise<BuildVersion>;
  getAll(): Promise<BuildVersion[]>;
  update(buildVersion: BuildVersion): Promise<BuildVersion>;
  add(buildVersion: BuildVersion): Promise<BuildVersion>;
  delete(name: string): Promise<BuildVersion>;
}
