import sinon from 'sinon';
import * as mocha from 'mocha';
import { expect } from 'chai';
import { Response, Request, NextFunction } from 'express';
import { Container } from 'typedi';
import { Result } from '../src/core/logic/Result';
import { IFamilyHistoryEntryDTO } from '../src/dto/IFamilyHistoryEntryDTO';
import { FamilyHistoryEntry } from '../src/domain/familyHistoryEntry';
import IFamilyHistoryEntryService from '../src/services/IServices/IFamilyHistoryEntryService';
import FamilyHistoryEntryController from '../src/controllers/familyHistoryEntryController';
import IFamilyHistoryEntryRepo from '../src/services/IRepos/IFamilyHistoryEntryRepo';
import IMedicalRecordRepo from '../src/services/IRepos/IMedicalRecordRepo';
import { MedicalRecord } from '../src/domain/medicalRecord';

const timeout:number = 30000;

//-------------------------------SCHEMA--------------------------------------
const famHistorySchemaPath:string = "../src/persistence/schemas/familyHistoryEntrySchema";
const medicalRecordSchemaPath:string = "../src/persistence/schemas/medicalRecordSchema";

//--------------------------------REPO---------------------------------------
const medRecordRepoPath:string = "../src/repos/medicalRecordRepo";
const famHistoryRepoPath:string = "../src/repos/familyHistoryEntryRepo";

//--------------------------------SERVICE---------------------------------------
const famHistoryServicePath:string = "../src/services/familyHistoryEntryService";

