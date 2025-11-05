import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClinicalCareComponent } from './clinical-care.component';

describe('ClinicalCareComponent', () => {
  let component: ClinicalCareComponent;
  let fixture: ComponentFixture<ClinicalCareComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClinicalCareComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClinicalCareComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
