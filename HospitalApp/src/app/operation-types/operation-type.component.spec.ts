import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationTypeComponent } from './operation-type.component';
import { OperationTypeService } from './operation-type.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { path } from '../app.config';
import { API_PATH } from '../config-path';
import { Status } from './status.enum';
import { Specialization } from './specialization.enum';
import { HttpClient, HttpClientModule, HttpRequest, HttpResponse } from '@angular/common/http';
import { mock } from 'node:test';
import exp from 'constants';


interface OperationType {
  id: string;
  name: string;
  anaesthesiaTime: number;
  surgeryTime: number;
  cleaningTime: number;
  minDoctor: number;
  minAnaesthetist: number;
  minNurseAnaesthetist: number;
  minInstrumentingNurse: number;
  minCirculatingNurse: number;
  minXRayTechnician: number;
  minMedicalActionAssistant: number;
  Status: Status;
  Specialization: Specialization;
}
describe('OperationTypeComponent', () => {

  let component: OperationTypeComponent;
  let fixture: ComponentFixture<OperationTypeComponent>;
  let operationType: OperationType = {
    id: '1',
    name: 'Test Operation',
    anaesthesiaTime: 10,
    surgeryTime: 20,
    cleaningTime: 30,
    minDoctor: 1,
    minAnaesthetist: 1,
    minNurseAnaesthetist: 1,
    minInstrumentingNurse: 1,
    minCirculatingNurse: 1,
    minXRayTechnician: 1,
    minMedicalActionAssistant: 1,
    Status: Status.ACTIVE,
    Specialization: Specialization.Prosthetics

  };

  let mockOperationTypeService: jasmine.SpyObj<OperationTypeService>;

  beforeEach(async () => {
    mockOperationTypeService = jasmine.createSpyObj('OperationTypeService', ['createOperationType', 'editOperationType', 'deleteOperationType', 'getOperationTypes']);
    await TestBed.configureTestingModule({

      imports: [OperationTypeComponent, HttpClientTestingModule],
      providers: [
        { provide: OperationTypeService, useValue: mockOperationTypeService },
        { provide: API_PATH, useValue: path },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(OperationTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('Creation', () => {
    it('should successfully create an operation type', async () => {
      const createAttributes: OperationType = {
        id: '2',
        name: 'Test Operation',
        anaesthesiaTime: 10,
        surgeryTime: 20,
        cleaningTime: 30,
        minDoctor: 1,
        minAnaesthetist: 1,
        minNurseAnaesthetist: 1,
        minInstrumentingNurse: 1,
        minCirculatingNurse: 1,
        minXRayTechnician: 1,
        minMedicalActionAssistant: 1,
        Status: Status.ACTIVE,
        Specialization: Specialization.Prosthetics
      };
      let mockResponse: HttpResponse<OperationType> = new HttpResponse();
      mockOperationTypeService.createOperationType().and.returnValue(Promise.resolve(mockResponse));

      component.createFields['name'].value = createAttributes.name;
      component.createFields['anaesthesiaTime'].value = createAttributes.anaesthesiaTime;
      component.createFields['surgeryTime'].value = createAttributes.surgeryTime;
      component.createFields['cleaningTime'].value = createAttributes.cleaningTime;
      component.createFields['minDoctor'].value = createAttributes.minDoctor;
      component.createFields['minAnaesthetist'].value = createAttributes.minAnaesthetist;
      component.createFields['minNurseAnaesthetist'].value = createAttributes.minNurseAnaesthetist;
      component.createFields['minInstrumentingNurse'].value = createAttributes.minInstrumentingNurse;
      component.createFields['minCirculatingNurse'].value = createAttributes.minCirculatingNurse;
      component.createFields['minXRayTechnician'].value = createAttributes.minXRayTechnician;
      component.createFields['minMedicalActionAssistant'].value = createAttributes.minMedicalActionAssistant;
      component.createFields['Status'].value = createAttributes.Status;
      component.createFields['Specialization'].value = createAttributes.Specialization;

      await component.onSubmit();
      expect(mockOperationTypeService.createOperationType).toHaveBeenCalled();
    });
  })
  describe('Deletion', () => {
    it('should successfully delete an operation type', async () => {
      let mockResponse: HttpResponse<OperationType> = new HttpResponse();
      mockOperationTypeService.deleteOperationType().and.returnValue(Promise.resolve(mockResponse));
      await component.deleteOperationType(operationType.id);
      expect(mockOperationTypeService.deleteOperationType).toHaveBeenCalled();
    });

  })
  describe('Edit', () => {
    it('should successfully edit an operation type', async () => {
      const editAttributes: OperationType = {
        id: '2',
        name: 'Test Operation',
        anaesthesiaTime: 10,
        surgeryTime: 20,
        cleaningTime: 30,
        minDoctor: 1,
        minAnaesthetist: 1,
        minNurseAnaesthetist: 1,
        minInstrumentingNurse: 1,
        minCirculatingNurse: 1,
        minXRayTechnician: 1,
        minMedicalActionAssistant: 1,
        Status: Status.ACTIVE,
        Specialization: Specialization.Prosthetics
      };
      let mockResponse: HttpResponse<OperationType> = new HttpResponse();
      mockOperationTypeService.updateOperationType().and.returnValue(Promise.resolve(mockResponse));

      component.editingFields['name'].value = editAttributes.name;
      component.editingFields['anaesthesiaTime'].value = editAttributes.anaesthesiaTime;
      component.editingFields['surgeryTime'].value = editAttributes.surgeryTime;
      component.editingFields['cleaningTime'].value = editAttributes.cleaningTime;
      component.editingFields['minDoctor'].value = editAttributes.minDoctor;
      component.editingFields['minAnaesthetist'].value = editAttributes.minAnaesthetist;
      component.editingFields['minNurseAnaesthetist'].value = editAttributes.minNurseAnaesthetist;
      component.editingFields['minInstrumentingNurse'].value = editAttributes.minInstrumentingNurse;
      component.editingFields['minCirculatingNurse'].value = editAttributes.minCirculatingNurse;
      component.editingFields['minXRayTechnician'].value = editAttributes.minXRayTechnician;
      component.editingFields['minMedicalActionAssistant'].value = editAttributes.minMedicalActionAssistant;
      component.editingFields['Status'].value = editAttributes.Status;
      component.editingFields['Specialization'].value = editAttributes.Specialization;

      await component.onSubmit();
      expect(mockOperationTypeService.updateOperationType().toHaveBeenCalled());
    });
  })
});
