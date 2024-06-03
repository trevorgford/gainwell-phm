if not exists ( select * from sys.tables where name = 'assessmentSectionInstructions' )
create table assessmentSectionInstructions (
    sectionInstructionId int identity not null,
    sectionId int not null,
    description varchar(500) not null,
    active bit not null constraint df_assessmentSectionInstructions_active default 1,
    constraint pk_assessmentSectionInstructions primary key ( sectionInstructionId ),
    constraint fk_assessmentSectionInstructions_assessmentSections foreign key ( sectionId ) references assessmentSections ( sectionId )
);
go
