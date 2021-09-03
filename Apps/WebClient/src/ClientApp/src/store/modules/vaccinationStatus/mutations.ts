import BannerError from "@/models/bannerError";
import { LoadStatus } from "@/models/storeOperations";
import VaccinationStatus from "@/models/vaccinationStatus";

import { VaccinationStatusMutations, VaccinationStatusState } from "./types";

export const mutations: VaccinationStatusMutations = {
    setRequested(state: VaccinationStatusState) {
        state.public.error = undefined;
        state.public.status = LoadStatus.REQUESTED;
        state.public.statusMessage = "";
    },
    setVaccinationStatus(
        state: VaccinationStatusState,
        vaccinationStatus: VaccinationStatus
    ) {
        state.public.vaccinationStatus = vaccinationStatus;
        state.public.status = LoadStatus.LOADED;
        state.public.statusMessage = "";
    },
    vaccinationStatusError(state: VaccinationStatusState, error: BannerError) {
        state.public.vaccinationStatus = undefined;
        state.public.error = error;
        state.public.status = LoadStatus.ERROR;
        state.public.statusMessage = "";
    },
    setPdfRequested(state: VaccinationStatusState) {
        state.public.error = undefined;
        state.public.statusMessage = "";
    },
    pdfError(state: VaccinationStatusState, error: BannerError) {
        state.public.error = error;
        state.public.status = LoadStatus.ERROR;
    },
    setStatusMessage(state: VaccinationStatusState, statusMessage: string) {
        state.public.statusMessage = statusMessage;
    },
    setAuthenticatedRequested(state: VaccinationStatusState) {
        state.authenticated.error = undefined;
        state.authenticated.status = LoadStatus.REQUESTED;
        state.authenticated.statusMessage = "";
    },
    setAuthenticatedVaccinationStatus(
        state: VaccinationStatusState,
        vaccinationStatus: VaccinationStatus
    ) {
        state.authenticated.vaccinationStatus = vaccinationStatus;
        state.authenticated.status = LoadStatus.LOADED;
        state.authenticated.statusMessage = "";
    },
    authenticatedVaccinationStatusError(
        state: VaccinationStatusState,
        error: BannerError
    ) {
        state.authenticated.vaccinationStatus = undefined;
        state.authenticated.error = error;
        state.authenticated.status = LoadStatus.ERROR;
        state.authenticated.statusMessage = "";
    },
    setAuthenticatedPdfRequested(state: VaccinationStatusState) {
        state.authenticated.error = undefined;
        state.authenticated.statusMessage = "";
    },
    authenticatedPdfError(state: VaccinationStatusState, error: BannerError) {
        state.authenticated.error = error;
        state.authenticated.status = LoadStatus.ERROR;
    },
    setAuthenticatedStatusMessage(
        state: VaccinationStatusState,
        statusMessage: string
    ) {
        state.authenticated.statusMessage = statusMessage;
    },
};
