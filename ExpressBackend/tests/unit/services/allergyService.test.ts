import * as sinon from 'sinon';
import * as mocha from 'mocha';
import "reflect-metadata";
import { expect } from 'chai';
import AllergyService from '../../../src/services/allergyService';
import { Allergy } from '../../../src/domain/allergy';
import { IAllergyDTO } from '../../../src/dto/IAllergyDTO';
import Container from 'typedi';
import IAllergyRepo from '../../../src/services/IRepos/IAllergyRepo';
import { AllergyMap } from '../../../src/mappers/AllergyMap';

const timeout: number = 10000;

//--------------------------------SCHEMA---------------------------------------
const allergySchemaPath: string = "../../../src/persistence/schemas/allergySchema";

//--------------------------------REPO---------------------------------------
const allergyRepoPath: string = "../../../src/repos/allergyRepo";

function seedAllergy(): Allergy {
    return Allergy.create({
        code: "A123",
        name: "Peanuts",
        description: "Severe allergic reactions to peanuts",
    }).getValue();
}

function seedAllergyDTO(): IAllergyDTO {
    return {
        code: "A124",
        name: "Tree Nuts",
        description: "Mild reactions to tree nuts",
    };
}

mocha.describe('allergy service', () => {
    const sandbox = sinon.createSandbox();

    mocha.beforeEach(function () {
        this.timeout(timeout);
        Container.reset();

        // ALLERGY SCHEMA
        let allergySchemaInstance = require(allergySchemaPath).default;
        Container.set("allergySchema", allergySchemaInstance);

        // ALLERGY REPO
        let allergyRepoClass = require(allergyRepoPath).default;
        let allergyRepoInstance = Container.get(allergyRepoClass);
        Container.set("AllergyRepo", allergyRepoInstance);
    });

    mocha.afterEach(function () {
        sandbox.restore();
    });

    //-------------------CREATE------------------------------------------------------

    mocha.it('create allergy', async function () {
        // Arrange
        this.timeout(timeout);

        const allergyRepoInstance: IAllergyRepo = Container.get("AllergyRepo");
        sinon.stub(allergyRepoInstance, "save").resolves(seedAllergy());

        // Act
        const serv = new AllergyService(allergyRepoInstance);
        const output = (await serv.createAllergy(seedAllergyDTO())).getValue();

        // Assert
        expect(serv).to.not.be.undefined;
        expect(output.code).to.be.eq(seedAllergyDTO().code);
        expect(output.name).to.be.eq(seedAllergyDTO().name);
        expect(output.description).to.be.eq(seedAllergyDTO().description);
    });

    //-------------------GET BY NAME------------------------------------------------------

    mocha.it('get allergy by name (found)', async function () {
        // Arrange
        this.timeout(timeout);

        const allergyRepoInstance: IAllergyRepo = Container.get("AllergyRepo");
        sinon.stub(allergyRepoInstance, "findByName").resolves(seedAllergy());

        // Act
        const serv = new AllergyService(allergyRepoInstance);
        const output = (await serv.getAllergyByName("Peanuts")).getValue();

        // Assert
        expect(serv).to.not.be.undefined;
        expect(output.name).to.be.eq("Peanuts");
        expect(output.code).to.be.eq("A123");
        expect(output.description).to.be.eq("Severe allergic reactions to peanuts");
    });

    mocha.it('get allergy by name (not found)', async function () {
        // Arrange
        this.timeout(timeout);

        const allergyRepoInstance: IAllergyRepo = Container.get("AllergyRepo");
        sinon.stub(allergyRepoInstance, "findByName").resolves(undefined);

        // Act
        const serv = new AllergyService(allergyRepoInstance);
        const output = (await serv.getAllergyByName("Nonexistent")).errorValue();

        // Assert
        expect(serv).to.not.be.undefined;
        expect(output.toString()).to.be.eq("allergy not found");
    });

    //-------------------UPDATE------------------------------------------------------

    mocha.it('update allergy (success)', async function () {
        // Arrange
        this.timeout(timeout);

        const allergyRepoInstance: IAllergyRepo = Container.get("AllergyRepo");
        const originalAllergy = seedAllergy();
        sinon.stub(allergyRepoInstance, "findByName").resolves(originalAllergy);
        sinon.stub(allergyRepoInstance, "save").resolves(originalAllergy);

        const updatedDTO = {
            code: "A125",
            name: "Updated Name",
            description: "Updated Description",
        };

        // Act
        const serv = new AllergyService(allergyRepoInstance);
        const output = (await serv.updateAllergy("Peanuts", updatedDTO)).getValue();

        // Assert
        expect(serv).to.not.be.undefined;
        expect(output.code).to.be.eq("A125");
        expect(output.name).to.be.eq("Updated Name");
        expect(output.description).to.be.eq("Updated Description");
    });

    mocha.it('update allergy (not found)', async function () {
        // Arrange
        this.timeout(timeout);

        const allergyRepoInstance: IAllergyRepo = Container.get("AllergyRepo");
        sinon.stub(allergyRepoInstance, "findByName").resolves(undefined);

        const updatedDTO = {
            code: "A125",
            name: "Updated Name",
            description: "Updated Description",
        };

        // Act
        const serv = new AllergyService(allergyRepoInstance);
        const output = (await serv.updateAllergy("Nonexistent", updatedDTO)).errorValue();

        // Assert
        expect(serv).to.not.be.undefined;
        expect(output.toString()).to.be.eq("allergy not found");
    });
});
