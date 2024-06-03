create or alter procedure carePlan_goal_add
    @carePlanId int,
    @goalId int,
    @userId int,
    @tenantId int
as

declare @initialGoalStatusId int = ( select top 1 goalStatusId from goalStatuses where initial = 1 and tenantId = @tenantId and active = 1 );

insert into carePlanGoals ( carePlanId, goalId, goalStatusId )
values      ( @carePlanId, @goalId, @initialGoalStatusId );

go
