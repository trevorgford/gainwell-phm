if not exists ( select * from sys.tables where name = 'carePlanInterventions' )
create table carePlanInterventions (
    carePlanInterventionId int identity not null,
    carePlanGoalId int not null,
    interventionId int not null,
    interventionStatusId int not null,
    dueDate datetime2 null,
    active bit not null constraint df_carePlanInterventions_active default 1,
    constraint pk_carePlanInterventions primary key ( carePlanInterventionId ),
    constraint fk_carePlanInterventions_carePlanGoals foreign key ( carePlanGoalId ) references carePlanGoals ( carePlanGoalId ),
    constraint fk_carePlanInterventions_interventions foreign key ( interventionId ) references interventions ( interventionId ),
    constraint fk_carePlanInterventions_interventionStatuses foreign key ( interventionStatusId ) references interventionStatuses ( interventionStatusId )
);
go
