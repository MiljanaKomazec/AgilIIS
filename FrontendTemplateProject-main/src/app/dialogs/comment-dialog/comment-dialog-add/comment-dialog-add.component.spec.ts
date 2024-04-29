import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommentDialogAddComponent } from './comment-dialog-add.component';

describe('CommentDialogAddComponent', () => {
  let component: CommentDialogAddComponent;
  let fixture: ComponentFixture<CommentDialogAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommentDialogAddComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CommentDialogAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
