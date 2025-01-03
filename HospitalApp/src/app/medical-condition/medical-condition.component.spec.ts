import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalConditionComponent } from './medical-condition.component';
import { HttpClientModule } from '@angular/common/http';

describe('StaffManagement', () => {  
  let component: MedicalConditionComponent;
  let fixture: ComponentFixture<MedicalConditionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalConditionComponent, HttpClientModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicalConditionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
