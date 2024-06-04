create or alter procedure assessmentQuestions_load
    @assessmentId int
as

select      q.questionId as id,
            q.sectionId,
            q.description,
            q.sortOrder,
            q.questionTypeId
from        assessmentSections s
inner join  assessmentQuestions q               on s.sectionId = q.sectionId
where       s.assessmentId = @assessmentId
and         s.active = 1
and         q.active = 1;

go
