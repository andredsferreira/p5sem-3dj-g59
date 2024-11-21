// patient.service.spec.ts
import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PatientService } from '../patient-service';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { path } from '../../app.config';
import { API_PATH } from '../../config-path';
import { Patient, PatientCreateAttributes, PatientEditAttributes, PatientSearchAttributes } from '../patient-types';

describe('PatientService', () => {
  let service: PatientService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],  // Use HttpClientTestingModule instead of HttpClientModule
      providers: [
        PatientService,
        { provide: API_PATH, useValue: path }  // Provide a mock API_PATH
      ]
    });
    service = TestBed.inject(PatientService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  //------------------------------------------------Search-----------------------------------------------------------------

  it('should retrieve patients list', async () => {
    const at : PatientSearchAttributes = {FullName: 'John Doe'}; 
    const token = 'test-token';
    const mockResponse: Patient[] = [
      { MedicalRecordNumber: '202411000001', FullName: 'John Doe', Email: 'teste1@gmail.com', PhoneNumber: '912834756', Gender: 'Male', Allergies: 'Gatos', DateOfBirth: new Date('20041010') },
      { MedicalRecordNumber: '202411000002', FullName: 'John Doe', Email: 'teste2@gmail.com', PhoneNumber: '921843765', Gender: 'Male', Allergies: 'Fungos', DateOfBirth: new Date('20041111') },
    ];

    service.getPatients(token, at).then((patients) => {
      expect(patients.body).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${path}/Patient/Search`);
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization')).toBe(`Bearer ${token}`);
    expect(req.request.body).toEqual(at);
    req.flush(mockResponse);
  });

  //------------------------------------------------Create-----------------------------------------------------------------

  it('should create a new patient locally', () => {
    const token = 'test-token';
    const newPatient: PatientCreateAttributes = { 
      FullName: 'Patient 3', 
      Email: 'patient3@example.com', 
      PhoneNumber: '932923093', 
      Allergies: 'Gatos', 
      DateOfBirth: new Date('20041010'),
      Gender: "Male" 
    };
  
    const mockPatient: Patient = {
      MedicalRecordNumber: '202411000001',
      FullName: 'Patient 3', 
      Email: 'patient3@example.com', 
      PhoneNumber: '932923093', 
      Allergies: 'Gatos', 
      DateOfBirth: new Date('20041010'),
      Gender: "Male" 
    };
  
    service.createPatient(token, newPatient).then((response) => {
        expect(response.body).toEqual(mockPatient);
    });
  
    const req = httpMock.expectOne(`${path}/Patient/Create`);
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization')).toBe(`Bearer ${token}`);
    req.flush(mockPatient);  // Use mockPatient instead of wrapping in HttpResponse
  });
  

  //------------------------------------------------Edit-----------------------------------------------------------------

  it('should edit an existing patient', async () => {
    const token = 'test-token';
    const MedicalRecordNumber = '202411000001';
    const attributes: PatientEditAttributes = { 
      Email: 'newemail@example.com', 
      Allergies: 'Peanuts' 
    };
  
    const mockPatient: Patient = {
      MedicalRecordNumber: MedicalRecordNumber,
      FullName: 'Test Edit',
      Email: 'newemail@example.com',
      PhoneNumber: '912834756',
      Allergies: 'Peanuts',
      DateOfBirth: new Date('20041010'),
      Gender: 'Male'
    };
  
    service.editPatient(token, MedicalRecordNumber, attributes).then((response) => {
      expect(response.body).toEqual(mockPatient);
    });
  
    const req = httpMock.expectOne(`${path}/Patient/Edit/${MedicalRecordNumber}`);
    expect(req.request.method).toBe('PATCH');
    expect(req.request.headers.get('Authorization')).toBe(`Bearer ${token}`);
    req.flush(mockPatient);
  });
  

  //------------------------------------------------Delete-----------------------------------------------------------------

  it('should delete a patient', async () => {
    const token = 'test-token';
    const MedicalRecordNumber = '202411000001';
    const mockPatient: Patient = {
      MedicalRecordNumber: MedicalRecordNumber,
      FullName: 'Test Edit',
      Email: 'newemail@example.com',
      PhoneNumber: '912834756',
      Allergies: 'Peanuts',
      DateOfBirth: new Date('20041010'),
      Gender: 'Male'
    };
  
    service.deletePatient(token, MedicalRecordNumber).then((response) => {
      expect(response.body).toEqual(mockPatient);
    });
  
    const req = httpMock.expectOne(`${path}/Patient/Delete/${MedicalRecordNumber}`);
    expect(req.request.method).toBe('DELETE');
    expect(req.request.headers.get('Authorization')).toBe(`Bearer ${token}`);
    req.flush(mockPatient);
  });
  

  it('should throw an error if token is null for getPatients', async () => {
    try {
      await service.getPatients(null, {});
      fail('Expected an error to be thrown');
    } catch (error) {
      expect(error).toEqual(new Error("Token is required"));
    }
  });
  
  it('should throw an error if token is null for editPatient', async () => {
    try {
      await service.editPatient(null, '123', {});
      fail('Expected an error to be thrown');
    } catch (error) {
      expect(error).toEqual(new Error("Token is required"));
    }
  });
  
  it('should throw an error if token is null for deletePatient', async () => {
    try {
      await service.deletePatient(null, '123');
      fail('Expected an error to be thrown');
    } catch (error) {
      expect(error).toEqual(new Error("Token is required"));
    }
  });
  
});
