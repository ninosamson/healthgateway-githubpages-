# Functional Tests

Implements functional tests for Health Gateway Admin using Cypress.io tooling.

## Prequisites

A Developer should have gone through the Health Gateway installation and configuration.

The developer should re-seed his/her database by connecting to the DB using PGAdmin or another client tool and running
healthgateway/Testing/functional/tests/cypress/db/seed.sql

## Cypress

### Installation and configuration

Installation of Cypress is as easy as running npm install.

```bash
npm install
```

Create a cypress.env.json and update with passwords or any other environment variables you would like to override.

```bash
{
    "baseUrl": "https://dev-admin.healthgateway.gov.bc.ca",
    "idir_username": "THE IDIR USERNAME",
    "idir_password": "THE PASSWORD"
}
```

### Running Interactively

While creating and debugging tests you will want to run Cypress interactively.

```bash
export CYPRESS_BASE_URL=https://dev-admin.healthgateway.gov.bc.ca
npx cypress open
```

If you want to verify the tests againt https://dev-admin.healthgateway.gov.bc.ca, then do not set the CYPRESS_BASE_URL environment variable.
Note: you cannot run cypress tests locally i.e., http://localhost:5027 because Admin uses IDIR authentication, which cannot be used locally.

e2e: contains tests that will be run in the dev environment only.
ui: contains tests that are either stubbed or cosmetic only and can be run in any environment

### Running via CLI

You can run Cypress on the CLI and have the system record videos.

To Run all tests for dev environment execute:

```bash
npx cypress run --browser chrome --spec "cypress/integration/ui/**/*,cypress/integration/e2e/**/*"
```

to Run the tests intended for the mock environment:

```bash
npx cypress run --browser chrome --spec "cypress/integration/ui/**/*,cypress/integration/mock/**/*" --config baseUrl=https://mock-admin.healthgateway.gov.bc.ca
```

You can also run Cypress as a specific browser by executing although this will launch and close Chrome many times.

```bash
npx cypress run --browser chrome
```

You can run a specific test

```bash
npx cypress run  --browser chrome --spec "cypress/integration/e2e/authentication/login.cy.js"
```

Run and record the results to the Cypress dashboard

```bash
npx cypress run --record --key KEY
```