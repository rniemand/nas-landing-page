import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { UserLink, UserLinksClient } from 'src/app/nlp-api';

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

  constructor(private _linkClient: UserLinksClient) { }

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

    const linkUrl = this.link.url;
    const linkId = this.link.id;

    console.log(linkId);

    this._linkClient.registerLinkFollow(linkId).toPromise().then(
      () => { window.open(linkUrl, '_blank'); },
      () => { window.open(linkUrl, '_blank'); }
    );
  }
}
