create or alter procedure assessmentSection_create
    @assessmentId int,
    @description varchar(100),
    @sortOrder int,
    @userId int
as

insert into assessmentSections ( assessmentId, description, sortOrder )
values      ( @assessmentId, @description, @sortOrder );

go
