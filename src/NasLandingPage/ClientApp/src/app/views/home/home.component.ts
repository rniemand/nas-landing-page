import { Component } from '@angular/core';
import { ProjectsClient } from 'src/app/nlp-api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(private _projectsClient: ProjectsClient) {
    this._projectsClient.getAll().toPromise().then(projects => {
      console.log(projects);
    });
  }
}
