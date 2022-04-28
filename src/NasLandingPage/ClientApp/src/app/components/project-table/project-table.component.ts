import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { ClientConfig, ProjectInfo, ProjectTableColumn } from 'src/app/nlp-api';

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

  constructor() { }
  
  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  ngOnChanges(_: SimpleChanges): void {
    this.hasConfig = (this?.config ?? undefined) !== undefined;
    this.hasProjects = this.projects.length > 0;
    this.canRender = this.hasConfig && this.hasProjects;
  }

  public canRenderColumn = (column: string) => {
    if(!this.hasConfig || !this.config) { return false; }
    let resolved = (<any>ProjectTableColumn)[column];

    if(this.config.columns.indexOf(resolved) === -1) {
      return false;
    }

    return true;
  }
}
