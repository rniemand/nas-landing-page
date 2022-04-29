import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { ClientConfig, ProjectInfo, ProjectsClient, ProjectTableColumn, RunCommandRequest, RunCommandResponse } from 'src/app/nlp-api';

@Component({
  selector: 'app-project-table',
  templateUrl: './project-table.component.html',
  styleUrls: ['./project-table.component.scss']
})
export class ProjectTableComponent implements OnInit, OnDestroy, OnChanges {
  @Input('config') config?: ClientConfig = undefined;
  @Input('projects') projects: ProjectInfo[] = [];

  hasConfig: boolean = false;
  hasProjects: boolean = false;
  canRender: boolean = false;
  showNotification: boolean = false;
  notificationClass: string = 'alert alert-secondary';
  notification: string = '';

  constructor(private _projects: ProjectsClient) { }
  
  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  ngOnChanges(_: SimpleChanges): void {
    this.hasConfig = (this?.config ?? undefined) !== undefined;
    this.hasProjects = this.projects.length > 0;
    this.canRender = this.hasConfig && this.hasProjects;
  }

  public refreshProject = (project: ProjectInfo) => {
    const request = new RunCommandRequest({
      command: 'SyncProject',
      args: project.metadata.fileNameWithoutExtension
    });

    this._showNotification(`Updating project: ${project.name}`);
    this._projects.syncProject(request).toPromise().then(
      (response: RunCommandResponse) => {
        let messages = response.messages.join(', ');
        this._showSuccess(`Updated "${project.name}": ${messages}`);
      },
      (error: any) => {
        this._showError(`Error updating: ${error}`);
      }
    )
  }

  public canRenderColumn = (column: string) => {
    if(!this.hasConfig || !this.config) { return false; }
    let resolved = (<any>ProjectTableColumn)[column];

    if(this.config.columns.indexOf(resolved) === -1) {
      return false;
    }

    return true;
  }

  private _showNotification = (message: string) => {
    this.showNotification = true;
    this.notification = message;
    this.notificationClass = 'alert alert-secondary';
  }

  private _showError = (message: string) => {
    this.showNotification = true;
    this.notification = message;
    this.notificationClass = 'alert alert-danger';
  }

  private _showSuccess = (message: string) => {
    this.showNotification = true;
    this.notification = message;
    this.notificationClass = 'alert alert-success';
    setTimeout(() => { this.showNotification = false; }, 2000);
  }
}
