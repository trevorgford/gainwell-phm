if not exists ( select * from sys.tables where name = 'assessmentInstructions' )
create table assessmentInstructions (
    assessmentInstructionId int identity not null,
    assessmentId int not null,
    description varchar(500) not null,
    active bit not null constraint df_assessmentInstructions_active default 1,
    constraint pk_assessmentInstructions primary key ( assessmentInstructionId ),
    constraint fk_assessmentInstructions_assessments foreign key ( assessmentId ) references assessments ( assessmentId )
);
go
