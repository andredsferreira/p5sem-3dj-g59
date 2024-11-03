import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalFloorComponent } from './hospitalfloor.component';

describe('HospitalFloorComponent', () => {
  let component: HospitalFloorComponent;
  let fixture: ComponentFixture<HospitalFloorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HospitalFloorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HospitalFloorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
