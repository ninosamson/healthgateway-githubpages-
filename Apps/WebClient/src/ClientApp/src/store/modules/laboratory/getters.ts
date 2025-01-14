import { CustomBannerError } from "@/models/errors";
import {
    Covid19LaboratoryOrder,
    LaboratoryOrder,
    PublicCovidTestResponseResult,
} from "@/models/laboratory";
import { LoadStatus } from "@/models/storeOperations";

import { LaboratoryGetters, LaboratoryState } from "./types";

export const getters: LaboratoryGetters = {
    covid19LaboratoryOrders(state: LaboratoryState): Covid19LaboratoryOrder[] {
        return state.authenticatedCovid19.laboratoryOrders;
    },
    covid19LaboratoryOrdersCount(state: LaboratoryState): number {
        return state.authenticatedCovid19.laboratoryOrders.length;
    },
    covid19LaboratoryOrdersAreLoading(state: LaboratoryState): boolean {
        return state.authenticatedCovid19.status === LoadStatus.REQUESTED;
    },
    laboratoryOrders(state: LaboratoryState): LaboratoryOrder[] {
        return state.authenticated.laboratoryOrders;
    },
    laboratoryOrdersCount(state: LaboratoryState): number {
        return state.authenticated.laboratoryOrders.length;
    },
    laboratoryOrdersAreLoading(state: LaboratoryState): boolean {
        return state.authenticated.status === LoadStatus.REQUESTED;
    },
    laboratoryOrdersAreQueued(state: LaboratoryState): boolean {
        return state.authenticated.queued;
    },
    publicCovidTestResponseResult(
        state: LaboratoryState
    ): PublicCovidTestResponseResult | undefined {
        return state.publicCovid19.publicCovidTestResponseResult;
    },
    isPublicCovidTestResponseResultLoading(state: LaboratoryState): boolean {
        return state.publicCovid19.status === LoadStatus.REQUESTED;
    },
    publicCovidTestResponseResultError(
        state: LaboratoryState
    ): CustomBannerError | undefined {
        return state.publicCovid19.error;
    },
    publicCovidTestResponseResultStatusMessage(state: LaboratoryState): string {
        return state.publicCovid19.statusMessage;
    },
};
