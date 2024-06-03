if not exists ( select * from sys.types where name = 'idsTableType' )
create type idsTableType as table (
    id int
);
