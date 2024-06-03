create or alter procedure carePlan_create
    @memberId int,
    @problemId int,
    @carePlanName varchar(100) = null,
    @goalId int,
    @interventions idsTableType readonly,
    @barriers idsTableType readonly,
    @userId int,
    @tenantId int
as

declare @carePlanId int,
        @carePlanGoalId int,
        @initialGoalStatusId int = ( select top 1 goalStatusId from goalStatuses where initial = 1 and tenantId = @tenantId and active = 1 ),
        @initialInterventionStatusId int = ( select top 1 interventionStatusId from interventionStatuses where initial = 1 and tenantId = @tenantId and active = 1 );

insert into carePlans ( memberId, problemId, carePlanName )
values      ( @memberId, @problemId, @carePlanName );

set @carePlanId = scope_identity();

if @goalId is not null begin

    insert into carePlanGoals ( carePlanId, goalId, goalStatusId )
    values      ( @carePlanId, @goalId, @initialGoalStatusId );

    set @carePlanGoalId = scope_identity();

    if exists ( select * from @interventions )
    insert into carePlanInterventions ( carePlanGoalId, interventionId, interventionStatusId )
    select      @carePlanGoalId, 
                id, 
                @initialInterventionStatusId
    from        @interventions;

    if exists ( select * from @barriers )
    insert into carePlanBarriers ( carePlanGoalId, barrierId )
    select      @carePlanGoalId, 
                id
    from        @barriers;

end

go
