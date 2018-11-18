create table Services
(
	RecordID bigint identity(1,1) not null,
	Token varchar(32) not null unique,
	Version varchar(16) not null,
	Description varchar(max),
	constraint PK_Services primary key clustered
	(
		RecordID ASC
	)
	with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 10) on [primary]
) on [primary]

----- ##### ----- ##### ----- ##### ----- ##### ----- ##### ----- ##### ----- ##### -----

create table Toggles
(
	RecordID bigint identity(1,1) not null,
	Name varchar(32) not null,
	Value varchar(16) not null,
	constraint PK_Toggles primary key clustered
	(
		RecordID ASC
	)
	with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 10) on [primary]
) on [primary]

----- ##### ----- ##### ----- ##### ----- ##### ----- ##### ----- ##### ----- ##### -----

create table ServiceToggles
(
	RecordID bigint identity(1,1) not null,
	Name varchar(32) not null,
	Value varchar(16) not null,
	constraint PK_Toggles primary key clustered
	(
		RecordID ASC
	)
	with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 10) on [primary]
) on [primary]

----- ##### ----- ##### ----- ##### ----- ##### ----- ##### ----- ##### ----- ##### -----

create table Roles
(
	RecordID bigint identity(1,1) not null,
	Role varchar(32) not null unique,
	Description varchar(max),
	constraint PK_Roles primary key clustered
	(
		RecordID ASC
	)
	with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 10) on [primary]
) on [primary]

----- ##### ----- ##### ----- ##### ----- ##### ----- ##### ----- ##### ----- ##### -----

create table Users
(
	RecordID bigint identity(1,1) not null,
	Username varchar(32) not null unique,
	Password varchar(max),
	Role varchar (32),
	constraint PK_Users primary key clustered
	(
		RecordID ASC
	)
	with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 10) on [primary]
) on [primary]

alter table Users with check add constraint [FK_Users_Role] foreign key(Role)
references Roles (Role)