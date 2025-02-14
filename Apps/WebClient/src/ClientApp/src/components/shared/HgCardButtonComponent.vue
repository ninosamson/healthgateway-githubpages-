<script lang="ts">
import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";

@Component
export default class HgCardButtonComponent extends Vue {
    @Prop({ required: true }) title!: string;

    private get hasIconSlot(): boolean {
        return this.$slots.icon !== undefined;
    }

    private get hasMenuSlot(): boolean {
        return this.$slots.menu !== undefined;
    }
}
</script>

<template>
    <b-button
        class="hg-card-button h-100 w-100 p-4 d-flex flex-column align-content-start text-left rounded shadow"
        v-bind="$attrs"
        v-on="$listeners"
    >
        <b-row no-gutters align-h="end" class="mb-4 mt-n3 w-100">
            <b-col
                v-if="hasIconSlot"
                cols="auto"
                align-self="center"
                class="pr-3 mt-3 d-flex"
            >
                <slot name="icon" />
            </b-col>
            <b-col
                data-testid="card-button-title"
                class="hg-card-button-title mt-3"
            >
                {{ title }}
            </b-col>
            <b-col v-if="hasMenuSlot" cols="auto" class="mt-2">
                <slot name="menu" />
            </b-col>
        </b-row>
        <slot />
    </b-button>
</template>

<style lang="scss" scoped>
@import "@/assets/scss/_variables.scss";

.hg-card-button {
    background: $light_background;
    color: $hg-text-primary;
    border: none;
    outline-width: 0.2rem;
    outline-style: solid;
    outline-color: transparent;

    // add outline-color to transition
    transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out,
        border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out,
        outline-color 0.15s ease-in-out;

    .hg-card-button-title {
        font-size: 1.2rem;
        font-weight: bold;

        // add text-decoration-color to transition
        transition: text-decoration-color 0.15s ease-in-out;

        text-decoration: underline;
        text-decoration-color: transparent;
    }

    &:hover:not([disabled]),
    &:active:not([disabled]),
    &:focus:not([disabled]),
    &:focus-visible:not([disabled]) {
        color: $hg-text-primary;
        background: #f5f5f5;

        .hg-card-button-title {
            text-decoration-color: $hg-text-primary;
        }
    }

    &:active:not([disabled]),
    &:focus:not([disabled]),
    &:focus-visible:not([disabled]) {
        outline-width: 0.2rem;
        outline-style: solid;
        outline-color: rgba(86, 86, 86, 0.25);
    }
}
</style>
