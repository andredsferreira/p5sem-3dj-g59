import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientManagementComponent } from '../patient-management.component';
import { HttpClient, HttpClientModule, HttpRequest, HttpResponse } from '@angular/common/http';
import { API_PATH } from '../../config-path';
import { path } from '../../app.config';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Patient, PatientCreateAttributes } from '../patient-types';
import { PatientService } from '../patient-service';

describe('PatientManagementComponent', () => {
  let component: PatientManagementComponent;
  let fixture: ComponentFixture<PatientManagementComponent>;
  let mockToken = 'mock-token';
  let patient : Patient;

  let mockPatientService: jasmine.SpyObj<PatientService>;

  beforeEach(async () => {
    mockPatientService = jasmine.createSpyObj('PatientService', ['createPatient']);

    await TestBed.configureTestingModule({
      imports: [PatientManagementComponent, HttpClientTestingModule],
      providers: [
        { provide: PatientService, useValue: mockPatientService },
        { provide: API_PATH, useValue: path },
      ],
    }).compileComponents();

    localStorage.setItem('token',mockToken);
    fixture = TestBed.createComponent(PatientManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it('should create', () => {
    expect(component).toBeTruthy();
  });
  describe('Creation', () => {
    it('should successfully create a patient', async () => {
      const createAttributes: PatientCreateAttributes = {
        Email: 'test@example.com',
        PhoneNumber: '123456789',
        FullName: 'Test User',
        DateOfBirth: '2000-01-01',
        Gender: 'Male',
        Allergies: 'Peanuts, Shellfish',
      };
      let mockResponse: HttpResponse<Patient> = new HttpResponse();
      mockPatientService.createPatient.and.returnValue(Promise.resolve(mockResponse));
      
      component.createFields['Email'].value = createAttributes.Email;
      component.createFields['PhoneNumber'].value = createAttributes.PhoneNumber;
      component.createFields['FullName'].value = createAttributes.FullName;
      component.createFields['DateOfBirth'].value = 
      typeof createAttributes.DateOfBirth === 'string' 
        ? createAttributes.DateOfBirth 
        : createAttributes.DateOfBirth.toISOString(); // Or use any preferred date format
          component.createFields['Gender'].value = createAttributes.Gender;
      component.allergiesList = ['Peanuts', 'Shellfish'];

      await component.onCreate();

      expect(component.messageText).toContain('criado com sucesso!');
      expect(component.messageClass).toContain('bg-green-500');
    });
  })
});
