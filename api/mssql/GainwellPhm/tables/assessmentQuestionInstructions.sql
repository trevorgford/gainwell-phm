if not exists ( select * from sys.tables where name = 'assessmentQuestionInstructions' )
create table assessmentQuestionInstructions (
    questionInstructionId int identity not null,
    questionId int not null,
    description varchar(500) not null,
    active bit not null constraint df_assessmentQuestionInstructions_active default 1,
    constraint pk_assessmentQuestionInstructions primary key ( questionInstructionId ),
    constraint fk_assessmentQuestionInstructions_assessmentQuestions foreign key ( questionId ) references assessmentQuestions ( questionId )
);
go
