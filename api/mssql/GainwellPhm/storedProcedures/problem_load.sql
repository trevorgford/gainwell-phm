create or alter procedure problem_load
    @id int
as

select  problemId as id,
        tenantId,
        code,
        description,
        active 
from    problems 
where   problemId = @id;

go
