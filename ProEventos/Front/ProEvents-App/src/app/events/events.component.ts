import { Component, TemplateRef } from '@angular/core';
import { EventService } from '../services/event.service';
import { Event } from '../models/Event';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss'],
  //providers: [EventService]
})
export class EventsComponent {

  constructor(
    private eventService: EventService,
    private modalService: BsModalService

    )
  {
  }

  modalRef?: BsModalRef;
  public events: Event[] = [];
  public eventsFiltereds : Event[] = [];

  public widthImg: number = 150;
  public marginImg: number = 2;
  public showImage = true;
  private _filterList : string = '';

  public get filterList(): string {
    return this._filterList;
  }

  public set filterList(value : string) {
    this._filterList = value;
    this.eventsFiltereds = this._filterList ? this.filterEvents(this.filterList) : this.events
  }

  public filterEvents(filterBy: string): Event[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter(
      (event: any) => event.theme.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      event.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  public ngOnInit(): void {
    this.getEvents();
  }

  public getEvents(): void {
    this.eventService.getEvents().subscribe({
      next: (eventResponse: Event[]) => {
        this.events = eventResponse;
        this.eventsFiltereds = this.events;
      },
      error: (error: any) => console.log(error)
    });
  }

  public changeImageState(): void {
    this.showImage = !this.showImage;
  }
  openModal(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef?.hide();
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
