import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserStorySprintAddDialogComponent } from './user-story-sprint-add-dialog.component';

describe('UserStorySprintAddDialogComponent', () => {
  let component: UserStorySprintAddDialogComponent;
  let fixture: ComponentFixture<UserStorySprintAddDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserStorySprintAddDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserStorySprintAddDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
