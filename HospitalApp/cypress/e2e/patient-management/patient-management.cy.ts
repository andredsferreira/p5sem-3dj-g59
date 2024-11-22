let token:string;

describe('login', () => {
    it('logs in and accesses patient page', () => {
        cy.intercept('POST', '/api/auth/login').as('loginRequest');
        cy.visit("http://localhost:4200/login");
        cy.get('#username').type('admin');
        cy.get('#password').type('adminpassword');
        cy.get('button[type="submit"]').click(); 

        cy.wait('@loginRequest').then((interception) => {
            if (interception.response != undefined) token = interception.response.body.token;
            cy.log('Token received:', token);
            expect(token).to.exist;
        });

        cy.url().should('include', '/admin');
        
        cy.contains('button', 'Gerir Pacientes').click();
        cy.url().should('include', '/patientmanagement');
    })
})
describe('patient', () => {
    before(() => {
        if (!token) {
            throw new Error('Token não encontrado');
        }
    });

    it('accesses patient page', () => {
        localStorage.setItem("token", token)
        cy.visit("http://localhost:4200/patientmanagement");

        cy.get('input[ng-reflect-name="FullNameSelected"]').should('exist').check();
        cy.get('input[ng-reflect-name="FullNameValue"]').should('exist').type('Test One');

        cy.get('button[type="submit"]').click();
        cy.contains('Não foi encontrado nenhum Paciente com essas configurações.').should('be.visible');
    });

    it('creates patient', () => {
        localStorage.setItem("token", token)

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

        // cy.contains('Paciente criado com sucesso!').should('be.visible');
    });
});
