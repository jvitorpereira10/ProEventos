import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Event } from '../models/Event';

@Injectable(
  // { providedIn: 'root'}
  )
export class EventService {
baseURL = 'https://localhost:5001/api/events';

constructor(private http: HttpClient) { }

  public getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseURL);
  }

  public getEventsByTheme(theme: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseURL}/theme/${theme}`);
  }

  public getEventById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.baseURL}/${id}`);
  }
}
