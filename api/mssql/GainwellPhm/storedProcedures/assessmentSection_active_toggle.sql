create or alter procedure assessmentSection_active_toggle
    @sectionId int,
    @active bit
as

update assessmentSections set active = @active where sectionId = @sectionId;

go
