import { Component, inject } from '@angular/core';
import { IBuildVersionsApiService } from '../../abstract/ibuild-versions-api.service';
import { BuildVersionsApiService } from '../../services/build-versions-api.service';
import { BuildVersion } from '../../models/build-version.model';
import { AngularMaterialComponent } from '../../common/angular-material.component';
import { RouterLink } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { BuildVersionAddComponent } from '../build-version-add/build-version-add.component';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-build-versions-list',
  standalone: true,
  imports: [
    AngularMaterialComponent,
    RouterLink,
    MatIconModule,
    MatTableModule,
    MatButtonModule,
  ],
  templateUrl: './build-versions-list.component.html',
  styleUrl: './build-versions-list.component.scss',
})
export class BuildVersionsListComponent {
  title = 'List of BuildVersions';
  service = inject(BuildVersionsApiService) as IBuildVersionsApiService;
  dialog = inject(MatDialog);

  public buildVersions: BuildVersion[] = [];
  public newBuildVersion: BuildVersion = {} as BuildVersion;
  public dataSource = new MatTableDataSource<BuildVersion>();
  columns: string[] = [
    'id',
    'projectName',
    'version',
    'release',
    'semanticVersion',
    'semanticRelease',
    'actions',
  ];
  errorMessage: string | undefined;
  currentIndex = -1;

  async ngOnInit(): Promise<void> {
    await this.fetch();
  }

  async fetch(): Promise<void> {
    try {
      this.buildVersions = await this.service.getAll();
      console.log(this.buildVersions);
      this.dataSource = new MatTableDataSource(this.buildVersions);
    } catch (error) {
      this.errorMessage = 'An error occurred while fetching BuildVersions.';
      console.error(error);
    }
  }

  async refresh(): Promise<void> {
    await this.fetch();
    this.currentIndex = -1;
  }

  setActive(buildVersion: BuildVersion, index: number): void {
    this.currentIndex = index;
  }

  async remove(id: number): Promise<void> {
    try {
      const res = await this.service.delete(id);
      console.log(res);
      await this.refresh();
    } catch (e) {
      console.error(e);
      this.errorMessage =
        'An error occurred while deleting the BuildVersion. Please try again.';
    }
  }

  async openDialog(): Promise<void> {
    this.newBuildVersion = {} as BuildVersion;
    const dialogRef = this.dialog.open(BuildVersionAddComponent, {
      data: this.newBuildVersion,
    });

    const result: BuildVersion = await firstValueFrom(dialogRef.afterClosed());

    console.log(result);

    if (result && result.projectName && result.projectName.length !== 0) {
      await this.add(result);
    }
  }

  async add(result: BuildVersion): Promise<void> {
    try {
      const res = await this.service.add(result);
      console.log(res);
      await this.refresh();
    } catch (e) {
      console.error(e);
      this.errorMessage =
        'An error occurred while adding the category. Please try again.';
    }
    this.currentIndex = -1;
  }
}
