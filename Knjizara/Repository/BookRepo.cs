using Knjizara.Models;
using Knjizara.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Knjizara.Repository
{
    public class BookRepo : IBookRepository
    {
        public string ConnectionString { get; set; }

        public IGenreRepository genreRepo = new GenreRepo();
        public BookRepo()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["KnjizaraDBContext"].ConnectionString;
        }

        public int Create(Book book)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Insert into Books(BookName, Price, GenreId, Deleted) Values (@BookName, @Price, @GenreId, @Deleted);";
                        cmd.CommandText += "SELECT SCOPE_IDENTITY()";
                        cmd.Parameters.AddWithValue("@BookName", book.Name);
                        cmd.Parameters.AddWithValue("@Price", book.Price);
                        cmd.Parameters.AddWithValue("@GenreId", book.Genre.Id);
                        cmd.Parameters.AddWithValue("@Deleted", book.Deleted);

                        conn.Open();
                        var newId = cmd.ExecuteScalar();
                        conn.Close();

                        if (newId != null)
                        {
                            return int.Parse(newId.ToString());
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return -1;
        }

        public bool DeleteActual(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Delete from Books where Id=@id";
                        cmd.Parameters.AddWithValue("@Id", id);

                        conn.Open();
                        var delRows = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (delRows == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update Books set Deleted=1 where Id=@id ";
                        cmd.Parameters.AddWithValue("@Id", id);

                        conn.Open();
                        var delRows = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (delRows == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return false;
        }

        public IEnumerable<Book> GetAll()
        {
            List<Book> listaKnjiga = new List<Book>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select Books.Id, Books.BookName, Books.Price, Genres.Id, Books.Deleted from Books left join Genres on Books.GenreId = Genres.Id";
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        conn.Close();

                        foreach (DataRow dr in dt.Rows)
                        {
                            int idBook = (int)dr[0];
                            string nameBook = (string)dr[1];
                            double priceBook = (double)dr[2];
                            int genBookId = (int)dr[3];
                            Genre gen = genreRepo.GetById(genBookId);
                            bool deletBook = (bool)dr[4];

                            Book book = new Book(idBook, nameBook, priceBook, gen, deletBook);
                            listaKnjiga.Add(book);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return listaKnjiga;
        }

        public Book GetById(int id)
        {
            Book book = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select * from Books where Books.Id=@Id";
                        cmd.Parameters.AddWithValue("@Id", id);
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        conn.Close();
                        if (dt.Rows.Count == 1)
                        {
                            DataRow dr = dt.Rows[0];
                            int idBook = (int)dr[0];
                            string nameBook = (string)dr[1];
                            double priceBook = (double)dr[2];
                            int genBookId = (int)dr[3];
                            Genre gen = genreRepo.GetById(genBookId);
                            bool deletBook = (bool)dr[4];

                            return book = new Book(idBook, nameBook, priceBook, gen, deletBook);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return book;
        }

        public bool Update(Book book)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update Books set BookName = @BookName, Price=@Price, GenreId=@genreId where Id=@Id";
                        cmd.Parameters.AddWithValue("@Id", book.Id);
                        cmd.Parameters.AddWithValue("@BookName", book.Name);
                        cmd.Parameters.AddWithValue("@Price", book.Price);
                        cmd.Parameters.AddWithValue("@GenreId", book.Genre.Id);

                        conn.Open();
                        var updateBook = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (updateBook == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        public List<Book> Search(int id)
        {
            List<Book> listBook = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select Books.Id, Books.BookName, Books.Price, Genres.Id, Books.Deleted from Books left join Genres on Books.GenreId=Genres.Id where Genres.Id=@id";
                        cmd.Parameters.AddWithValue("@Id", id);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        conn.Close();

                        foreach (DataRow dr in dt.Rows)
                        {
                            int idBook = (int)dr[0];
                            string nameBook = (string)dr[1];
                            double priceBook = (double)dr[2];
                            int genBookId = (int)dr[3];
                            Genre gen = genreRepo.GetById(genBookId);
                            bool deletBook = (bool)dr[4];

                            Book book = new Book(idBook, nameBook, priceBook, gen, deletBook);
                            listBook.Add(book);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return listBook;
        }

        public Book SearchByNAme(string name)
        {
            Book listBook = new Book();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select Books.Id, Books.BookName, Books.Price, Genres.Id, Books.Deleted from Books left join Genres on Books.GenreId=Genres.Id where Books.BookName=@BookName";
                        cmd.Parameters.AddWithValue("@BookName", name);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        conn.Close();

                        foreach (DataRow dr in dt.Rows)
                        {
                            int idBook = (int)dr[0];
                            string nameBook = (string)dr[1];
                            double priceBook = (double)dr[2];
                            int genBookId = (int)dr[3];
                            Genre gen = genreRepo.GetById(genBookId);
                            bool deletBook = (bool)dr[4];

                            listBook = new Book(idBook, nameBook, priceBook, gen, deletBook);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return listBook;
        }
    }
}
