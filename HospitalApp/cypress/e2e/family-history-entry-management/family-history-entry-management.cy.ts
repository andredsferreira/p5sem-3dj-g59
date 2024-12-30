let tokenFamilyHistoryEntryManagement:string;

describe('login', () => {
    it('logs in and accesses medical record page', () => {
        cy.intercept('POST', '/api/auth/login').as('loginRequest');
        cy.visit("http://localhost:4200/login");
        cy.get('#username').type('tiago');
        cy.get('#password').type('tiagopassword');
        cy.get('button[type="submit"]').click(); 

        cy.wait('@loginRequest').then((interception) => {
            if (interception.response != undefined) tokenFamilyHistoryEntryManagement = interception.response.body.token;
            cy.log('Token received:', tokenFamilyHistoryEntryManagement);
            expect(tokenFamilyHistoryEntryManagement).to.exist;
        });

        cy.url().should('include', '/doctor');
    })
})
describe('family history entry', () => {
    before(() => {
        if (!tokenFamilyHistoryEntryManagement) {
            throw new Error('Token não encontrado');
        }
    });

    it('accesses medical record', () => {
        localStorage.setItem("token", tokenFamilyHistoryEntryManagement)
        cy.visit("http://localhost:4200/medicalrecord/202412000002");
        cy.contains('Registo 202412000002');

        cy.contains('Histórico Médico Familiar');
    });

    it('creates family history entry', () => {
        localStorage.setItem("token", tokenFamilyHistoryEntryManagement)

        cy.visit("http://localhost:4200/medicalrecord/202412000002");

        cy.contains('button', 'Histórico Médico Familiar').click();
        cy.contains('button', 'Criar entrada').click();

        cy.get('input[ng-reflect-name="relative"]').type('Mãe');
        cy.get('input[ng-reflect-name="history"]').type('Osteoporose.');

        cy.contains('button', 'Salvar').click();
        cy.wait(1000);
    });

    it("edits family history entry's description", () => {
        localStorage.setItem("token", tokenFamilyHistoryEntryManagement);
        cy.visit("http://localhost:4200/medicalrecord/202412000002");

        cy.contains('button', 'Histórico Médico Familiar').click();
        cy.contains('button', 'Pesquisar entradas').click();

        cy.get('input[ng-reflect-name="relativeSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="relativeValue"]').should('exist').type("Mãe");
        cy.get('input[ng-reflect-name="historySelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="historyValue"]').should('exist').type("Osteoporose.");
        cy.get('button[type="submit"]').click();
        
        cy.contains('Editar').click(); //Yey, encontramos

        cy.get('input[ng-reflect-name="historySelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="historyValue"]').should('exist').clear().type("Asma, Osteoporose.");
        cy.contains('button','Salvar').click();

        cy.contains('Asma, Osteoporose.'); //It was successfully edited
    })
});
