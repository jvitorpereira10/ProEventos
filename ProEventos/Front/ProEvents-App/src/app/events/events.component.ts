import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent {

  constructor(private http: HttpClient)
  {
  }

  public events: any = [];
  widthImg: number = 150;
  marginImg: number = 2;
  showImage = true;

  ngOnInit() {
    this.getEvents();
  }

  public getEvents(): void {
    this.http.get('https://localhost:5001/api/events').subscribe(
      response => this.events = response,
      error => console.log(error),
    );
  }

  public changeImageState(): void{
    this.showImage = !this.showImage;
  }

}
