if not exists ( select * from sys.tables where name = 'assessmentSections' )
create table assessmentSections (
    sectionId int identity not null,
    assessmentId int not null,
    description varchar(100) not null,
    sortOrder int not null,
    active bit not null constraint df_assessmentSections_active default 1,
    constraint pk_assessmentSections primary key ( sectionId ),
    constraint fk_assessmentSections_assessments foreign key ( assessmentId ) references assessments ( assessmentId )
);
go
