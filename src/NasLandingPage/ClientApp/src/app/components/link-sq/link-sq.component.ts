import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ClientConfig, ProjectInfo } from 'src/app/nlp-api';

@Component({
  selector: 'link-sq',
  templateUrl: './link-sq.component.html',
  styleUrls: ['./link-sq.component.scss']
})
export class LinkSqComponent implements OnInit, OnChanges {
  @Input('project') project?: ProjectInfo = undefined;
  @Input('config') config?: ClientConfig = undefined;

  hasProject: boolean = false;
  hasSonarQube: boolean = false;
  sqUrl: string = '';

  constructor() { }

  ngOnInit(): void {
  }

  ngOnChanges(_: SimpleChanges): void {
    this.hasProject = (this?.project ?? undefined) !== undefined;
    this.hasSonarQube = (this?.project?.sonarQube?.url?.length ?? 0) > 4;
    this._processUrl();
  }

  private _processUrl = () => {
    this.sqUrl = '';
    if(!this.hasSonarQube) { return; }
    var baseUrl = this?.project?.sonarQube?.url ?? '';
    const sqBaseUrl = this?.config?.sonarQubeUrl ?? '';
    const sqId = this?.project?.sonarQube?.id ?? '';

    this.sqUrl = baseUrl
      .replace('{sonarQubeBaseUrl}', sqBaseUrl)
      .replace('{id}', sqId);
  }

}
