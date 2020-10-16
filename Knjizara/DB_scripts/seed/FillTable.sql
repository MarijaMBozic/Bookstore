USE KnjizaraDB;
insert into Genres (Name, Deleted) 
            values ('Science', 0),
				   ('Comedy', 0),
			       ('Horror', 0)

insert into Books (BookName, Price, GenreId, Deleted) 
           values ('Roman', 1500.8, 1, 0),
				  ('Drama', 950, 1, 0),
				  ('Horor', 750, 3, 0),
				  ('Komedija', 150.8, 2, 0)
				   