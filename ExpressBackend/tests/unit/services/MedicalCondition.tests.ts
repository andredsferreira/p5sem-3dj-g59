import * as sinon from 'sinon';
import * as mocha from 'mocha';
import "reflect-metadata";
import { expect } from 'chai';
import FamilyHistoryEntryService from '../../../src/services/familyHistoryEntryService';
import { MedicalRecord } from '../../../src/domain/medicalRecord';
import { FamilyHistoryEntry } from '../../../src/domain/familyHistoryEntry';
import { IFamilyHistoryEntryDTO, IFamilyHistoryEntryOptionalDTO } from '../../../src/dto/IFamilyHistoryEntryDTO';
import Container from 'typedi';
import IMedicalRecordRepo from '../../../src/services/IRepos/IMedicalRecordRepo';
import IFamilyHistoryEntryRepo from '../../../src/services/IRepos/IFamilyHistoryEntryRepo';
import { FamilyHistoryEntryMap } from '../../../src/mappers/FamilyHistoryEntryMap';

const timeout:number = 10000;

//-------------------------------SCHEMA--------------------------------------
const famHistorySchemaPath:string = "../../../src/persistence/schemas/familyHistoryEntrySchema";
const medicalRecordSchemaPath:string = "../../../src/persistence/schemas/medicalRecordSchema";

//--------------------------------REPO---------------------------------------
const medRecordRepoPath:string = "../../../src/repos/medicalRecordRepo";
const famHistoryRepoPath:string = "../../../src/repos/familyHistoryEntryRepo";

function seedMedicalRecord(): MedicalRecord {
  return MedicalRecord.create({medicalRecordNumber: "202412000002"}).getValue();
}

function seedFamilyEntry(): FamilyHistoryEntry {
  return FamilyHistoryEntry.create({entryNumber: "202412000002001", medicalRecordNumber: "202412000002", relative: "Pai", history: "Sem histórico médico significativo."}).getValue();
}

function seedNewFamilyEntryDTO(): IFamilyHistoryEntryDTO {
  return {entryNumber: "vai ser trocado ao criar", medicalRecordNumber: "202412000002", relative: "Mãe", history: "Asma, Osteoporose."};
}

function seedHistoryChange(): IFamilyHistoryEntryOptionalDTO {
  return {history: "Asma, Osteoporose, Supertensão.", relative: undefined};
}

mocha.describe('familyEntry service', () => {
  const sandbox = sinon.createSandbox();

  mocha.beforeEach(function() {
    this.timeout(timeout);
    Container.reset();

    //FAMILY HISTORY ENTRY

    let familyHistorySchemaInstance = require(famHistorySchemaPath).default;
    Container.set("familyHistoryEntrySchema", familyHistorySchemaInstance);

    let familyHistoryRepoClass = require(famHistoryRepoPath).default;
    let familyHistoryRepoInstance = Container.get(familyHistoryRepoClass);
    Container.set("FamilyHistoryRepo", familyHistoryRepoInstance);

    //MEDICAL RECORD ENTRY

    let medRecordSchemaInstance = require(medicalRecordSchemaPath ).default;
    Container.set("medicalRecordSchema", medRecordSchemaInstance);

    let medRecordRepoClass = require(medRecordRepoPath).default;
    let medRecordRepoInstance = Container.get(medRecordRepoClass);
    Container.set("MedicalRecordRepo", medRecordRepoInstance);
  })

  mocha.afterEach(function() {
    sandbox.restore();
  });

  //-------------------CREATE------------------------------------------------------

  mocha.it('creation', async function() {
    // Arrange
    this.timeout(timeout);

    let medicalRecordRepoInstance:IMedicalRecordRepo = Container.get("MedicalRecordRepo");
    sinon.stub(medicalRecordRepoInstance, "findByMedicalRecordNumber").returns(Promise.resolve(seedMedicalRecord()));

    let familyHistoryRepoInstance:IFamilyHistoryEntryRepo = Container.get("FamilyHistoryRepo");
    sinon.stub(familyHistoryRepoInstance, "findByMedicalRecordNumber").returns(Promise.resolve([seedFamilyEntry()]));
    sinon.stub(familyHistoryRepoInstance, "save").returns(Promise.resolve(seedFamilyEntry()));

    // Act
    const serv = new FamilyHistoryEntryService(familyHistoryRepoInstance, medicalRecordRepoInstance);

    const output = (await serv.createFamilyHistoryEntry(seedNewFamilyEntryDTO())).getValue();
  
    // Assert
    expect(serv).to.not.be.undefined;
    expect(output.entryNumber).to.be.eq('202412000002002') // entry number foi gerado
  });

  //-------------------EDIT------------------------------------------------------


  mocha.it('edit', async function() {
    // Arrange
    this.timeout(timeout);

    let medicalRecordRepoInstance:IMedicalRecordRepo = Container.get("MedicalRecordRepo");

    let familyHistoryRepoInstance:IFamilyHistoryEntryRepo = Container.get("FamilyHistoryRepo");
    sinon.stub(familyHistoryRepoInstance, "findByEntryNumber").returns(Promise.resolve(seedFamilyEntry()));
    sinon.stub(familyHistoryRepoInstance, "save").returns(Promise.resolve(seedFamilyEntry()));

    // Act
    const serv = new FamilyHistoryEntryService(familyHistoryRepoInstance, medicalRecordRepoInstance);
    const input = seedFamilyEntry();
    const output = (await serv.editFamilyHistoryEntry(input.entryNumber, seedHistoryChange())).getValue();
  
    // Assert
    expect(serv).to.not.be.undefined;
    expect(input.entryNumber).to.be.eq(output.entryNumber) // entry number não mudou
    expect(input.relative).to.be.eq(output.relative) // relative não mudou

    expect(input.history).to.not.be.eq(output.history) // history mudou
    expect(output.history).to.be.eq(seedHistoryChange().history)
  });

  //-------------------SEARCH------------------------------------------------------

  mocha.it('search get values', async function() {
    // Arrange
    this.timeout(timeout);

    const entry = seedFamilyEntry();

    let medicalRecordRepoInstance:IMedicalRecordRepo = Container.get("MedicalRecordRepo");

    let familyHistoryRepoInstance:IFamilyHistoryEntryRepo = Container.get("FamilyHistoryRepo");
    sinon.stub(familyHistoryRepoInstance, "search").returns(Promise.resolve([entry]));

    // Act
    const serv = new FamilyHistoryEntryService(familyHistoryRepoInstance, medicalRecordRepoInstance);
    const output = (await serv.search(entry.medicalRecordNumber, seedHistoryChange())).getValue();
  
    // Assert
    expect(serv).to.not.be.undefined;
    expect(output).to.deep.include(FamilyHistoryEntryMap.toDTO(entry));
  });

  mocha.it('search doesnt get values', async function() {
    // Arrange
    this.timeout(timeout);

    const entry = seedFamilyEntry();

    let medicalRecordRepoInstance:IMedicalRecordRepo = Container.get("MedicalRecordRepo");

    let familyHistoryRepoInstance:IFamilyHistoryEntryRepo = Container.get("FamilyHistoryRepo");
    sinon.stub(familyHistoryRepoInstance, "search").returns(Promise.resolve([]));

    // Act
    const serv = new FamilyHistoryEntryService(familyHistoryRepoInstance, medicalRecordRepoInstance);
    const output = (await serv.search(entry.medicalRecordNumber, seedHistoryChange())).errorValue();
  
    // Assert
    expect(serv).to.not.be.undefined;
    expect(output.toString()).to.be.eq("familyHistoryEntry not found");
  });

});