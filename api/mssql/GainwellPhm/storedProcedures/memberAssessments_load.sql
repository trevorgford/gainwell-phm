create or alter procedure memberAssessments_load
    @memberId int
as

select      m.memberAssessmentId as id,
            m.assessmentId,
            m.memberId,
            a.versionName,
            a.description,
            m.createdTimestamp,
            m.modifiedTimestamp,
            m.completedTimestamp
from        memberAssessments m
inner join  assessments a               on  m.assessmentId = a.assessmentId
where       memberId = @memberId
and         m.active = 1;

go
