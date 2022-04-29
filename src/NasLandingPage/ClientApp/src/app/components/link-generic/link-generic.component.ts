import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'link-generic',
  templateUrl: './link-generic.component.html',
  styleUrls: ['./link-generic.component.scss']
})
export class LinkGenericComponent implements OnInit, OnChanges {
  @Input('url') url?: string = undefined;
  @Input('name') name: string = '';

  hasLink: boolean = false;

  constructor() { }

  ngOnInit(): void { }

  ngOnChanges(_: SimpleChanges): void {
    this.hasLink = (this?.url ?? undefined) !== undefined;
  }

}
