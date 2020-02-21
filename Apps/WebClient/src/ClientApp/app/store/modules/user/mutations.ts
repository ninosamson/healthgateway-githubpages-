import Vue from "vue";
import { MutationTree } from "vuex";
import { User as OidcUser } from "oidc-client";
import { StateType, UserState } from "@/models/storeState";
import PatientData from "@/models/patientData";
import User from "@/models/user";
import UserProfile from "@/models/userProfile";
import UserEmailInvite from "@/models/userEmailInvite";

export const mutations: MutationTree<UserState> = {
  setOidcUserData(state: UserState, oidcUser: OidcUser) {
    Vue.set(state.user, "hdid", oidcUser.profile.hdid);
    state.error = false;
    state.statusMessage = "success";
    state.stateType = StateType.INITIALIZED;
  },
  setPatientData(state: UserState, patientData: PatientData) {
    Vue.set(state.user, "phn", patientData.personalHealthNumber);
    state.error = false;
    state.statusMessage = "success";
    state.stateType = StateType.INITIALIZED;
  },
  setProfileUserData(state: UserState, userProfile: UserProfile) {
    Vue.set(
      state.user,
      "acceptedTermsOfService",
      userProfile ? userProfile.acceptedTermsOfService : false
    );
    Vue.set(
      state.user,
      "hasTermsOfServiceUpdated",
      userProfile ? userProfile.hasTermsOfServiceUpdated : false
    );
    state.error = false;
    state.statusMessage = "success";
    state.stateType = StateType.INITIALIZED;
  },
  setValidatedEmail(state: UserState, userEmailInvite: UserEmailInvite) {
    if (userEmailInvite) {
      Vue.set(state.user, "hasEmail", true);
      Vue.set(state.user, "verifiedEmail", userEmailInvite.validated);
    } else {
      Vue.set(state.user, "hasEmail", false);
      Vue.set(state.user, "verifiedEmail", false);
    }

    state.error = false;
    state.statusMessage = "success";
    state.stateType = StateType.INITIALIZED;
  },
  clearUserData(state: UserState) {
    state.user = new User();
    state.error = false;
    state.statusMessage = "success";
    state.stateType = StateType.INITIALIZED;
  },
  userError(state: UserState, errorMessage: string) {
    state.error = true;
    state.statusMessage = errorMessage;
    state.stateType = StateType.ERROR;
  }
};
