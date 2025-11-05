import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TriageFormComponent } from './triage-form.component';

describe('TriageFormComponent', () => {
  let component: TriageFormComponent;
  let fixture: ComponentFixture<TriageFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TriageFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TriageFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
