using Microsoft.Extensions.Configuration;
using ppmwebapp.model;
using ppmwebapp.repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppmwebapp.repo.Concrete
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguration Configuration;
        public ProjectRepository(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        //private SqlDataAdapter sqlDataAdapter
       // private DataSet dataSet
     
        public Project Create(Project model)
        {
            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ProjectList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "CreateProject");
                cmd.Parameters.AddWithValue("@id", model.id);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                cmd.Parameters.AddWithValue("@Budget", model.Budget);
                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public Project Delete(int id)
        {
            Project pro = Get(id);
            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ProjectList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteProject");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return pro;
        }

        public Project Edit(Project model)
        {
            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ProjectList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "Edit");
                cmd.Parameters.AddWithValue("@id", model.id);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                cmd.Parameters.AddWithValue("@Budget", model.Budget);
                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public Project Get(int id)
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;
            string connect = Configuration.GetConnectionString("DBConnection");
            var model = new Project();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ProjectList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetProByid");
                cmd.Parameters.AddWithValue("@Id", id);
                sqlDataAdapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    model.id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["id"]);
                    model.Name = Convert.ToString(dataSet.Tables[0].Rows[0]["Name"]);
                    model.StartDate = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["EndDate"]);
                    model.Budget = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["Budget"]);
                }
                return model;
            }

            
        }

        public IEnumerable<Project> GetAll()
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;
            string connect = Configuration.GetConnectionString("DBConnection");
            IList<Project> getProList = new List<Project>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ProjectList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetProList");
                sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        Project obj = new Project();
                        obj.id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["id"]);
                        obj.Name = Convert.ToString(dataSet.Tables[0].Rows[i]["Name"]);
                        obj.StartDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["StartDate"]);
                        obj.EndDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["EndDate"]);
                        obj.Budget = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["Budget"]);
                        getProList.Add(obj);

                    }

                }
            }
            return getProList;
        }

    }
    }
