<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

import { CovidTreatmentAssessmentOption } from "@/constants/covidTreatmentAssessmentOption";

@Component
export default class OptionDetails extends Vue {
    @Prop({ required: true }) value!: CovidTreatmentAssessmentOption;
    @Prop({ required: false, default: false }) hasNotSureOption!: boolean;
    @Prop({ required: false, default: false })
    responseOfNoIndicatesNoBenefit!: boolean;
    @Prop({ required: false, default: false })
    responseOfYesIndicatesBenefit!: boolean;
    @Prop({ required: false, default: false })
    responseOfYesIndicatesNoBenefit!: boolean;

    private get options(): CovidTreatmentAssessmentOption[] {
        let options = [
            CovidTreatmentAssessmentOption.Yes,
            CovidTreatmentAssessmentOption.No,
        ];
        if (this.hasNotSureOption) {
            options.push(CovidTreatmentAssessmentOption.NotSure);
        }
        return options;
    }

    private getLabel(option: CovidTreatmentAssessmentOption): string {
        switch (option) {
            case CovidTreatmentAssessmentOption.Yes:
                return "Yes";
            case CovidTreatmentAssessmentOption.No:
                return "No";
            case CovidTreatmentAssessmentOption.NotSure:
                return "Not Sure";
            default:
                return CovidTreatmentAssessmentOption.Unspecified;
        }
    }

    private get hasSelectedNo(): boolean {
        return this.value === CovidTreatmentAssessmentOption.No;
    }

    private get hasSelectedYes(): boolean {
        return this.value === CovidTreatmentAssessmentOption.Yes;
    }

    private get showMessageWithBenefit(): boolean {
        return this.responseOfYesIndicatesBenefit && this.hasSelectedYes;
    }

    private get showMessageWithNoBenefit(): boolean {
        return (
            (this.responseOfNoIndicatesNoBenefit && this.hasSelectedNo) ||
            (this.responseOfYesIndicatesNoBenefit && this.hasSelectedYes)
        );
    }

    private optionChange(value: CovidTreatmentAssessmentOption) {
        this.$emit("update:value", value);
    }
}
</script>

<template>
    <div>
        <v-radio-group :value="value" row>
            <v-radio
                v-for="(option, index) in options"
                :key="index"
                :label="getLabel(option)"
                :value="option"
                @change="optionChange(option)"
            />
        </v-radio-group>
        <div v-if="showMessageWithNoBenefit" class="option-message-color">
            Citizen would likely not benefit from COVID-19 treatment.
        </div>
        <div v-if="showMessageWithBenefit" class="option-message-color">
            Citizen may benefit from COVID-19 treatment.
        </div>
    </div>
</template>
<style scoped lang="scss">
.option-message-color {
    color: #ff9800;
}
</style>
