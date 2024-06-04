create or alter procedure assessmentSections_load
    @assessmentId int
as

select  sectionId as id,
        sectionId,
        description,
        sortOrder
from    assessmentSections
where   assessmentId = @assessmentId
and     active = 1;

go
