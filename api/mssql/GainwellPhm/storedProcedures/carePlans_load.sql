create or alter procedure carePlans_load
    @memberId int
as

select      carePlanId,
            carePlanName
from        carePlans
where       memberId = @memberId
and         active = 1; 

select      cp.carePlanId,
            p.problemId,
            p.description
from        carePlans cp
inner join  problems p                  on  cp.problemId = p.problemId
where       memberId = @memberId
and         cp.active = 1;

select      cpg.carePlanId,
            cpg.carePlanGoalId,
            g.goalId,
            g.description,
            gs.description as goalStatus,
            cpg.dueDate
from        carePlans cp
inner join  carePlanGoals cpg           on  cp.carePlanId = cpg.carePlanId
inner join  goals g                     on  cpg.goalId = g.goalId
inner join  goalStatuses gs             on  cpg.goalStatusId = gs.goalStatusId
where       cp.memberId = @memberId
and         cpg.active = 1;

select      cpg.carePlanGoalId,
            cpi.interventionId,
            i.description,
            s.description as interventionStatus
from        carePlans cp
inner join  carePlanGoals cpg           on  cp.carePlanId = cpg.carePlanId
inner join  carePlanInterventions cpi   on  cpg.carePlanGoalId = cpi.carePlanGoalId
inner join  interventions i             on  cpi.interventionId = i.interventionId
inner join  interventionStatuses s      on  cpi.interventionStatusId = s.interventionStatusId
where       cp.memberId = @memberId
and         cpg.active = 1
and         cpi.active = 1;

select      cpg.carePlanGoalId,
            cpb.barrierId,
            b.description
from        carePlans cp
inner join  carePlanGoals cpg           on  cp.carePlanId = cpg.carePlanId
inner join  carePlanBarriers cpb        on  cpg.carePlanGoalId = cpb.carePlanGoalId
inner join  barriers b                  on  cpb.barrierId = b.barrierId
where       cp.memberId = @memberId
and         cpg.active = 1
and         cpb.active = 1;

go
