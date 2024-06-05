import { AssessmentChoice } from "./AssessmentChoice";
import { AssessmentQuestion } from "./AssessmentQuestion";
import { MemberAssessment } from "./MemberAssessment";

export interface MemberAssessmentAnswer {
    id?: number;
    memberAssessment?: MemberAssessment;
    question?: AssessmentQuestion;
    choice?: AssessmentChoice;
    answerText?: string;
    score?: number;
    createdTimestamp?: Date;
    modifiedTimestamp?: Date;
    questionId?: number;
    choiceId?: number;
    choiceIds?: number[];
    answer?: string;
}