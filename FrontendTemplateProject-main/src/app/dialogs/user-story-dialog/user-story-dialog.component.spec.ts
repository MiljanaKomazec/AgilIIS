import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserStoryDialogComponent } from './user-story-dialog.component';

describe('UserStoryDialogComponent', () => {
  let component: UserStoryDialogComponent;
  let fixture: ComponentFixture<UserStoryDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserStoryDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserStoryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
