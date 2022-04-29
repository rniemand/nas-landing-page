import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'human-size',
  templateUrl: './human-size.component.html',
  styleUrls: ['./human-size.component.scss']
})
export class HumanSizeComponent implements OnInit, OnChanges {
  @Input('value') value?: number = undefined;
  modifier: string = 'B';
  printValue: number = 0;

  constructor() { }

  ngOnInit(): void { }

  ngOnChanges(_: SimpleChanges): void {
    let safeValue = this?.value ?? 0;
    this.modifier = 'B';
    this.printValue = 0;

    if(safeValue <= 1024) {
      this.printValue = safeValue;
    }
    else if(safeValue <= 1048576) {
      this.printValue = Math.ceil(safeValue / 1024);
      this.modifier = 'Kb';
    }
    else if(safeValue <= 1073741824) {
      this.printValue = Math.ceil(safeValue / 1048576);
      this.modifier = 'Mb';
    }
    else {
      this.printValue = 999;
      this.modifier = 'Add more!';
    }
  }

}
