if not exists ( select * from sys.tables where name = 'assessmentQuestionTypes' )
create table assessmentQuestionTypes (
    questionTypeId int identity not null,
    description varchar(50) not null,
    active bit not null constraint df_assessmentQuestionTypes_active default 1,
    constraint pk_assessmentQuestionTypes primary key ( questionTypeId )
);
go
