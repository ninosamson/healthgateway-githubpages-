import { GetterTree } from "vuex";

import { Dictionary } from "@/models/baseTypes";
import { CommentState, LoadStatus, RootState } from "@/models/storeState";
import { UserComment } from "@/models/userComment";

export const getters: GetterTree<CommentState, RootState> = {
    comments(state: CommentState): Dictionary<UserComment[]> {
        return state.profileComments;
    },
    getEntryComments: (state: CommentState) => (
        entryId: string
    ): UserComment[] | null => {
        if (state.profileComments[entryId] !== undefined) {
            return state.profileComments[entryId];
        } else {
            return null;
        }
    },
    entryHasComments: (state: CommentState) => (entryId: string): boolean => {
        return entryId in state.profileComments;
    },
    isLoading(state: CommentState): boolean {
        return state.status === LoadStatus.REQUESTED;
    },
};