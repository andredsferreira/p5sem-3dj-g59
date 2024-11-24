let tokenOperationRequest:string;
let foda:string;

describe('login', () => {
    it('logs in and accesses doctor page', () => {
        cy.intercept('POST', '/api/auth/login').as('loginRequest');
        cy.visit("http://localhost:4200/login");
        cy.get('#username').type('tiago');
        cy.get('#password').type('tiagopassword');
        cy.get('button[type="submit"]').click(); 

        cy.wait('@loginRequest').then((interception) => {
            if (interception.response != undefined) tokenOperationRequest = interception.response.body.token;
            cy.log('Token received:', tokenOperationRequest);
            expect(tokenOperationRequest).to.exist;
        });

        cy.url().should('include', '/doctor');
    })
});
describe('operationrequest', () => {
    before(() => {
        if (!tokenOperationRequest) {
            throw new Error('Token não encontrado');
        }
    });

    it('accesses doctor page', () => {
        localStorage.setItem("token", tokenOperationRequest)
        cy.visit("http://localhost:4200/doctor");

        cy.get('select[formControlName="filterCriteria"]').select('patientFullName');
        cy.get('input[formControlName="filterValue"]').type('André Tomás Tomás André');

        cy.contains("Search").click();
        //Nothing appears
    });
    it('creates operation request', () => {
        localStorage.setItem("token", tokenOperationRequest)
        cy.visit("http://localhost:4200/doctor");

        cy.contains('button', 'Create Operation Request').click();

        cy.get('input[formControlName="patientId"]').type('1705adf7-216b-42db-b17d-fa01d124194f');
        cy.get('select[formControlName="operationTypeName"]').select('Trigger finger');
        cy.get('select[formControlName="priority"]').select('Elective');
        cy.get('input[formControlName="dateTime"]').type('2024-11-28T18:30');
        cy.get('select[formControlName="requestStatus"]').select('Pending');

        cy.contains("Submit").click();
    });
    it('edits operation request', () => {
        localStorage.setItem("token", tokenOperationRequest)
        cy.visit("http://localhost:4200/doctor");

        cy.contains('button', 'List Operation Requests').click();
        cy.get('select[formControlName="filterCriteria"]').select('patientFullName');
        cy.get('input[formControlName="filterValue"]').type('André Tomás Tomás André');

        cy.wait(10000);

        cy.contains("Search").click();

        cy.wait(5000);

        cy.contains("Update").click();

        cy.get('select[formControlName="priority"]').select('Emergency');

        cy.contains("Update").click();

        cy.wait(10000);

        cy.contains("Emergency");
    });
    it('edits operation request', () => {
        localStorage.setItem("token", tokenOperationRequest)
        cy.visit("http://localhost:4200/doctor");

        cy.contains('button', 'List Operation Requests').click();
        cy.get('select[formControlName="filterCriteria"]').select('patientFullName');
        cy.get('input[formControlName="filterValue"]').type('André Tomás Tomás André');

        cy.wait(10000);

        cy.contains("Search").click();

        cy.wait(5000);

        cy.contains("Delete").click();
        cy.contains("Yes, Delete").click();

        cy.wait(10000);
    });
});
