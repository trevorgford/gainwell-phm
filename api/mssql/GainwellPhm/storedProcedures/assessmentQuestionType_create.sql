create or alter procedure assessmentQuestionType_create
    @description varchar(50),
    @userId int
as

if not exists ( select * from assessmentQuestionTypes where description = @description )
insert into assessmentQuestionTypes ( description )
values      ( @description );

go
