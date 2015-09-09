using System;
using System.Collections.Generic;
using System.Configuration;
using
System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DieChart.Entity
{
    public class DataAccessLayer
    {
        public IEnumerable<DieEntity>  SearchDieEntity(string SearchTerm)
        {
           
            var connString = ConfigurationManager.ConnectionStrings["DB_9B1091_generalEntities"].ConnectionString;
            List<DieEntity> DieEntity = new List<DieEntity>();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var cmd = new SqlCommand("select * from die where " + 
                        " code like @SearchTerm or " +
                        "Description like @SearchTerm or " +
                        "Id1 like @SearchTerm or " +
                        "Id2 like @SearchTerm or " +
                        "ID1_ID2 like @SearchTerm or " +
                        "Length like @SearchTerm or " +
                        "Price_val like @SearchTerm  " + 
                        "order by ID desc", conn);
                cmd.Parameters.AddWithValue("@SearchTerm", String.Format("%{0}%", SearchTerm));

                var reader = cmd.ExecuteReader();

                int currentPersonID = -1;
                DieEntity currentInstructor = null;
                while (reader.Read())
                {
                    var personID = Convert.ToInt32(reader["id"]);
                    if (personID != currentPersonID)
                    {
                        currentPersonID = personID;
                        if (currentInstructor != null)
                        {
                            DieEntity.Add(currentInstructor);
                        }
                        currentInstructor = new DieEntity();
                        currentInstructor.Code = reader["Code"].ToString();
                        currentInstructor.Description = reader["Description"].ToString();
                        currentInstructor.Id1 = reader["Id1"].ToString();
                        currentInstructor.id = Convert.ToInt32(reader["id"]);
                        currentInstructor.ID1_ID2 = reader["ID1_ID2"].ToString();
                        currentInstructor.Id2 = reader["Id2"].ToString();
                        currentInstructor.Length = reader["Length"].ToString();
                        currentInstructor.Price_val = Convert.ToInt32(reader["Price_val"]);

                    }

                }
                if (currentInstructor != null)
                {
                    DieEntity.Add(currentInstructor);
                }

                reader.Close();
                cmd.Dispose();
            }
            return DieEntity;


        }
        public bool SaveDieEntity(DieEntity model)
        {

            try
            {
                var connString = ConfigurationManager.ConnectionStrings["DB_9B1091_generalEntities"].ConnectionString;
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    var command = new SqlCommand("UPDATE [Die] set " +
                            "[Code] = '" + model.Code + "'," +
                            "[Description] = '" + model.Description + "'," +
                            "[Id1] = '" + model.Id1 + "'," +
                            "[Id2] = '" + model.Id2 + "'," +
                            "[Length] = '" + model.Length + "'," +
                            "[Price_val] = " + model.Price_val + "," +
                            "[Glass_Size] = '" + model.Glass_Size + "'," +
                            "[ID1_ID2] = '" + model.ID1_ID2 + "'" +
                            " where id = " + model.id, conn);
                    command.ExecuteNonQuery();
                    return true;
                }

            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public DieEntity EditDieEntity(int id)
        {
            var connString = ConfigurationManager.ConnectionStrings["DB_9B1091_generalEntities"].ConnectionString;
            DieEntity DieEntity = new DieEntity();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var cmd = new SqlCommand("select * from die where id = " + id, conn);
                var reader = cmd.ExecuteReader();

                int currentPersonID = -1;
                
                while (reader.Read())
                {
                    var personID = Convert.ToInt32(reader["id"]);
                    if (personID != currentPersonID)
                    {
                        currentPersonID = personID;

                        DieEntity.Code = reader["Code"].ToString();
                        DieEntity.Description = reader["Description"].ToString();
                        DieEntity.Id1 = reader["Id1"].ToString();
                        DieEntity.id = Convert.ToInt32(reader["id"]);
                        DieEntity.ID1_ID2 = reader["ID1_ID2"].ToString();
                        DieEntity.Id2 = reader["Id2"].ToString();
                        DieEntity.Length = reader["Length"].ToString();
                        DieEntity.Price_val = Convert.ToInt32(reader["Price_val"]);
                        
                    }

                }

                reader.Close();
                cmd.Dispose();
            }
            return DieEntity;

        }
        public bool DeleteDieEnity(int id)
        {
            var connString = ConfigurationManager.ConnectionStrings["DB_9B1091_generalEntities"].ConnectionString;
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var command = new SqlCommand("DELETE FROM die WHERE id = @id", conn);
                command.Parameters.AddWithValue("@id", id);  
                command.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        public bool PutDieEntity(DieEntity model)
        {
            
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["DB_9B1091_generalEntities"].ConnectionString;
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO [Die] ("+
                            "[Code], [Description], [Id1], [Id2], [Length], [Price_val], [Glass_Size], [ID1_ID2]) VALUES ("+
                            "'" + model.Code + "'," + 
                            "'" + model.Description + "'," + 
                            "'" + model.Id1 + "'," + 
                            "'" + model.Id2 + "'," + 
                            "'" + model.Length + "'," + 
                            "" + model.Price_val + "," + 
                            "'" + model.Glass_Size + "'," + 
                            "'" + model.ID1_ID2 + "');"
                        , conn);
                    command.ExecuteNonQuery();
                    return true;
                }
               
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }
        public IEnumerable<DieEntity> GetDieEntityADONET()
        {
            var connString = ConfigurationManager.ConnectionStrings["DB_9B1091_generalEntities"].ConnectionString;
            List<DieEntity> DieEntity = new List<DieEntity>();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var cmd = new SqlCommand("select Top 1000 * from die order by ID desc", conn);
                var reader = cmd.ExecuteReader();

                int currentPersonID = -1;
                DieEntity currentInstructor = null;
                while (reader.Read())
                {
                    var personID = Convert.ToInt32(reader["id"]);
                    if (personID != currentPersonID)
                    {
                        currentPersonID = personID;
                        if (currentInstructor != null)
                        {
                            DieEntity.Add(currentInstructor);
                        }
                        currentInstructor = new DieEntity();
                        currentInstructor.Code = reader["Code"].ToString();
                        currentInstructor.Description = reader["Description"].ToString();
                        currentInstructor.Id1 = reader["Id1"].ToString();
                        currentInstructor.id = Convert.ToInt32(reader["id"]);
                        currentInstructor.ID1_ID2 = reader["ID1_ID2"].ToString();
                        currentInstructor.Id2 = reader["Id2"].ToString();
                        currentInstructor.Length = reader["Length"].ToString();
                        currentInstructor.Price_val = Convert.ToInt32(reader["Price_val"]);
                        
                    }
                    
                }
                if (currentInstructor != null)
                {
                    DieEntity.Add(currentInstructor);
                }

                reader.Close();
                cmd.Dispose();
            }
            return DieEntity;


        }
    }
}