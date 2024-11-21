import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationTypeComponent } from './operation-type.component';
import { OperationTypeService } from './operation-type.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { path } from '../app.config';
import { API_PATH } from '../config-path';

describe('OperationTypeComponent', () => {

    let component: OperationTypeComponent;
    let fixture: ComponentFixture<OperationTypeComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [OperationTypeComponent, HttpClientTestingModule],
            providers: [
              { provide: OperationTypeService, useValue: OperationTypeService },
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

});
