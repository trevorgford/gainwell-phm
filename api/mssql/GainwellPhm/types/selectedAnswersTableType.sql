if not exists ( select * from sys.types where name = 'selectedAnswersTableType' )
create type selectedAnswersTableType as table (
    questionId int,
    choiceId int,
    answerText varchar(200)
);
