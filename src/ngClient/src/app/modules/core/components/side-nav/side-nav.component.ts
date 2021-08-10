import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {
  @Output() closeSidenav = new EventEmitter<void>();
  loggedIn: boolean = false;

  constructor() { }

  ngOnInit(): void { }

  logout = () => { }

  onClose = () => {
    this.closeSidenav.emit();
  }

  toggle = () => { }

}
