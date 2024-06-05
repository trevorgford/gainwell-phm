create or alter procedure memberAssessment_load
    @id int
as

select      memberAssessmentId as id,
            createdTimestamp,
            modifiedTimestamp,
            completedTimestamp,
            assessmentId,
            memberId
from        memberAssessments 
where       memberAssessmentId = @id;

select  memberAssessmentAnswerId as id,
        questionId,
        choiceId,
        answerText
from    memberAssessmentAnswers
where   memberAssessmentId = @id;

go
