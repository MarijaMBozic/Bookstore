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
    public class GenreRepo : IGenreRepository
    {
        public string ConnectionString { get; set; }

        public GenreRepo()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["KnjizaraDBContext"].ConnectionString;
        }

        public int Create(Genre genre)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Insert into Genres(Name, Deleted) Values (@Name, @Deleted);";
                        cmd.CommandText += "select scope_identity()";
                        cmd.Parameters.AddWithValue("@Name", genre.Name);
                        cmd.Parameters.AddWithValue("@Deleted", genre.Deleted);

                        conn.Open();
                        var newId = cmd.ExecuteScalar();
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
                        cmd.CommandText = "Delete from Genres where Id=@id";
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
                        cmd.CommandText = "update Genres set Deleted=1 where Id=@id ";
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
        public IEnumerable<Genre> GetAll()
        {
            List<Genre> listGenre = new List<Genre>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select*from Genres;";

                        //DataSet ds = new DataSet();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            int idGenre = (int)dr[0];
                            string nameGanre = (string)dr[1];
                            bool deletedGenre = (bool)dr[2];

                            Genre genre = new Genre(idGenre, nameGanre, deletedGenre);
                            listGenre.Add(genre);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return listGenre;
        }

        public Genre GetById(int id)
        {
            Genre genreDetail = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {                 
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select*from Genres where id=" + id;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        conn.Close();

                        if (dt.Rows.Count == 1)
                        {
                            DataRow dr = dt.Rows[0];
                            int idGenre = (int)dr[0];
                            string nameGanre = (string)dr[1];
                            bool deletedGenre = (bool)dr[2];

                            genreDetail = new Genre(idGenre, nameGanre, deletedGenre);
                        }
                    }
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return genreDetail;
        }

        public bool Update(Genre genre)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update Genres set Name = @Name where Id = @id";
                        cmd.Parameters.AddWithValue("@Id", genre.Id);
                        cmd.Parameters.AddWithValue("@Name", genre.Name);

                        conn.Open();
                        var updatRow= cmd.ExecuteNonQuery();
                        conn.Close();

                        if(updatRow==1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return false;
        }

    }
}