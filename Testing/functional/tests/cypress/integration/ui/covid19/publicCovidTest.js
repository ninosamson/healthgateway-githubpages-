const { monthNames } = require("../../../support/constants");

const dobYearSelector =
    "[data-testid=dateOfBirthInput] [data-testid=formSelectYear]";
const dobMonthSelector =
    "[data-testid=dateOfBirthInput] [data-testid=formSelectMonth]";
const dobDaySelector =
    "[data-testid=dateOfBirthInput] [data-testid=formSelectDay]";
const collectionDateYearSelector =
    "[data-testid=dateOfCollectionInput] [data-testid=formSelectYear]";
const collectionDateMonthSelector =
    "[data-testid=dateOfCollectionInput] [data-testid=formSelectMonth]";
const collectionDateDaySelector =
    "[data-testid=dateOfCollectionInput] [data-testid=formSelectDay]";

const feedbackPhnMustBeValidSelector = "[data-testid=feedbackPhnMustBeValid]";
const feedbackPhnIsRequiredSelector = "[data-testid=feedbackPhnIsRequired]";
const feedbackDobIsRequiredSelector = "[data-testid=feedbackDobIsRequired]";
const feedbackCollectionDateIsRequiredSelector =
    "[data-testid=feedbackCollectionDateIsRequired]";

const publicLaboratoryResultModule = "PublicLaboratoryResult";
const laboratoryModule = "Laboratory";
const covidTestUrl = "/covidtest";

const dummyYear = "2021";
const dummyMonth = "June";
const dummyDay = "15";

function selectOption(selector, option) {
    return cy.get(selector).should("be.visible", "be.enabled").select(option);
}

function selectShouldContain(selector, value) {
    cy.get(selector)
        .children("[value=" + value + "]")
        .should("exist");
}

function selectShouldNotContain(selector, value) {
    cy.get(selector)
        .children("[value=" + value + "]")
        .should("not.exist");
}

function enterCovidTestPHN(phn) {
    cy.get("[data-testid=phnInput]")
        .should("be.visible", "be.enabled")
        .clear()
        .type(phn);
}

function clickCovidTestEnterButton() {
    cy.get("[data-testid=btnEnter]").should("be.enabled", "be.visible").click();
}

