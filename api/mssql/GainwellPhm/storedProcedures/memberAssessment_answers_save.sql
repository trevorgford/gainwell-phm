create or alter procedure memberAssessment_answers_save
    @id int,
    @answers selectedAnswersTableType readonly,
    @userId int
as

declare @d datetime2 = sysutcdatetime();

delete from memberAssessmentAnswers where memberAssessmentId = @id;

insert into memberAssessmentAnswers ( memberAssessmentId, questionId, choiceId, answerText, createdTimestamp, modifiedTimestamp )
select      @id, a.questionId, a.choiceId, a.answerText, @d, @d
from        @answers a;

go
