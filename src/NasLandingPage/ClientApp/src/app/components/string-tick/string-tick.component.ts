import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'string-tick',
  templateUrl: './string-tick.component.html',
  styleUrls: ['./string-tick.component.scss']
})
export class StringTickComponent implements OnInit, OnChanges {
  @Input('value') value?: string = undefined;
  hasValue: boolean = false;

  constructor() { }
  
  ngOnInit(): void { }

  ngOnChanges(_: SimpleChanges): void {
    this.hasValue = (this?.value?.length ?? 0) > 0;
  }
}
