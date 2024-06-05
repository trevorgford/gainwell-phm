import { AssessmentChoice } from "./AssessmentChoice";
import { AssessmentQuestionType } from "./AssessmentQuestionType";

export interface AssessmentQuestion {
    id?: number;
    sectionId?: number;
    description?: string;
    sortOrder?: number;
    questionType?: AssessmentQuestionType;
    parentChoice?: AssessmentChoice;
    choices?: AssessmentChoice[];
}