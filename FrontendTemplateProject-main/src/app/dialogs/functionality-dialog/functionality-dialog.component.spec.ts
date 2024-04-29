import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalityDialogComponent } from './functionality-dialog.component';

describe('FunctionalityDialogComponent', () => {
  let component: FunctionalityDialogComponent;
  let fixture: ComponentFixture<FunctionalityDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FunctionalityDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FunctionalityDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
