import { Component, OnInit } from '@angular/core';
import { ClientConfig, ConfigClient, UserLink, UserLinksClient } from 'src/app/nlp-api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  clientConfig?: ClientConfig = undefined;
  
  constructor(private _config: ConfigClient, private _links: UserLinksClient) { }

  ngOnInit(): void {
    this._config.getClientConfig().toPromise().then(config => {
      console.log(config);
    });

    this._links.getAll().toPromise().then((links: UserLink[]) => {
      console.log(links);
    });
  }
}
