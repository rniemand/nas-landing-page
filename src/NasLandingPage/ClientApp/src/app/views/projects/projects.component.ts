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

  constructor(private _config: ConfigClient, private _projects: ProjectsClient) { }

  ngOnInit(): void {
    this._projects.getAll().toPromise().then(projects => { this.projects = projects; });
    this._config.getClientConfig().toPromise().then(config => { this.clientConfig = config; });
  }

}
