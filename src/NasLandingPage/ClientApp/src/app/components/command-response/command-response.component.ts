import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { RunCommandResponse } from 'src/app/nlp-api';

@Component({
  selector: 'command-response',
  templateUrl: './command-response.component.html',
  styleUrls: ['./command-response.component.scss']
})
export class CommandResponseComponent implements OnInit, OnChanges {
  @Input('response') response?: RunCommandResponse = undefined;

  shouldRender: boolean = false;
  wrapperClass: string = 'alert alert-info';
  wasSuccess: boolean = false;
  messageCount: number = 0;
  hasArgs: boolean = false;

  constructor() { }
  
  ngOnInit(): void { }

  ngOnChanges(_: SimpleChanges): void {
    this.shouldRender = (this?.response ?? undefined) !== undefined;
    this.wasSuccess = false;
    this.messageCount = 0;
    this.hasArgs = false;

    if(!this.shouldRender) { return; }

    this._setClass();
    this._setMessageCount();
    this.hasArgs = (this?.response?.args?.length ?? 0) > 0;
  }

  public close = () => {
    this.shouldRender = false;
  }

  private _setClass = () => {
    this.wasSuccess = this?.response?.success ?? false;
    this.wrapperClass = this.wasSuccess ? 'alert alert-info' : 'alert alert-danger';
  }

  private _setMessageCount = () => {
    this.messageCount = this?.response?.messages?.length ?? 0;
  }
}
