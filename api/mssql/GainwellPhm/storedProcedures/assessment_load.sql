create or alter procedure assessment_load
    @id int
as

select  assessmentId as id,
        tenantId,
        versionName,
        code,
        description,
        isCurrent,
        active
from    assessments
where   assessmentId = @id;

go
