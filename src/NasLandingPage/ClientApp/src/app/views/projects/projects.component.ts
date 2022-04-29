import { Component, OnInit } from '@angular/core';
import { ClientConfig, ConfigClient, ProjectInfo, ProjectsClient } from 'src/app/nlp-api';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {
  clientConfig?: ClientConfig = undefined;
  projects: ProjectInfo[] = [];

  constructor(
    private _projectsClient: ProjectsClient,
    private _configClient: ConfigClient) { }

  ngOnInit(): void {
    this._projectsClient.getAll().toPromise().then(projects => {
      this.projects = projects;
    });

    this._configClient.getClientConfig().toPromise().then(config => {
      this.clientConfig = config;
    });
  }

}
