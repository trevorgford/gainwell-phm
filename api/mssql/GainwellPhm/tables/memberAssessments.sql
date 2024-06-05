if not exists ( select * from sys.tables where name = 'memberAssessments' )
create table memberAssessments (
    memberAssessmentId int identity not null,
    memberId int not null,
    assessmentId int not null,
    active bit not null constraint df_memberAssessments_active default 1,
    createdTimestamp datetime2 not null,
    modifiedTimestamp datetime2 not null,
    completedTimestamp datetime2 null,
    constraint pk_memberAssessments primary key ( memberAssessmentId ),
    constraint fk_memberAssessments_members foreign key ( memberId ) references members ( memberId ),
    constraint fk_memberAssessments_assessments foreign key ( assessmentId ) references assessments ( assessmentId )
);
go
