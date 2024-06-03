if not exists ( select * from sys.tables where name = 'assessmentChoices' )
create table assessmentChoices (
    choiceId int identity not null,
    questionId int not null,
    description varchar(200) not null,
    sortOrder int not null,
    score decimal(15, 5) null,
    active bit not null constraint df_assessmentChoices_active default 1,
    constraint pk_assessmentChoices primary key ( choiceId ),
    constraint fk_assessmentChoices_assessmentQuestions foreign key ( questionId ) references assessmentQuestions ( questionId )
);
go
