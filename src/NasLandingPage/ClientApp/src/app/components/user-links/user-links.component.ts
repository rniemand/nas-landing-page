import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ClientConfig, UserLink } from 'src/app/nlp-api';

@Component({
  selector: 'user-links',
  templateUrl: './user-links.component.html',
  styleUrls: ['./user-links.component.scss']
})
export class UserLinksComponent implements OnInit, OnChanges {
  @Input('config') config?: ClientConfig = undefined;
  @Input('links') links: UserLink[] = [];

  canRender: boolean = false;

  constructor() { }
  
  ngOnInit(): void { }

  ngOnChanges(_: SimpleChanges): void {
    const hasConfig = (this?.config ?? undefined) != undefined;
    this.canRender = hasConfig && this.links.length > 0;

    console.log(this.canRender);
  }

}
