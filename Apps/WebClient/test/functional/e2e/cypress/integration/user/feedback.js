const { AuthMethod } = require("../../support/constants")

describe('User Feedback', () => {
    beforeEach(() => {
    })

    it('Send feedback', () => {
        cy.login(Cypress.env('keycloak.username'), 
                 Cypress.env('keycloak.password'), 
                 AuthMethod.KeyCloak)
        cy.get('[data-testid=feedbackContainer]').click()
        cy.get('[data-testid=feedbackCommentInput]')
            .type('Great job team!');
        cy.get('[data-testid=sendFeedbackMessageBtn]').click()
        cy.get('[data-testid=noNeedBtn]').should('be.visible');
        cy.get('[data-testid=updateMyEmailButton]').should('be.visible');
    })

    it('Protected user Sending feedback', () => {
        cy.login(Cypress.env('keycloak.protected.username'),
                 Cypress.env('keycloak.password'),
                 AuthMethod.KeyCloak)
        cy.get('[data-testid=feedbackContainer]').click()
        cy.get('[data-testid=feedbackCommentInput]')
            .type('Great job team!');
        cy.get('[data-testid=sendFeedbackMessageBtn]').click()
        cy.get('[data-testid=noNeedBtn]').should('be.visible');
        cy.get('[data-testid=updateMyEmailButton]').should('be.visible');
    })
})