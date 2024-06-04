create or alter procedure assessmentChoice_create
    @questionId int,
    @description nvarchar(255),
    @sortOrder int,
    @score decimal(15, 5) = null,
    @userId int
as

insert into assessmentChoices ( questionId, description, sortOrder, score )
values      ( @questionId, @description, @sortOrder, @score );

go