mocha.describe('familyEntry controller', function () {
    const sandbox = sinon.createSandbox();

    mocha.beforeEach(function() {
        this.timeout(timeout);
        Container.reset();

        // FAMILY ENTRY

        let familyHistorySchemaInstance = require(famHistorySchemaPath).default;
        Container.set("familyHistoryEntrySchema", familyHistorySchemaInstance);

        let familyHistoryRepoClass = require(famHistoryRepoPath).default;
        let familyHistoryRepoInstance = Container.get(familyHistoryRepoClass);
        Container.set("FamilyHistoryEntryRepo", familyHistoryRepoInstance);

        // MEDICAL RECORD
        
        let medRecordSchemaInstance = require(medicalRecordSchemaPath ).default;
        Container.set("medicalRecordSchema", medRecordSchemaInstance);
    
        let medRecordRepoClass = require(medRecordRepoPath).default;
        let medRecordRepoInstance = Container.get(medRecordRepoClass);
        Container.set("MedicalRecordRepo", medRecordRepoInstance);

        // FAMILY HISTORY SERVICE (must be last)

        let familyHistoryServiceClass = require(famHistoryServicePath).default;
        let familyHistoryServiceInstance = Container.get(familyHistoryServiceClass);
        Container.set("FamilyHistoryEntryService", familyHistoryServiceInstance);
    });

    afterEach(function() {
        sandbox.restore();
    });

    mocha.it('familyHistoryEntryController create unit test using familyHistoryEntryService stub', async function () {
        // Arrange
        this.timeout(timeout);
        const body = { 
            "medicalRecordNumber": "202412000002",
            "relative": "Mãe",
            "history": "Asma, Osteoporose."
        };
    
        const req: Partial<Request> = { body };
        const jsonSpy = sinon.spy();
        const res: Partial<Response> = {
            json: jsonSpy, 
        };
        const next: Partial<NextFunction> = () => {};
    
        const familyHistoryEntryServiceInstance: IFamilyHistoryEntryService = Container.get("FamilyHistoryEntryService");
        sinon.stub(familyHistoryEntryServiceInstance, "createFamilyHistoryEntry").returns(
            Promise.resolve(
                Result.ok<IFamilyHistoryEntryDTO>({
                    entryNumber: "202412000002001", 
                    medicalRecordNumber: req.body.medicalRecordNumber,
                    relative: req.body.relative,
                    history: req.body.history,
                })
            )
        );
    
        const ctrl = new FamilyHistoryEntryController(familyHistoryEntryServiceInstance);
    
        // Act
        await ctrl.createFamilyHistoryEntry(req as Request, res as Response, next as NextFunction);
    
        // Assert
        sinon.assert.calledOnce(jsonSpy);
        sinon.assert.calledWith(jsonSpy, sinon.match({ 
            entryNumber: "202412000002001", 
            medicalRecordNumber: req.body.medicalRecordNumber,
            relative: req.body.relative,
            history: req.body.history
        }));
    });

    mocha.it('familyHistoryEntryController + familyHistoryEntryService create integration test using familyHistoryEntryRepository stub', async function () {	
        // Arrange	
        this.timeout(timeout);
        const body = { 
            "medicalRecordNumber": "202412000002",
            "relative": "Mãe",
            "history": "Asma, Osteoporose."
        };
        
        const req: Partial<Request> = { body };
        const jsonSpy = sinon.spy();
        const res: Partial<Response> = {
            json: jsonSpy, 
        };
        const next: Partial<NextFunction> = () => {};

        let medicalRecordRepoInstance:IMedicalRecordRepo = Container.get("MedicalRecordRepo");
        sinon.stub(medicalRecordRepoInstance, "findByMedicalRecordNumber").returns(Promise.resolve(MedicalRecord.create({medicalRecordNumber:"202412000002"}).getValue()));

        let familyHistoryEntryRepoInstance:IFamilyHistoryEntryRepo = Container.get("FamilyHistoryEntryRepo");
        sinon.stub(familyHistoryEntryRepoInstance, "findByMedicalRecordNumber").returns(Promise.resolve([]))
        sinon.stub(familyHistoryEntryRepoInstance, "save").returns(new Promise<FamilyHistoryEntry>((resolve, reject) => {
            resolve(FamilyHistoryEntry.create({
                entryNumber: "202412000002001", 
                medicalRecordNumber: req.body.medicalRecordNumber,
                relative: req.body.relative,
                history: req.body.history,
            }).getValue())
        }));

        let familyHistoryEntryServiceInstance = Container.get("FamilyHistoryEntryService");

        const ctrl = new FamilyHistoryEntryController(familyHistoryEntryServiceInstance as IFamilyHistoryEntryService);

        // Act
        await ctrl.createFamilyHistoryEntry(<Request>req, <Response>res, <NextFunction>next);

        // Assert
        sinon.assert.calledOnce(jsonSpy);
        sinon.assert.calledWith(jsonSpy, sinon.match({ 
            entryNumber: "202412000002001", 
            medicalRecordNumber: req.body.medicalRecordNumber,
            relative: req.body.relative,
            history: req.body.history
        }));
    });
    mocha.it('familyHistoryEntryController + familyHistoryEntryService integration test using spy on familyHistoryEntryService', async function () {		
        // Arrange
        this.timeout(timeout);
        const body = { 
            "medicalRecordNumber": "202412000002",
            "relative": "Mãe",
            "history": "Asma, Osteoporose."
        };
        
        const req: Partial<Request> = { body };
        const jsonSpy = sinon.spy();
        const res: Partial<Response> = {
            json: jsonSpy, 
        };
        const next: Partial<NextFunction> = () => {};

        let medicalRecordRepoInstance:IMedicalRecordRepo = Container.get("MedicalRecordRepo");
        sinon.stub(medicalRecordRepoInstance, "findByMedicalRecordNumber").returns(Promise.resolve(MedicalRecord.create({medicalRecordNumber:"202412000002"}).getValue()));

        let familyHistoryEntryRepoInstance:IFamilyHistoryEntryRepo = Container.get("FamilyHistoryEntryRepo");
        sinon.stub(familyHistoryEntryRepoInstance, "findByMedicalRecordNumber").returns(Promise.resolve([]))
        sinon.stub(familyHistoryEntryRepoInstance, "save").returns(new Promise<FamilyHistoryEntry>((resolve, reject) => {
            resolve(FamilyHistoryEntry.create({
                entryNumber: "202412000002001", 
                medicalRecordNumber: req.body.medicalRecordNumber,
                relative: req.body.relative,
                history: req.body.history,
            }).getValue())
        }));

        let familyHistoryEntryServiceInstance:IFamilyHistoryEntryService = Container.get("FamilyHistoryEntryService");

        const familyHistoryEntryServiceSpy = sinon.spy(familyHistoryEntryServiceInstance, "createFamilyHistoryEntry");

        const ctrl = new FamilyHistoryEntryController(familyHistoryEntryServiceInstance as IFamilyHistoryEntryService);
        // Act
        await ctrl.createFamilyHistoryEntry(<Request>req, <Response>res, <NextFunction>next);

        // Assert
        sinon.assert.calledOnce(jsonSpy);
        sinon.assert.calledWith(jsonSpy, sinon.match({ 
            "entryNumber":"202412000002001", 
            "medicalRecordNumber":req.body.medicalRecordNumber,
            "relative":req.body.relative,
            "history":req.body.history,
        }));
        sinon.assert.calledOnce(familyHistoryEntryServiceSpy);
        //sinon.assert.calledTwice(roleServiceSpy);
        sinon.assert.calledWith(familyHistoryEntryServiceSpy, sinon.match({
            "medicalRecordNumber":req.body.medicalRecordNumber,
            "relative":req.body.relative,
            "history":req.body.history,
        }));
    });

    it('familyHistoryEntryController unit test using familyHistoryEntryService mock', async function () {		
        // Arrange
        this.timeout(timeout);
        const body = { 
            "medicalRecordNumber": "202412000002",
            "relative": "Mãe",
            "history": "Asma, Osteoporose."
        };
        
        const req: Partial<Request> = { body };
        const jsonSpy = sinon.spy();
        const res: Partial<Response> = {
            json: jsonSpy, 
        };
        const next: Partial<NextFunction> = () => {};

        let familyHistoryEntryServiceInstance:IFamilyHistoryEntryService = Container.get("FamilyHistoryEntryService");
        const familyHistoryEntryServiceMock = sinon.mock(familyHistoryEntryServiceInstance)
        familyHistoryEntryServiceMock.expects("createFamilyHistoryEntry")
            .once()
            .withArgs(sinon.match({
                "medicalRecordNumber":req.body.medicalRecordNumber,
                "relative":req.body.relative,
                "history":req.body.history,
            }))
            .returns(Result.ok<IFamilyHistoryEntryDTO>( {
                "entryNumber":"202412000002001",
                "medicalRecordNumber":req.body.medicalRecordNumber,
                "relative":req.body.relative,
                "history":req.body.history,
            } ));

        const ctrl = new FamilyHistoryEntryController(familyHistoryEntryServiceInstance as IFamilyHistoryEntryService);

        // Act
        await ctrl.createFamilyHistoryEntry(<Request>req, <Response>res, <NextFunction>next);

        // Assert
        familyHistoryEntryServiceMock.verify();
        sinon.assert.calledOnce(jsonSpy);
        sinon.assert.calledWith(jsonSpy, sinon.match({ 
            entryNumber: "202412000002001", 
            medicalRecordNumber: req.body.medicalRecordNumber,
            relative: req.body.relative,
            history: req.body.history
        }));
    });
});