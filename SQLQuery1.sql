use joyeriadb

create database InterviewDB;
go

use InterviewDB;

create table [dbo].[User]
(
	UserId		int identity(1, 1) primary key,
	Name		varchar(50),
	Lastname	varchar(100),
	Age			int
);
go

select * from [dbo].[User]
