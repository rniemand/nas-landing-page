import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'bool-tick',
  templateUrl: './bool-tick.component.html',
  styleUrls: ['./bool-tick.component.scss']
})
export class BoolTickComponent implements OnInit {
  @Input('value') value: boolean = false;

  constructor() { }

  ngOnInit(): void { }
}
