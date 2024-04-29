import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FunctionalitySprintAddDialogComponent } from './functionality-sprint-add-dialog.component';


describe('FunctionalitySprintAddDialogComponent', () => {
  let component: FunctionalitySprintAddDialogComponent;
  let fixture: ComponentFixture<FunctionalitySprintAddDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FunctionalitySprintAddDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FunctionalitySprintAddDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
