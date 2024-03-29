import { Version } from "@angular/core";

export class BuildVersion {
  id?: number;
  projectName?: string;
  major?: number;
  minor?: number;
  build?: number;
  revision?: number;
  semanticVersionText?: string;
  version?: string;// Version;
  release?: string;
  semanticVersion?: string;
  semanticRelease?: string;

  username?: string;
  created?: Date;
  changed?: Date;
}
