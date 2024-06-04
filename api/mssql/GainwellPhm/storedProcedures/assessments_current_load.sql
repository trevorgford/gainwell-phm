create or alter procedure assessments_current_load
    @tenantId int
as

select  assessmentId as id,
        versionName,
        description
from    assessments
where   tenantId = @tenantId
and     isCurrent = 1
and     active = 1;

go
