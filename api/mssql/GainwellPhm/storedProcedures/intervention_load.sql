create or alter procedure intervention_load
    @id int
as

select  interventionId as id,
        tenantId,
        code,
        description,
        active 
from    interventions 
where   interventionId = @id;

go
