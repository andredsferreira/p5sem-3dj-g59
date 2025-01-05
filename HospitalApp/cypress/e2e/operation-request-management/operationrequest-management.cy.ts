let tokenOperationRequest:string="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRpYWdvQGhvc3BpdGFsLmNvbSIsImV4cCI6MTczNjExNTM2Mywicm9sZSI6ImRvY3RvciIsInVzZXJuYW1lIjoidGlhZ28ifQ.xaRye-O-crHg4MFpqj2fSQxif-psBmblkVRODEOIz_c";
let foda:string;

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

        cy.get('input[formControlName="medicalRecordNumber"]').type('202411000002');
        cy.get('select[formControlName="operationTypeName"]').select('Trigger finger');
        cy.get('select[formControlName="priority"]').select('Elective');
        cy.get('input[formControlName="dateTime"]').type('2025-02-10T18:30');
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
    it('removes operation request', () => {
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
