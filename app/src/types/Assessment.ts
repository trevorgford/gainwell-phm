import { AssessmentSection } from "./AssessmentSection";

export interface Assessment {
    id?: number;
    description?: string;
    versionName?: string;
    sections?: AssessmentSection[];
}