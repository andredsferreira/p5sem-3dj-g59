let tokenPatientManagement:string;
let MedicalRecordNumber:string;

describe('login', () => {
    it('logs in and accesses patient page', () => {
        cy.intercept('POST', '/api/auth/login').as('loginRequest');
        cy.visit("http://localhost:4200/login");
        cy.get('#username').type('admin');
        cy.get('#password').type('adminpassword');
        cy.get('button[type="submit"]').click(); 

        cy.wait('@loginRequest').then((interception) => {
            if (interception.response != undefined) tokenPatientManagement = interception.response.body.token;
            cy.log('Token received:', tokenPatientManagement);
            expect(tokenPatientManagement).to.exist;
        });

        cy.url().should('include', '/admin');
        
        cy.contains('button', 'Gerir Pacientes').click();
        cy.url().should('include', '/patientmanagement');
    })
})
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
