create or alter procedure assessmentQuestion_create
    @sectionId int,
    @questionTypeId int,
    @parentChoiceId int = null,
    @description varchar(250),
    @sortOrder int,
    @userId int
as

insert into assessmentQuestions ( sectionId, questionTypeId, parentChoiceId, description, sortOrder )
values      ( @sectionId, @questionTypeId, @parentChoiceId, @description, @sortOrder );

go