describe("Public COVID-19 Test Form", () => {
    beforeEach(() => {
        cy.enableModules([laboratoryModule, publicLaboratoryResultModule]);
        cy.visit(covidTestUrl);
    });

    it("Cancel Button Should Go to Landing Page", () => {
        cy.get("[data-testid=btnCancel]").should("be.visible").click();
        cy.get("[data-testid=covid-test-button]").should("exist");
    });

    it("Log In Button Should Go to Login Page", () => {
        cy.get("[data-testid=btnLogin]")
            .should("be.visible", "be.enabled")
            .click();

        cy.get("[data-testid=vaccineCardFormTitle]").should("not.exist");
    });

    it("Validate PHN, DOB, and Collection Date Required", () => {
        clickCovidTestEnterButton();

        cy.get(feedbackPhnIsRequiredSelector).should("be.visible");
        cy.get(feedbackDobIsRequiredSelector).should("be.visible");
        cy.get(feedbackCollectionDateIsRequiredSelector).should("be.visible");
    });

    it("Validate PHN Format", () => {
        cy.log("Validate valid PHN passes validation.");
        enterCovidTestPHN(Cypress.env("phn"));

        selectOption(dobYearSelector, "Year");

        cy.get("[data-testid=phnInput]")
            .should("be.visible", "be.enabled")
            .and("have.class", "form-control is-valid");

        cy.log("Validate invalid PHN fails validation.");
        enterCovidTestPHN(Cypress.env("phn").replace(/.$/, ""));

        clickCovidTestEnterButton();

        cy.get(feedbackPhnMustBeValidSelector).should("be.visible");
    });

    it("Validate Date Range for DOB and Collection Date", () => {
        const d = new Date();
        const year = d.getFullYear();
        const monthNumber = d.getMonth(); //Maps to monthNames constant array. Index starts at 0
        const month = d.getMonth() + 1; //0 is January but UI index starts at 1 for January
        const nextMonth = d.getMonth() === 11 ? 1 : month + 1; //When current month is December, next month will be January
        const day = d.getDate();
        cy.log(
            "Current Date: " +
                d.toDateString() +
                " - Year: " +
                year +
                " - Month Number: " +
                monthNumber +
                " - Month: " +
                month +
                " - Next Month: " +
                nextMonth +
                " - Day: " +
                day
        );

        d.setDate(d.getDate() + 1);
        const nextYear = d.getFullYear() === year ? year + 1 : d.getFullYear();
        const nextDay = d.getDate();
        cy.log(
            "Future Date - Next Year: " + nextYear + " - Next Day: " + nextDay
        );

        // Test Future Year does not exist
        cy.log("Testing Future Year does not exist.");
        selectShouldNotContain(dobYearSelector, nextYear.toString());
        selectShouldNotContain(collectionDateYearSelector, nextYear.toString());

        // Test Current Year
        selectOption(dobYearSelector, year.toString());
        selectOption(collectionDateYearSelector, year.toString());
        cy.log("Testing Current Year.");

        if (nextMonth > 1) {
            cy.log("Next Month is: " + nextMonth);
            cy.log(
                "Current year has been set in dropdown, so if next month is 1 - January, it means current month is December. Test Future Month does not exist. Month can only be current or past month for current year."
            );
            selectShouldNotContain(dobMonthSelector, nextMonth);
            selectShouldNotContain(collectionDateMonthSelector, nextMonth);
        }

        cy.log("Test and set Current Month");
        selectOption(dobMonthSelector, monthNames[monthNumber]);
        selectOption(collectionDateMonthSelector, monthNames[monthNumber]);

        if (nextDay > 1) {
            cy.log("Next Day: " + nextDay);
            cy.log(
                "Current Year and Month have been set. If next day is 1, it means previous day was last day of current month. Next Day is associated with the current month. Test Future Day in current month does not exist."
            );
            selectShouldNotContain(dobDaySelector, nextDay);
            selectShouldNotContain(collectionDateDaySelector, nextDay);
        }
        //Test Current Day exists
        cy.log("Test Current Day exists.");
        selectShouldContain(dobDaySelector, day);
        selectShouldContain(collectionDateDaySelector, day);
    });

    it("Validate DOB Year Required", () => {
        selectOption(dobMonthSelector, dummyMonth);
        selectOption(dobDaySelector, dummyDay);

        clickCovidTestEnterButton();

        cy.get(feedbackDobIsRequiredSelector).should("be.visible");
    });

    it("Validate DOB Month Required", () => {
        selectOption(dobYearSelector, dummyYear);
        selectOption(dobDaySelector, dummyDay);

        clickCovidTestEnterButton();

        cy.get(feedbackDobIsRequiredSelector).should("be.visible");
    });

    it("Validate DOB Day Required", () => {
        selectOption(dobYearSelector, dummyYear);
        selectOption(dobMonthSelector, dummyMonth);

        clickCovidTestEnterButton();

        cy.get(feedbackDobIsRequiredSelector).should("be.visible");
    });

    it("Validate Collection Date Year Required", () => {
        selectOption(collectionDateMonthSelector, dummyMonth);
        selectOption(collectionDateDaySelector, dummyDay);

        clickCovidTestEnterButton();

        cy.get(feedbackCollectionDateIsRequiredSelector).should("be.visible");
    });

    it("Validate Collection Date Month Required", () => {
        selectOption(collectionDateYearSelector, dummyYear);
        selectOption(collectionDateDaySelector, dummyDay);

        clickCovidTestEnterButton();

        cy.get(feedbackCollectionDateIsRequiredSelector).should("be.visible");
    });

    it("Validate Collection Date Day Required", () => {
        selectOption(collectionDateYearSelector, dummyYear);
        selectOption(collectionDateMonthSelector, dummyMonth);

        clickCovidTestEnterButton();

        cy.get(feedbackCollectionDateIsRequiredSelector).should("be.visible");
    });
});

