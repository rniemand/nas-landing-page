import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ClientConfig, UserLink } from 'src/app/nlp-api';

@Component({
  selector: 'link-category',
  templateUrl: './link-category.component.html',
  styleUrls: ['./link-category.component.scss']
})
export class LinkCategoryComponent implements OnInit, OnChanges {
  @Input('category') category?: string = undefined;
  @Input('links') links: UserLink[] = [];
  @Input('config') clientConfig?: ClientConfig = undefined;

  canDisplay: boolean = false;

  constructor() { }
  
  ngOnInit(): void { }

  ngOnChanges(changes: SimpleChanges): void {
    this.canDisplay = 
      (this?.category?.length ?? 0) > 0 &&
      this.links.length > 0 &&
      (this?.clientConfig ?? undefined) !== undefined;
  }

}
