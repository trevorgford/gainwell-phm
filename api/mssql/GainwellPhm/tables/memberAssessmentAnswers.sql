if not exists ( select * from sys.tables where name = 'memberAssessmentAnswers' )
create table memberAssessmentAnswers (
    memberAssessmentAnswerId int identity not null,
    memberAssessmentId int not null,
    questionId int not null,
    choiceId int null,
    answerText varchar(200) null,
    createdTimestamp datetime2 not null,
    modifiedTimestamp datetime2 not null,
    constraint pk_memberAssessmentAnswers primary key ( memberAssessmentAnswerId ),
    constraint fk_memberAssessmentAnswers_memberAssessments foreign key ( memberAssessmentId ) references memberAssessments ( memberAssessmentId ),
    constraint fk_memberAssessmentAnswers_questions foreign key ( questionId ) references assessmentQuestions ( questionId ),
    constraint fk_memberAssessmentAnswers_choices foreign key ( choiceId ) references assessmentChoices ( choiceId )
);
go
