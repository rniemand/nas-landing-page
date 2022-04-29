import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { UserLink } from 'src/app/nlp-api';

@Component({
  selector: 'user-link',
  templateUrl: './user-link.component.html',
  styleUrls: ['./user-link.component.scss']
})
export class UserLinkComponent implements OnInit, OnChanges {
  @Input('link') link?: UserLink = undefined;

  canRender: boolean = false;

  constructor() { }

  ngOnInit(): void { }

  ngOnChanges(_: SimpleChanges): void {
    this.canRender = (this?.link ?? undefined) !== undefined;
  }

}
