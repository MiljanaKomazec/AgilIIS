import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventTypeDialogComponent } from './event-type-dialog.component';

describe('EventTypeDialogComponent', () => {
  let component: EventTypeDialogComponent;
  let fixture: ComponentFixture<EventTypeDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventTypeDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EventTypeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
