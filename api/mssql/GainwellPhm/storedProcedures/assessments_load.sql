create or alter procedure assessments_load
    @tenantId int
as

select  assessmentId as id,
        versionName,
        code,
        description,
        isCurrent
from    assessments 
where   tenantId = @tenantId
and     active = 1;

go
