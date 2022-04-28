import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ProjectInfo, RepoType } from 'src/app/nlp-api';

@Component({
  selector: 'repo-type',
  templateUrl: './repo-type.component.html',
  styleUrls: ['./repo-type.component.scss']
})
export class RepoTypeComponent implements OnInit, OnChanges {
  @Input('project') project?: ProjectInfo = undefined;

  hasProject: boolean = false;
  repoType: string = 'Unknown';

  constructor() { }

  ngOnInit(): void { }

  ngOnChanges(changes: SimpleChanges): void {
    this.hasProject = (this?.project ?? undefined) !== undefined;
    this.repoType = this.hasProject ? RepoType[this.project!.repo.repoType] : "";
  }
}
