import { Component, inject } from '@angular/core';
import { AngularMaterialComponent } from '../../common/angular-material/angular-material.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { BuildVersionsListComponent } from '../build-versions-list/build-versions-list.component';
import { IBuildVersionsApiService } from '../../abstract/ibuild-versions-api.service';
import { BuildVersionsApiService } from '../../services/build-versions-api.service';

@Component({
  selector: 'app-build-version-add',
  standalone: true,
  imports: [
    AngularMaterialComponent,
    MatFormFieldModule,
    MatDialogModule,
    MatInputModule,
    FormsModule,
    BuildVersionsListComponent,
  ],
  templateUrl: './build-version-add.component.html',
  styleUrl: './build-version-add.component.scss',
})
export class BuildVersionAddComponent {
  title = 'Add BuildVersion';

  dialogRef = inject(MatDialogRef);
  data = inject(MAT_DIALOG_DATA);
  service = inject(BuildVersionsApiService) as IBuildVersionsApiService;
  errorMessage: string | undefined;

  onNoClick(): void {
    this.dialogRef.close();
  }

  async save(): Promise<void> {
    try {
      this.data = await this.service.add(this.data);
      console.log(this.data);
      this.dialogRef.close(this.data);
    } catch (error) {
      this.errorMessage =
        'An error occured while adding a BuildVersion. Try again!';
      console.error(error);
    }
  }
}
