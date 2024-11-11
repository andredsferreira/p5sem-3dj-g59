import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientManagementComponent } from './patient-management.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';

describe('PatientManagementComponent', () => {
  let component: PatientManagementComponent;
  let fixture: ComponentFixture<PatientManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientManagementComponent, HttpClientModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
