create or alter procedure assessmentChoices_ByQuestion_load
    @questionId int
as

select  choiceId as id,
        questionId,
        description,
        sortOrder,
        score
from    assessmentChoices 
where   questionId = @questionId
and     active = 1;

go
