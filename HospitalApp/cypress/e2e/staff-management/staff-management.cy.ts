let tokenStaffManagement:string;
let LicenseNumber:string;

describe('login', () => {
    it('logs in and accesses patient page', () => {
        cy.intercept('POST', '/api/auth/login').as('loginRequest');
        cy.visit("http://localhost:4200/login");
        cy.get('#username').type('admin');
        cy.get('#password').type('adminpassword');
        cy.get('button[type="submit"]').click(); 

        cy.wait('@loginRequest').then((interception) => {
            if (interception.response != undefined) tokenStaffManagement = interception.response.body.token;
            cy.log('Token received:', tokenStaffManagement);
            expect(tokenStaffManagement).to.exist;
        });

        cy.url().should('include', '/admin');
        
        cy.contains('button', 'Gerir Staff').click();
        cy.url().should('include', '/staffmanagement');
    })
})
describe('staff', () => {
    before(() => {
        if (!tokenStaffManagement) {
            throw new Error('Token não encontrado');
        }
    });

    it('accesses staff page', () => {
        localStorage.setItem("token", tokenStaffManagement)
        cy.visit("http://localhost:4200/staffmanagement");

     });

    it('creates staff', () => {
        localStorage.setItem("token", tokenStaffManagement)
        cy.visit('http://localhost:4200/staffmanagement');

        cy.contains('button', 'Create Staff').click();

        cy.get('input[formControlName="StaffRole"]').type('Medic');
        cy.get('input[formControlName="IdentityUsername"]').type('IdentityUsername123321');
        cy.get('input[formControlName="Email"]').type('email123123@hospital.com');
        cy.get('input[formControlName="Phone"]').type('912345678');
        cy.get('input[formControlName="Name"]').type('joaozinho');
        cy.get('input[formControlName="LicenseNumber"]').type('joaozinho123123');

        cy.contains("Submit").click();
    });
    it('edits staff', () => {
        localStorage.setItem("token", tokenStaffManagement)
        cy.visit("http://localhost:4200/staffmanagement");

        cy.contains('button', 'List Staffs').click();

        cy.contains('button', 'Update').click();

        cy.get('input[formControlName="email"]').clear().type('email123@hospital.com');
        cy.get('input[formControlName="phone"]').clear().type('912345676');
        cy.get('input[formControlName="FullName"]').clear().type('joaozinho Monteiro');
        
        cy.get('button[type="submit"]').click();
    });
    it("delete staff", () => {
        localStorage.setItem("token", tokenStaffManagement);
        cy.visit('http://localhost:4200/staffmanagement');

        cy.contains('button', 'List Staffs').click();

        cy.contains('button', 'Delete').click();

        cy.contains('button', 'Yes, Delete').click();

    })
    
});
