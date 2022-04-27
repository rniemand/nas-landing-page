import { Component } from '@angular/core';
import { ConfigClient, ProjectsClient } from 'src/app/nlp-api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(
    private _projectsClient: ProjectsClient,
    private _configClient: ConfigClient) {
    this._projectsClient.getAll().toPromise().then(projects => {
      console.log(projects);
    });

    this._configClient.getClientConfig().toPromise().then(config => {
      console.log(config);
    });
  }
}
