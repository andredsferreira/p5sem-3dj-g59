let tokenMedicalConditionEntryManagement:string = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRpYWdvQGhvc3BpdGFsLmNvbSIsImV4cCI6MTczNjExNTEzNCwicm9sZSI6ImRvY3RvciIsInVzZXJuYW1lIjoidGlhZ28ifQ.206RylRzGK5ngUITe7n9MB5AdKs3FqU_Ybp_er0Ayhg";

describe('medical condition entry', () => {
    before(() => {
        if (!tokenMedicalConditionEntryManagement) {
            throw new Error('Token não encontrado');
        }
    });

    it('accesses medical record', () => {
        localStorage.setItem("token", tokenMedicalConditionEntryManagement)
        cy.visit("http://localhost:4200/medicalrecord/202412000002");
        cy.contains('Registo 202412000002');

        cy.contains('Condições Medicas');
    });

    it('creates medical condition entry', () => {
        localStorage.setItem("token", tokenMedicalConditionEntryManagement)

        cy.visit("http://localhost:4200/medicalrecord/202412000002");

        cy.contains('button', 'Condições Medicas').click();
        cy.contains('button', 'Criar entrada').click();

        cy.get('input[ng-reflect-name="condition"]').type('Hypertension');
        cy.get('input[ng-reflect-name="year"]').type('2000');

        cy.contains('button', 'Salvar').click();
        cy.wait(1000);
    });

    it("edits family history entry's description", () => {
        localStorage.setItem("token", tokenMedicalConditionEntryManagement);
        cy.visit("http://localhost:4200/medicalrecord/202412000002");

        cy.contains('button', 'Condições Medicas').click();
        cy.contains('button', 'Pesquisar entradas').click();

        cy.get('input[ng-reflect-name="conditionSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="conditionValue"]').should('exist').type("Hypertension");
        cy.get('input[ng-reflect-name="yearSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="yearValue"]').should('exist').type("2000");
        cy.get('button[type="submit"]').click();
        
        cy.contains('Editar').click(); //Yey, encontramos

        cy.get('input[ng-reflect-name="conditionSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="conditionValue"]').should('exist').clear().type("Iron Deficiency Anemia");
        cy.contains('button','Salvar').click();

        cy.contains('Hypertension'); //It was successfully edited
    })
});
