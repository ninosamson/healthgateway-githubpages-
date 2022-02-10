import { deleteDownloadsFolder } from "../../../support/utils";

const dobYearSelector =
    "[data-testid=dateOfBirthInput] [data-testid=formSelectYear]";
const dobMonthSelector =
    "[data-testid=dateOfBirthInput] [data-testid=formSelectMonth]";
const dobDaySelector =
    "[data-testid=dateOfBirthInput] [data-testid=formSelectDay]";
const dovYearSelector =
    "[data-testid=dateOfVaccineInput] [data-testid=formSelectYear]";
const dovMonthSelector =
    "[data-testid=dateOfVaccineInput] [data-testid=formSelectMonth]";
const dovDaySelector =
    "[data-testid=dateOfVaccineInput] [data-testid=formSelectDay]";

const vaccineCardUrl = "/vaccinecard";

const fullyVaccinatedPhn = "9735361219";
const fullyVaccinatedDobYear = "1994";
const fullyVaccinatedDobMonth = "June";
const fullyVaccinatedDobDay = "9";
const fullyVaccinatedDovYear = "2021";
const fullyVaccinatedDovMonth = "January";
const fullyVaccinatedDovDay = "20";

function enterVaccineCardPHN(phn) {
    cy.get("[data-testid=phnInput]")
        .should("be.visible", "be.enabled")
        .clear()
        .type(phn);
}

function selectOption(selector, option) {
    return cy.get(selector).should("be.visible", "be.enabled").select(option);
}

function clickVaccineCardEnterButton() {
    cy.get("[data-testid=btnEnter]").should("be.enabled", "be.visible").click();
}

describe("Public Vaccine Card Result", () => {
    it("Partially Vaccinated", () => {
        const phn = "9735353315";
        const dobYear = "1967";
        const dobMonth = "June";
        const dobDay = "2";
        const dovYear = "2021";
        const dovMonth = "July";
        const dovDay = "4";

        cy.enableModules([
            "Immunization",
            "VaccinationStatus",
            "VaccinationStatusPdf",
        ]);
        cy.visit(vaccineCardUrl);

        enterVaccineCardPHN(phn);

        selectOption(dobYearSelector, dobYear);
        selectOption(dobMonthSelector, dobMonth);
        selectOption(dobDaySelector, dobDay);
        selectOption(dovYearSelector, dovYear);
        selectOption(dovMonthSelector, dovMonth);
        selectOption(dovDaySelector, dovDay);

        clickVaccineCardEnterButton();

        cy.get("[data-testid=formTitleVaccineCard]").should("be.visible");
        cy.get("[data-testid=statusPartiallyVaccinated]").should("be.visible");
    });

    it("Fully Vaccinated", () => {
        cy.enableModules([
            "Immunization",
            "VaccinationStatus",
            "VaccinationStatusPdf",
        ]);
        cy.visit(vaccineCardUrl);

        enterVaccineCardPHN(fullyVaccinatedPhn);
        selectOption(dobYearSelector, fullyVaccinatedDobYear);
        selectOption(dobMonthSelector, fullyVaccinatedDobMonth);
        selectOption(dobDaySelector, fullyVaccinatedDobDay);
        selectOption(dovYearSelector, fullyVaccinatedDovYear);
        selectOption(dovMonthSelector, fullyVaccinatedDovMonth);
        selectOption(dovDaySelector, fullyVaccinatedDovDay);

        clickVaccineCardEnterButton();

        cy.get("[data-testid=formTitleVaccineCard]").should("be.visible");
        cy.get("[data-testid=statusVaccinated]").should("be.visible");
    });
});

describe("Public Vaccine Card Downloads", () => {
    beforeEach(() => {
        deleteDownloadsFolder();

        cy.enableModules([
            "Immunization",
            "VaccinationStatus",
            "VaccinationStatusPdf",
            "PublicVaccineDownloadPdf",
        ]);
        cy.visit(vaccineCardUrl);

        enterVaccineCardPHN(fullyVaccinatedPhn);
        selectOption(dobYearSelector, fullyVaccinatedDobYear);
        selectOption(dobMonthSelector, fullyVaccinatedDobMonth);
        selectOption(dobDaySelector, fullyVaccinatedDobDay);
        selectOption(dovYearSelector, fullyVaccinatedDovYear);
        selectOption(dovMonthSelector, fullyVaccinatedDovMonth);
        selectOption(dovDaySelector, fullyVaccinatedDovDay);

        clickVaccineCardEnterButton();
    });

    it("Save Federal Proof of Vaccination PDF", () => {
        cy.get("[data-testid=save-dropdown-btn]")
            .should("be.visible", "be.enabled")
            .click();
        cy.get("[data-testid=save-as-pdf-dropdown-item]")
            .should("be.visible")
            .click();

        cy.get("[data-testid=genericMessageModal]").should("be.visible");
        cy.get("[data-testid=genericMessageSubmitBtn]").click();

        cy.verifyDownload("VaccineProof.pdf");
    });
});