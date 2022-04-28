import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'array-count',
  templateUrl: './array-count.component.html',
  styleUrls: ['./array-count.component.scss']
})
export class ArrayCountComponent implements OnInit, OnChanges {
  @Input('value') value: any[] = [];
  count: number = 0;

  constructor() { }
  
  ngOnInit(): void {
  }

  ngOnChanges(_: SimpleChanges): void {
    this.count = this.value.length;
  }

}
