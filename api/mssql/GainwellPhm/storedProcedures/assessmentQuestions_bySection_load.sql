create or alter procedure assessmentQuestions_bySection_load
    @sectionId int
as

select  questionId as id,
        sectionId,
        description,
        sortOrder,
        questionTypeId
from    assessmentQuestions 
where   sectionId = @sectionId
and     active = 1;

go
