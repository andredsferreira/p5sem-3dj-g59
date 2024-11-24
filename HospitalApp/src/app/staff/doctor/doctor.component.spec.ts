import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { DoctorComponent } from './doctor.component';
import { OperationRequestService } from '../../operation-request/operation-request.service';
import { RequestStatus } from '../../operation-request/request-status.enum';
import { RequestPriority } from '../../operation-request/request-priority.enum';

describe('DoctorComponent', () => {
  let component: DoctorComponent;
  let fixture: ComponentFixture<DoctorComponent>;
  let mockOperationRequestService: jasmine.SpyObj<OperationRequestService>;

  beforeEach(async () => {
    mockOperationRequestService = jasmine.createSpyObj('OperationRequestService', [
      'createOperationRequest',
      'updateOperationRequest',
      'deleteOperationRequest',
      'getOperationRequests',
    ]);
  
    await TestBed.configureTestingModule({
      imports: [DoctorComponent], // Import the standalone component
      providers: [{ provide: OperationRequestService, useValue: mockOperationRequestService }],
    }).compileComponents();
  
    fixture = TestBed.createComponent(DoctorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  describe('Form Initialization', () => {
    it('should initialize operationRequestForm with default values', () => {
      const form = component.operationRequestForm;
      expect(form.get('patientId')?.value).toBe('');
      expect(form.get('operationTypeId')?.value).toBe('');
      expect(form.get('priority')?.value).toBe(RequestPriority.Elective);
      expect(form.get('requestStatus')?.value).toBe(RequestStatus.Pending);
      expect(form.valid).toBeFalse();
    });
  });

  describe('CRUD Operations', () => {
    it('should call createOperationRequest and refresh the list on successful form submission', async () => {
      mockOperationRequestService.createOperationRequest.and.returnValue(Promise.resolve());
      mockOperationRequestService.getOperationRequests.and.returnValue(Promise.resolve([]));

      component.operationRequestForm.patchValue({
        patientId: '123',
        operationTypeId: '456',
        priority: RequestPriority.Elective,
        dateTime: '2023-12-01T12:00',
        requestStatus: RequestStatus.Pending,
      });

      await component.onSubmit();

      expect(mockOperationRequestService.createOperationRequest).toHaveBeenCalledWith(
        '123',
        '456',
        RequestPriority.Elective,
        '2023-12-01T12:00',
        RequestStatus.Pending
      );
      expect(mockOperationRequestService.getOperationRequests).toHaveBeenCalled();
      expect(component.showModal).toBeFalse();
    });

    it('should handle errors during createOperationRequest', async () => {
      mockOperationRequestService.createOperationRequest.and.returnValue(
        Promise.reject(new Error('Create failed'))
      );

      component.operationRequestForm.patchValue({
        patientId: '123',
        operationTypeId: '456',
        priority: RequestPriority.Elective,
        dateTime: '2023-12-01T12:00',
        requestStatus: RequestStatus.Pending,
      });

      await component.onSubmit();

      expect(component.formError).toBe(
        'Failed to create operation request. Please check your input and try again.'
      );
    });

    it('should update operation request and refresh the list', async () => {
      const mockRequest = { operationRequestId: '1', priority: 'High', dateTime: '2023-12-01T12:00' };
      mockOperationRequestService.updateOperationRequest.and.returnValue(Promise.resolve());
      mockOperationRequestService.getOperationRequests.and.returnValue(Promise.resolve([]));

      component.selectedRequest = mockRequest;
      component.updateRequestForm.patchValue({
        priority: 'Low',
        dateTime: '2023-12-05T12:00',
      });

      await component.onUpdateSubmit();

      expect(mockOperationRequestService.updateOperationRequest).toHaveBeenCalledWith(
        '1',
        'Low',
        '2023-12-05T12:00'
      );
      expect(mockOperationRequestService.getOperationRequests).toHaveBeenCalled();
    });

    it('should delete operation request and refresh the list', async () => {
      mockOperationRequestService.deleteOperationRequest.and.returnValue(Promise.resolve());
      mockOperationRequestService.getOperationRequests.and.returnValue(Promise.resolve([]));

      component.deleteOperationRequest('1');
      await component.confirmDelete();

      expect(mockOperationRequestService.deleteOperationRequest).toHaveBeenCalledWith('1');
      expect(mockOperationRequestService.getOperationRequests).toHaveBeenCalled();
    });
  });

  describe('Filtering', () => {
    it('should filter requests based on filterForm values', () => {
      component.operationRequests = [
        { patientName: 'John Doe', priority: 'High' },
        { patientName: 'Jane Smith', priority: 'Low' },
      ];

      component.filterForm.patchValue({
        filterCriteria: 'patientName',
        filterValue: 'Jane',
      });

      component.filterRequests();
      expect(component.filteredRequests).toEqual([{ patientName: 'Jane Smith', priority: 'Low' }]);
    });
  });
});
