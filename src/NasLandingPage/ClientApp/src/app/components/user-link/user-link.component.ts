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
  hasImage: boolean = false;
  linkImage: string = '';

  constructor() { }

  ngOnInit(): void { }

  ngOnChanges(_: SimpleChanges): void {
    // http://localhost:44497/api/UserLinks/image/my-home.png
    this.canRender = (this?.link ?? undefined) !== undefined;
    if(!this.canRender) { return; }

    const img = this?.link?.image ?? '';
    this.linkImage = `/api/UserLinks/image/${img}`;
    this.hasImage = img !== '';
  }

  public follow = () => {
    if(!this?.link?.url) {
      return;
    }

    window.open(this.link.url, '_blank');
  }
}
