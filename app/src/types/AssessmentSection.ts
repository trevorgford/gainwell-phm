import { AssessmentQuestion } from "./AssessmentQuestion";

export interface AssessmentSection {
    id?: number;
    description?: string;
    assessmentId?: number;
    sortOrder?: number;
    questions?: AssessmentQuestion[];
}