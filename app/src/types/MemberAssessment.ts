import { Assessment } from "./Assessment";
import { Member } from "./Member";
import { MemberAssessmentAnswer } from "./MemberAssessmentAnswer";

export interface MemberAssessment {
    id: number;
    member: Member;
    assessment: Assessment;
    answers?: MemberAssessmentAnswer[];
    createdTimestamp: Date;
    modifiedTimestamp: Date;
    completedTimestamp: Date;
}