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
  public eventsFiltereds : any = [];
  widthImg: number = 150;
  marginImg: number = 2;
  showImage = true;
  private _filterList : string = '';

  public get filterList(): string {
    return this._filterList;
  }

  public set filterList(value : string) {
    this._filterList = value;
    this.eventsFiltereds = this._filterList ? this.filterEvents(this.filterList) : this.events
  }

  filterEvents(filterBy: string): any {
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter(
      (event: any) => event.theme.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
                      event.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  ngOnInit() {
    this.getEvents();
  }

  public getEvents(): void {
    this.http.get('https://localhost:5001/api/events').subscribe(
      response => {
        this.events = response,
        this.eventsFiltereds = this.events;

      },
      error => console.log(error),
    );
  }

  public changeImageState(): void{
    this.showImage = !this.showImage;
  }

}
