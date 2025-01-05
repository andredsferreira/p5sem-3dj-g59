import { expect } from 'chai';
import MedConditionService from '../../../src/services/medConditionService';
import { HttpClient } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import sinon from 'sinon';

describe('MedConditionService', () => {
  let medConditionService: MedConditionService;
  let httpClientMock: sinon.SinonStubbedInstance<HttpClient>;

  beforeEach(() => {
    httpClientMock = sinon.createStubInstance(HttpClient);
    medConditionService = new MedConditionService(httpClientMock as any);
  });

  afterEach(() => {
    sinon.restore();
  });

  describe('createMedCondition', () => {
    it('should create a new condition', async () => {
      // Arrange
      const condition = {
        code: 'C001',
        designation: 'Condition 1',
        description: 'Description of condition 1',
        symptoms: ['Symptom 1', 'Symptom 2'],
      };
      const mockResponse = { message: 'Condition created successfully' };

      httpClientMock.post.resolves(mockResponse);

      // Act
      const result = await medConditionService.createMedCondition(condition);

      // Assert
      expect(result).to.deep.equal(mockResponse);
      sinon.assert.calledOnceWithExactly(httpClientMock.post, 'http://localhost:4000/api/medConditions', condition, sinon.match.object);
    });

    it('should handle error when creating a condition', async () => {
      // Arrange
      const condition = {
        code: 'C001',
        designation: 'Condition 1',
        description: 'Description of condition 1',
        symptoms: ['Symptom 1', 'Symptom 2'],
      };
      const mockError = new Error('Error creating condition');
      httpClientMock.post.rejects(mockError);

      // Act & Assert
      try {
        await medConditionService.createMedCondition(condition);
        throw new Error('Expected error to be thrown');
      } catch (error) {
        expect(error).to.equal(mockError);
      }
    });
  });

  describe('updateMedCondition', () => {
    it('should update a condition', async () => {
      // Arrange
      const code = 'C001';
      const condition = {
        code: 'C001',
        designation: 'Updated Condition',
        description: 'Updated description',
        symptoms: ['Updated symptom'],
      };
      const mockResponse = { message: 'Condition updated successfully' };

      httpClientMock.patch.resolves(mockResponse);

      // Act
      const result = await medConditionService.updateMedCondition(condition);

      // Assert
      expect(result).to.deep.equal(mockResponse);
      sinon.assert.calledOnceWithExactly(httpClientMock.patch, `http://localhost:4000/api/medConditions/${code}`, condition, sinon.match.object);
    });

    it('should handle error when updating a condition', async () => {
      // Arrange
      const code = 'C001';
      const condition = {
        code: 'C001',
        designation: 'Updated Condition',
        description: 'Updated description',
        symptoms: ['Updated symptom'],
      };
      const mockError = new Error('Error updating condition');
      httpClientMock.patch.rejects(mockError);

      // Act & Assert
      try {
        await medConditionService.updateMedCondition(condition);
        throw new Error('Expected error to be thrown');
      } catch (error) {
        expect(error).to.equal(mockError);
      }
    });
  });

  describe('getAllMedConditions', () => {
    it('should fetch all conditions', async () => {
      // Arrange
      const mockConditions = [
        { code: 'C001', designation: 'Condition 1', description: 'Description 1', symptoms: ['Symptom 1'] },
        { code: 'C002', designation: 'Condition 2', description: 'Description 2', symptoms: ['Symptom 2'] },
      ];

      httpClientMock.get.resolves(mockConditions);

      // Act
      const result = await medConditionService.getAllMedConditions();

      // Assert
      expect(result).to.deep.equal(mockConditions);
      sinon.assert.calledOnceWithExactly(httpClientMock.get, 'http://localhost:4000/api/medConditions', sinon.match.object);
    });

    it('should handle error when fetching conditions', async () => {
      // Arrange
      const mockError = new Error('Error fetching conditions');
      httpClientMock.get.rejects(mockError);

      // Act & Assert
      try {
        await medConditionService.getAllMedConditions();
        throw new Error('Expected error to be thrown');
      } catch (error) {
        expect(error).to.equal(mockError);
      }
    });
  });
});

