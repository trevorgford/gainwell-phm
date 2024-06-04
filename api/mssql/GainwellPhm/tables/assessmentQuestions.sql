if not exists ( select * from sys.tables where name = 'assessmentQuestions' )
create table assessmentQuestions (
    questionId int identity not null,
    sectionId int not null,
    questionTypeId int not null,
    parentChoiceId int null,
    description varchar(250) not null,
    sortOrder int not null,
    active bit not null constraint df_assessmentQuestions_active default 1,
    constraint pk_assessmentQuestions primary key ( questionId ),
    constraint fk_assessmentQuestions_assessmentSections foreign key ( sectionId ) references assessmentSections ( sectionId ),
    constraint fk_assessmentQuestions_assessmentQuestionTypes foreign key ( questionTypeId ) references assessmentQuestionTypes ( questionTypeId )--,
    --constraint fk_assessmentQuestions_assessmentChoices foreign key ( parentChoiceId ) references assessmentChoices ( choiceId )
);
go
