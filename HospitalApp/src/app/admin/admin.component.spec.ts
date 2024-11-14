import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminComponent } from './admin.component';
import { HttpClientModule } from '@angular/common/http';

describe('AdminComponent', () => {  
  let component: AdminComponent;
  let fixture: ComponentFixture<AdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminComponent, HttpClientModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
