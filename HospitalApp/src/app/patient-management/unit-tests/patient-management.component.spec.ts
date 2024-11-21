import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientManagementComponent } from '../patient-management.component';
import { HttpClient, HttpClientModule, HttpRequest, HttpResponse } from '@angular/common/http';
import { API_PATH } from '../../config-path';
import { path } from '../../app.config';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Patient, PatientCreateAttributes, PatientEditAttributes } from '../patient-types';
import { PatientService } from '../patient-service';

describe('PatientManagementComponent', () => {
  let component: PatientManagementComponent;
  let fixture: ComponentFixture<PatientManagementComponent>;
  let patient : Patient = {
    MedicalRecordNumber: '202411000001',
    FullName: 'Test Subject',
    Email: 'teste@gmail.com',
    PhoneNumber: '933922900',
    DateOfBirth: '20041020',
    Gender: 'Male',
    Allergies: 'Cats, Dogs, Fungus'
  };

  let mockPatientService: jasmine.SpyObj<PatientService>;

  beforeEach(async () => {
    mockPatientService = jasmine.createSpyObj('PatientService', ['createPatient', 'editPatient']);

    await TestBed.configureTestingModule({
      imports: [PatientManagementComponent, HttpClientTestingModule],
      providers: [
        { provide: PatientService, useValue: mockPatientService },
        { provide: API_PATH, useValue: path },
      ],
    }).compileComponents();

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
  describe('Edit', () => {
    it('should successfully edit a patient', async () => {
      const editAttributes: PatientEditAttributes = {
        Email: 'test@example.com',
        FullName: 'Test User',
      };
      let mockResponse: HttpResponse<Patient> = new HttpResponse();
      mockPatientService.editPatient.and.returnValue(Promise.resolve(mockResponse));
      
      if(editAttributes.Email != undefined) component.editingFields['Email'].value = editAttributes.Email;
      if(editAttributes.FullName != undefined) component.editingFields['FullName'].value = editAttributes.FullName;
      
      await component.submitEdit(patient);

      expect(component.messageText).toContain('editado com sucesso!');
      expect(component.messageClass).toContain('bg-green-500');
    });

    it('shouldnt edit non existent patient', async () => {
      const editAttributes: PatientEditAttributes = {
        Email: 'test@example.com',
        FullName: 'Test User',
      };
      let mockResponse: HttpResponse<Patient> = new HttpResponse();
      mockPatientService.editPatient.and.returnValue(Promise.resolve(mockResponse));
      
      if(editAttributes.Email != undefined) component.editingFields['Email'].value = editAttributes.Email;
      if(editAttributes.FullName != undefined) component.editingFields['FullName'].value = editAttributes.FullName;
      
      await component.submitEdit(null);

      expect(component.messageText).toBe('');
      expect(component.messageClass).toBe('');
      //"patient does not exist" was logged 
    });
  })
});
