import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskSprintAddDialogComponent } from './task-sprint-add-dialog.component';


describe('TaskSprintAddDialogComponent', () => {
  let component: TaskSprintAddDialogComponent;
  let fixture: ComponentFixture<TaskSprintAddDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskSprintAddDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TaskSprintAddDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
