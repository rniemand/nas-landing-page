import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ProjectsClient } from 'src/app/nlp-api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private _projectsClient: ProjectsClient) {
    // http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
    //   this.forecasts = result;
    // }, error => console.error(error));

    console.log(this._projectsClient);
  }
}
