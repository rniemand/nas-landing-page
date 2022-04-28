import { Component } from '@angular/core';
import { ClientConfig, ConfigClient, ProjectInfo, ProjectsClient } from 'src/app/nlp-api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  clientConfig?: ClientConfig = undefined;
  projects: ProjectInfo[] = [];

  constructor(
    private _projectsClient: ProjectsClient,
    private _configClient: ConfigClient) {
    this._projectsClient.getAll().toPromise().then(projects => {
      this.projects = projects;
    });

    this._configClient.getClientConfig().toPromise().then(config => {
      this.clientConfig = config;
    });
  }
}
