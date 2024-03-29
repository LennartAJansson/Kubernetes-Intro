import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { BuildVersion } from '../../models/build-version.model';
import { ActivatedRoute } from '@angular/router';
import { BuildVersionsApiService } from '../../services/build-versions-api.service';
import { IBuildVersionsApiService } from '../../abstract/ibuild-versions-api.service';

@Component({
  selector: 'app-build-version-detail',
  standalone: true,
  imports: [MatCardModule],
  templateUrl: './build-version-detail.component.html',
  styleUrl: './build-version-detail.component.scss',
})
export class BuildVersionDetailComponent {
  title = 'BuildVersion Details';
  public buildVersion?: BuildVersion;
  errorMessage: string | undefined;
  id?: number;

  activatedRoute = inject(ActivatedRoute);
  service = inject(BuildVersionsApiService) as IBuildVersionsApiService;

  async ngOnInit(): Promise<void> {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    this.id = parseInt(id!);
    this.fetch(this.id!);
  }

  async fetch(id: number): Promise<void> {
    try {
      this.buildVersion = await this.service.getById(id);
      console.log(this.buildVersion);
    } catch (error) {
      this.errorMessage = 'An error occurred while fetching BuildVersion.';
      console.error(error);
    }
  }
}
