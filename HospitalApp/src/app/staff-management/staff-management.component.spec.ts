import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffManagement } from './staff-management.component';
import { HttpClientModule } from '@angular/common/http';

describe('StaffManagement', () => {  
  let component: StaffManagement;
  let fixture: ComponentFixture<StaffManagement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StaffManagement, HttpClientModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffManagement);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
