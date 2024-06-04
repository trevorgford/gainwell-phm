create or alter procedure assessmentQuestionTypes_load

as

select  questionTypeId as id,
        description
from    assessmentQuestionTypes
where   active = 1;

go
