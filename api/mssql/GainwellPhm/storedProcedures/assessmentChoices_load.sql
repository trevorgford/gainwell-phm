create or alter procedure assessmentChoices_load
    @assessmentId int
as

select      c.choiceId as id,
            c.questionId,
            c.description,
            c.sortOrder,
            c.score
from        assessmentSections s
inner join  assessmentQuestions q           on s.sectionId = q.sectionId
inner join  assessmentChoices c             on q.questionId = c.questionId
where       s.assessmentId = @assessmentId
and         s.active = 1
and         q.active = 1
and         c.active = 1;

go
