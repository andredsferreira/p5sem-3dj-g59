let tokenStaffManagement:string="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkbWluQGhvc3BpdGFsLmNvbSIsImV4cCI6MTczNjExNTQ5OCwicm9sZSI6ImFkbWluIiwidXNlcm5hbWUiOiJhZG1pbiJ9.wfGTLgD2mYJUA1gFf9JFIeuARGycQs3KK3ZNVbbIE00";
let LicenseNumber:string;

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
