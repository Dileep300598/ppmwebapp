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
    public class RoleRepository : IRoleRepository
    {
        private readonly IConfiguration Configuration;
        public RoleRepository(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        //public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString

      // private SqlDataAdapter sqlDataAdapter
       // private DataSet dataSet

        public Role Create(Role model)
        {
            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("RoleDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "CreateRole");
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@RoleName", model.RoleName);
                cmd.ExecuteNonQuery();
            }
                return model;
        }

        public Role Delete(int id)
        {
            Role rol = Get(id);
            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("RoleDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "Deleterole");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return rol;
        }

        public Role Edit(Role model)
        {
            string connect = Configuration.GetConnectionString("DBConnection");

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("RoleDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "EditRole");
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@RoleName", model.RoleName);
                cmd.ExecuteNonQuery();
            }
                return model;
        }

        public Role Get(int id)
        {
           SqlDataAdapter sqlDataAdapter;
           DataSet dataSet;
           string connect = Configuration.GetConnectionString("DBConnection");
            var model = new Role();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("RoleDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetroleByid");
                cmd.Parameters.AddWithValue("@Id", id);
                sqlDataAdapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Id"]);
                    model.RoleName = Convert.ToString(dataSet.Tables[0].Rows[0]["RoleName"]);

                }
            }
                return model;
            
        }

        public IEnumerable<Role> GetAll()
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;
            string connect = Configuration.GetConnectionString("DBConnection");
            IList<Role> getroleList = new List<Role>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("RoleDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetroleList");
                sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        Role obj = new Role();
                        obj.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Id"]);
                        obj.RoleName = Convert.ToString(dataSet.Tables[0].Rows[i]["RoleName"]);
                        getroleList.Add(obj);
                    }
                }
            }
            return getroleList;
        }
    }
}
