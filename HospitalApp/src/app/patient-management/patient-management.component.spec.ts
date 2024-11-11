import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientManagementComponent } from './patient-management.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { API_PATH } from '../config-path';
import { path } from '../app.config';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('PatientManagementComponent', () => {
  let component: PatientManagementComponent;
  let fixture: ComponentFixture<PatientManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientManagementComponent, HttpClientTestingModule],
      providers: [
        { provide: API_PATH, useValue: path }  // Provide a mock API_PATH
      ]
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
