import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'raw-count',
  templateUrl: './raw-count.component.html',
  styleUrls: ['./raw-count.component.scss']
})
export class RawCountComponent implements OnInit, OnChanges {
  @Input('value') value?: number = undefined;

  hasValue: boolean = false;
  safeValue: number = 0;

  constructor() { }
  
  ngOnInit(): void {}

  ngOnChanges(_: SimpleChanges): void {
    this.hasValue = (this?.value ?? undefined) !== undefined;
    this.safeValue = this?.value ?? 0;
  }

}
