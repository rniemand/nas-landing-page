import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ClientConfig, ProjectTableColumn } from 'src/app/nlp-api';

interface TableColumnKey {
  value: number;
  name: string;
  checked: boolean;
  enumValue: ProjectTableColumn;
}

@Component({
  selector: 'option-editor',
  templateUrl: './option-editor.component.html',
  styleUrls: ['./option-editor.component.scss']
})
export class OptionEditorComponent implements OnInit, OnChanges {
  @Input('config') config?: ClientConfig = undefined;

  hasConfig: boolean = false;
  options: TableColumnKey[] = [];

  constructor() { }

  ngOnInit(): void {
    this._refreshOptions();
  }

  ngOnChanges(_: SimpleChanges): void {
    this.hasConfig = (this?.config ?? undefined) !== undefined;
    this._refreshOptions();
  }

  onChange = (e: any) => {
    var targetValue = Number(e.target.value);
    var isChecked = e.target.checked;

    var name = ProjectTableColumn[targetValue];
    var enumValue = (<any>ProjectTableColumn)[name];
    var exists = this.config?.columns.indexOf(enumValue) !== -1;

    if(isChecked && !exists) {
      this.config?.columns.push(enumValue);
    }
    if(!isChecked && exists) {
      this.config?.columns.splice(this.config?.columns.indexOf(enumValue), 1);
    }
  }

  private _refreshOptions = () => {
    let parsedValues: TableColumnKey[] = [];
    const optionColumns: ProjectTableColumn[] = this?.config?.columns ?? [];

    Object.keys(ProjectTableColumn).forEach(key => {
      let castNumber = Number(key);
      if(isNaN(castNumber)) { return; }

      var name = ProjectTableColumn[castNumber];
      var enumValue = (<any>ProjectTableColumn)[name];
      var idx = optionColumns.indexOf(enumValue);
      
      parsedValues.push({
        checked: idx !== -1,
        name: ProjectTableColumn[castNumber],
        value: castNumber,
        enumValue: enumValue
      });
    });

    this.options = parsedValues;
  }

  private _setCheckedState = (value: number, checked: boolean) => {
    this.options.forEach(option => {
      if(option.value !== value) { return; }
      console.log(option);
    });
  }
}
