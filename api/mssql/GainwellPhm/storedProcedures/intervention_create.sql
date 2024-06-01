create or alter procedure intervention_create
    @tenantId int,
    @code varchar(50) = null,
    @description varchar(50)
as

insert into interventions ( tenantId, code, description )
values      ( @tenantId, @code, @description );

go
