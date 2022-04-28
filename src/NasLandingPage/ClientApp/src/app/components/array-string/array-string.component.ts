import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'array-string',
  templateUrl: './array-string.component.html',
  styleUrls: ['./array-string.component.scss']
})
export class ArrayStringComponent implements OnInit {
  @Input('value') value: string[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
