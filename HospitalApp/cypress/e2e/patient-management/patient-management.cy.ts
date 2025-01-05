let tokenPatientManagement:string="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkbWluQGhvc3BpdGFsLmNvbSIsImV4cCI6MTczNjExNTQ1OSwicm9sZSI6ImFkbWluIiwidXNlcm5hbWUiOiJhZG1pbiJ9.Z864cckPmT6RsSNN453EQ_99TASrczDXquOUQO6mlqc";
let MedicalRecordNumber:string;

describe('patient', () => {
    before(() => {
        if (!tokenPatientManagement) {
            throw new Error('Token não encontrado');
        }
    });

    it('accesses patient page', () => {
        localStorage.setItem("token", tokenPatientManagement)
        cy.visit("http://localhost:4200/patientmanagement");

        cy.get('input[ng-reflect-name="FullNameSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="FullNameValue"]').should('exist').type('Test One');

        cy.get('button[type="submit"]').click();
        cy.contains('Não foi encontrado nenhum Paciente com essas configurações.').should('be.visible');
    });

    it('creates patient', () => {
        localStorage.setItem("token", tokenPatientManagement)

        cy.visit('http://localhost:4200/patientmanagement');

        cy.contains('button', 'Criar um novo Paciente').click();

        cy.get('input[ng-reflect-name="FullName"]').type('Test One');
        cy.get('input[ng-reflect-name="Email"]').type('diogo10072004@gmail.com');
        cy.get('input[ng-reflect-name="PhoneNumber"]').type('999888777');
        cy.get('input[ng-reflect-name="DateOfBirth"]').type('2004-10-07');
        cy.get('select[ng-reflect-name="Gender"]').select('Male');

        cy.contains('button', 'Adicionar Alergia').click();
        cy.get('input[placeholder="Insira uma alergia"]').type('Gatos');
        cy.get('input[placeholder="Insira uma alergia"]').should('have.value', 'Gatos'); 

        cy.contains('button', 'Salvar').click();
        cy.contains(/Paciente \d+ criado com sucesso!/).invoke('text').then((text) => {
            const match = text.match(/\d+/); // Procura o MedicalRecordNumber na string
            if (match) {
                MedicalRecordNumber = match[0];
                console.log(MedicalRecordNumber);
                cy.log(`Número do paciente: ${MedicalRecordNumber}`);
                cy.wrap(MedicalRecordNumber).as('MedicalRecordNumber');
            } else {
                throw new Error('Não foi possível extrair o número do paciente.');
            }
        });

        cy.get('@MedicalRecordNumber').then((MedicalRecordNumber) => {
            cy.get('input[ng-reflect-name="MedicalRecordNumberSelected"]').should('exist').check();
            cy.get('input[ng-reflect-name="MedicalRecordNumberValue"]').should('exist').type(String(MedicalRecordNumber));
        });

        cy.get('button[type="submit"]').click();
        cy.contains('Test One'); //Found it!
    });
    it("edits patients email", () => {
        localStorage.setItem("token", tokenPatientManagement);
        cy.visit('http://localhost:4200/patientmanagement');

        cy.get('input[ng-reflect-name="MedicalRecordNumberSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="MedicalRecordNumberValue"]').should('exist').type(String(MedicalRecordNumber));
        cy.get('button[type="submit"]').click();
        cy.contains('Test One').click();
        cy.contains('button', 'Editar').click();

        cy.get('input[ng-reflect-name="EmailSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="EmailValue"]').should('exist').clear().type('diogofscunha2004@gmail.com');
        cy.contains('button','Salvar').click();
        cy.contains(`Paciente ${MedicalRecordNumber} editado com sucesso!`);
        cy.contains('diogofscunha2004@gmail.com'); //It was successfully edited
    })
    it("delete patient", () => {
        localStorage.setItem("token", tokenPatientManagement);
        cy.visit('http://localhost:4200/patientmanagement');

        cy.get('input[ng-reflect-name="MedicalRecordNumberSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="MedicalRecordNumberValue"]').should('exist').type(String(MedicalRecordNumber));
        cy.get('button[type="submit"]').click();
        cy.contains('Test One').click();
        cy.contains('button', 'Eliminar').click();
        cy.contains('button','Sim').click();
        cy.contains(`Paciente ${MedicalRecordNumber} eliminado com sucesso!`);
        cy.contains('Não foi encontrado nenhum Paciente com essas configurações.').should('be.visible'); //It was successfully deleted
    })
});
