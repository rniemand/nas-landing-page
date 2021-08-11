import { Component, OnInit } from '@angular/core';
import { LinkService } from '../../services/link.service';

@Component({
  selector: 'app-links',
  templateUrl: './links.component.html',
  styleUrls: ['./links.component.scss']
})
export class LinksComponent implements OnInit {

  constructor(
    private _linkService: LinkService
  ) { }

  ngOnInit(): void {
    console.log(this._linkService);
  }

}