describe("Public COVID-19 Test Results", () => {
    beforeEach(() => {
        cy.enableModules([laboratoryModule, publicLaboratoryResultModule]);
        cy.visit(covidTestUrl);
    });

    it("Successful Response: 1 Result", () => {
        const phn = "9735361219 ";
        const dobYear = "1994";
        const dobMonth = "June";
        const dobDay = "9";
        const collectionDateYear = "2021";
        const collectionDateMonth = "January";
        const collectionDateDay = "20";

        cy.intercept("GET", "**/v1/api/PublicLaboratory/CovidTests", (req) => {
            req.reply({
                fixture: "LaboratoryService/covidTest.json",
            });
        });

        enterCovidTestPHN(phn);
        selectOption(dobYearSelector, dobYear);
        selectOption(dobMonthSelector, dobMonth);
        selectOption(dobDaySelector, dobDay);
        selectOption(collectionDateYearSelector, collectionDateYear);
        selectOption(collectionDateMonthSelector, collectionDateMonth);
        selectOption(collectionDateDaySelector, collectionDateDay);

        clickCovidTestEnterButton();
        cy.get("[data-testid=public-covid-test-result-form-title]").should(
            "be.visible"
        );
        cy.get("[data-testid=public-display-name]").should("be.visible");
        cy.get("[data-testid=public-display-name]").contains(
            "BONNET PROTERVITY"
        );
        cy.get("[data-testid=test-type-1]").should("be.visible");
        cy.get("[data-testid=test-type-1]").contains("Nasal");
        cy.get("[data-testid=test-outcome-span-1]").should("be.visible");
        cy.get("[data-testid=test-outcome-span-1]").contains("NoOrderFound");
        cy.get("[data-testid=collection-date-1]").should("be.visible");
        cy.get("[data-testid=collection-date-1]").contains(
            "2021-Dec-01, 4:07 PM"
        );
        cy.get("[data-testid=test-status]").should("be.visible");
        cy.get("[data-testid=test-status]").contains("Corrected");
        cy.get("[data-testid=result-date-1]").should("be.visible");
        cy.get("[data-testid=result-date-1]").contains("2021-Dec-01, 4:07 PM");
        cy.get("[data-testid=reporting-lab]").should("be.visible");
        cy.get("[data-testid=reporting-lab]").contains("Fha");
        cy.get("[data-testid=result-description]").should("be.visible");

        cy.get("[data-testid=btnCheckAnotherTest]").should("be.visible");
        cy.get("[data-testid=btnLogin]").should("be.visible");
    });

    it("Successful Response: No Results", () => {
        const phn = "9735361219 ";
        const dobYear = "1994";
        const dobMonth = "June";
        const dobDay = "9";
        const collectionDateYear = "2021";
        const collectionDateMonth = "January";
        const collectionDateDay = "20";

        cy.intercept("GET", "**/v1/api/PublicLaboratory/CovidTests", (req) => {
            req.reply({
                fixture: "LaboratoryService/emptyCovidTest.json",
            });
        });

        enterCovidTestPHN(phn);

        selectOption(dobYearSelector, dobYear);
        selectOption(dobMonthSelector, dobMonth);
        selectOption(dobDaySelector, dobDay);
        selectOption(collectionDateYearSelector, collectionDateYear);
        selectOption(collectionDateMonthSelector, collectionDateMonth);
        selectOption(collectionDateDaySelector, collectionDateDay);
        clickCovidTestEnterButton();
        cy.get("[data-testid=public-covid-test-result-form-title]").should(
            "be.visible"
        );
        cy.get("[data-testid=btnCheckAnotherTest]").should("be.visible");
        cy.get("[data-testid=btnLogin]").should("be.visible");
    });

    it("Unsuccessful Response: Data Mismatch", () => {
        const phn = "9735361219 ";
        const dobYear = "1994";
        const dobMonth = "June";
        const dobDay = "9";
        const collectionDateYear = "2021";
        const collectionDateMonth = "January";
        const collectionDateDay = "20";

        cy.intercept("GET", "**/v1/api/PublicLaboratory/CovidTests", (req) => {
            req.reply({
                fixture: "LaboratoryService/dataMismatchCovidTest.json",
            });
        });

        enterCovidTestPHN(phn);
        selectOption(dobYearSelector, dobYear);
        selectOption(dobMonthSelector, dobMonth);
        selectOption(dobDaySelector, dobDay);
        selectOption(collectionDateYearSelector, collectionDateYear);
        selectOption(collectionDateMonthSelector, collectionDateMonth);
        selectOption(collectionDateDaySelector, collectionDateDay);
        clickCovidTestEnterButton();
        cy.get("[data-testid=error-text-description]").should("be.visible");
    });

    it("Unsuccessful Response: Invalid", () => {
        const phn = "9735361219 ";
        const dobYear = "1994";
        const dobMonth = "June";
        const dobDay = "9";
        const collectionDateYear = "2021";
        const collectionDateMonth = "January";
        const collectionDateDay = "20";

        cy.intercept("GET", "**/v1/api/PublicLaboratory/CovidTests", (req) => {
            req.reply({
                fixture: "LaboratoryService/invalidCovidTest.json",
            });
        });

        enterCovidTestPHN(phn);
        selectOption(dobYearSelector, dobYear);
        selectOption(dobMonthSelector, dobMonth);
        selectOption(dobDaySelector, dobDay);
        selectOption(collectionDateYearSelector, collectionDateYear);
        selectOption(collectionDateMonthSelector, collectionDateMonth);
        selectOption(collectionDateDaySelector, collectionDateDay);
        clickCovidTestEnterButton();
        cy.get("[data-testid=error-text-description]").should("be.visible");
    });

    it("Check Another Test Button Should Go to Form", () => {
        const phn = "9735361219 ";
        const dobYear = "1994";
        const dobMonth = "June";
        const dobDay = "9";
        const collectionDateYear = "2021";
        const collectionDateMonth = "January";
        const collectionDateDay = "20";

        cy.intercept("GET", "**/v1/api/PublicLaboratory/CovidTests", (req) => {
            req.reply({
                fixture: "LaboratoryService/emptyCovidTest.json",
            });
        });

        enterCovidTestPHN(phn);

        selectOption(dobYearSelector, dobYear);
        selectOption(dobMonthSelector, dobMonth);
        selectOption(dobDaySelector, dobDay);
        selectOption(collectionDateYearSelector, collectionDateYear);
        selectOption(collectionDateMonthSelector, collectionDateMonth);
        selectOption(collectionDateDaySelector, collectionDateDay);

        clickCovidTestEnterButton();
        cy.get("[data-testid=public-covid-test-result-form-title]").should(
            "be.visible"
        );
        cy.get("[data-testid=btnLogin]").should("be.visible");
        cy.get("[data-testid=btnCheckAnotherTest]").should("be.visible");
        cy.get("[data-testid=btnCheckAnotherTest]").click();
        cy.get("[data-testid=phnInput]").should("be.visible");
    });
});