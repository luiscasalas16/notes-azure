import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'angular-static-web-app';

  //develpment
  //private apiUrl: string = 'http://localhost:5000/api';
  //production app service not linked to static web app
  //private apiUrl: string = 'https://lcs16-swa-as.azurewebsites.net/api';
  //production app service linked to static web app
  private apiUrl: string = '/api';

  public results: any[] = [];

  constructor(private http: HttpClient) {}

  getApi() {
    let endpointUrl = `${this.apiUrl}/weatherforecast`;

    this.http.get<any>(endpointUrl).subscribe((resp) => {
      console.log(endpointUrl);
      this.results = resp;
    });
  }
}
