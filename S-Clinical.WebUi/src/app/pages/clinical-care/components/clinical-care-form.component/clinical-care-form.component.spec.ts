import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClinicalCareFormComponent } from './clinical-care-form.component';

describe('ClinicalCareFormComponent', () => {
  let component: ClinicalCareFormComponent;
  let fixture: ComponentFixture<ClinicalCareFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClinicalCareFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClinicalCareFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
