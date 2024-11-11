// patient.service.spec.ts
import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PatientService } from './patient-service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { path } from '../app.config';
import { API_PATH } from '../config-path';

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

  it('should retrieve patients list', async () => {
    const token = 'test-token';
    const mockResponse = [
      { MedicalRecordNumber: '123', name: 'Patient 123', email: 'patient123@example.com' },
      { MedicalRecordNumber: '456', name: 'Patient 456', email: 'patient456@example.com' }
    ];
    const searchAttributes = { FullName: 'John Doe' };

    service.getPatients(token, searchAttributes).then((patients) => {
      expect(patients).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${path}/Patient/Search`);
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization')).toBe(`Bearer ${token}`);
    expect(req.request.body).toEqual(searchAttributes);
    req.flush(mockResponse);
  });

  it('should edit an existing patient', async () => {
    const token = 'test-token';
    const MedicalRecordNumber = '123';
    const attributes = { Email: 'newemail@example.com', Allergies: 'Peanuts' };
    const mockResponse = { success: true };

    service.editPatient(token, MedicalRecordNumber, attributes).then((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${path}/Patient/Edit/${MedicalRecordNumber}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.headers.get('Authorization')).toBe(`Bearer ${token}`);
    expect(req.request.body).toEqual(attributes);
    req.flush(mockResponse);
  });

  it('should delete a patient', async () => {
    const token = 'test-token';
    const MedicalRecordNumber = '123';
    const mockResponse = { success: true };

    service.deletePatient(token, MedicalRecordNumber).then((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${path}/Patient/Delete/${MedicalRecordNumber}`);
    expect(req.request.method).toBe('DELETE');
    expect(req.request.headers.get('Authorization')).toBe(`Bearer ${token}`);
    req.flush(mockResponse);
  });

  it('should return undefined if token is null for getPatients', async () => {
    const result = await service.getPatients(null, {});
    expect(result).toBeUndefined();
  });

  it('should return undefined if token is null for editPatient', async () => {
    const result = await service.editPatient(null, '123', {});
    expect(result).toBeUndefined();
  });

  it('should return undefined if token is null for deletePatient', async () => {
    const result = await service.deletePatient(null, '123');
    expect(result).toBeUndefined();
  });
});
