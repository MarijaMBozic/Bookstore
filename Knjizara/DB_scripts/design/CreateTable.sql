IF DB_ID('KnjizaraDB') IS NULL
CREATE DATABASE KnjizaraDB

GO
USE KnjizaraDB;

drop table if exists Books;
drop table if exists Genres;

create table Genres
(
	Id int identity(1,1) primary key,
	Name nvarchar(50),
	Deleted bit,
);

create table Books
(
	Id int identity(1,1) primary key,
	BookName nvarchar(50),
	Price float,
	GenreId int, 
	Deleted bit,
	foreign key (GenreId) references Genres(Id) on delete cascade
);