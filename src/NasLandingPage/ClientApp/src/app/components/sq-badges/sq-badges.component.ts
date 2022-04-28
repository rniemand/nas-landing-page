import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ClientConfig, ProjectInfo } from 'src/app/nlp-api';

@Component({
  selector: 'sq-badges',
  templateUrl: './sq-badges.component.html',
  styleUrls: ['./sq-badges.component.scss']
})
export class SqBadgesComponent implements OnInit, OnChanges {
  @Input('project') project?: ProjectInfo = undefined;
  @Input('config') config?: ClientConfig = undefined;

  hasConfig: boolean = false;
  hasProject: boolean = false;
  canRender: boolean = false;
  badges: string[] = [];
  badgeCount: number = 0;

  constructor() { }
  
  ngOnInit(): void {
  }

  ngOnChanges(_: SimpleChanges): void {
    this.hasConfig = (this?.config ?? undefined) !== undefined;
    this.hasProject = (this?.project ?? undefined) !== undefined;
    this.canRender = this.hasConfig && this.hasProject;
    this._processBadges();
  }

  private _isAllowed = (badge: string) => {
    if(!badge) { return false; }
    if((this?.config?.badges ?? []).indexOf(badge) === -1) {
      return false;
    }

    return true;
  }

  private _processBadges = () => {
    this.badges = [];
    this.badgeCount = 0;

    if(!this.canRender) { return; }
    var baseUrl = this?.config?.sonarQubeUrl ?? '';
    var projectId = this?.project?.sonarQube?.projectId ?? '';
    var tokenBadge = this?.project?.sonarQube?.badgeToken ?? '';
    var rawBadges = (this?.project?.sonarQube?.badges ?? []);
    var badgeUrls: string[] = [];

    Object.getOwnPropertyNames(rawBadges).forEach(badgeName => {
      if(!this._isAllowed(badgeName)) { return; }
      var badgeUrl = (this?.project?.sonarQube?.badges[badgeName] ?? '')
        .replace('{sonarQubeBaseUrl}', baseUrl)
        .replace('{id}', projectId)
        .replace('{tokenBadge}', tokenBadge);

      badgeUrls.push(badgeUrl);
    });

    this.badges = badgeUrls;
    this.badgeCount = this.badges.length;
  }

}
