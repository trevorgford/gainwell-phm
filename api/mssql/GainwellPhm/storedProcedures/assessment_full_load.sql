create or alter procedure assessment_full_load
    @assessmentId int
as

select      assessmentId as id,
            versionName,
            description
from        assessments 
where       assessmentId = @assessmentId;

select      sectionId as id,
            description,
            sortOrder 
from        assessmentSections 
where       assessmentId = @assessmentId
and         active = 1;

select      q.questionId as id,
            q.sectionId,
            q.description,
            q.sortOrder,
            q.questionTypeId 
from        assessmentSections s
inner join  assessmentQuestions q               on  s.sectionId = q.sectionId
where       s.assessmentId = @assessmentId
and         s.active = 1
and         q.active = 1;

select      c.choiceId as id,
            c.questionId,
            c.description,
            c.sortOrder,
            c.score 
from        assessmentSections s
inner join  assessmentQuestions q               on  s.sectionId = q.sectionId
inner join  assessmentChoices c                 on  q.questionId = c.questionId        
where       s.assessmentId = @assessmentId
and         s.active = 1
and         q.active = 1
and         c.active = 1;

go
