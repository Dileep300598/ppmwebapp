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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration Configuration;
        public EmployeeRepository(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

       // private SqlDataAdapter sqlDataAdapter
       // private DataSet dataSet
        public Employee Create(Employee model)
        {
            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "CreateEmployee");
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@FirstName", model.Firstname);
                cmd.Parameters.AddWithValue("@LastName", model.Lastname);
                cmd.Parameters.AddWithValue("@Contact", model.Contact);
                cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                cmd.ExecuteNonQuery();
            }
            return model;
        }

        public Employee Delete(int id)
        {
           Employee emp= Get(id);
            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteEmployee");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return emp;
        }

        public IEnumerable<Employee> GetAll()
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;
            string connect = Configuration.GetConnectionString("DBConnection");
              IList<Employee> getEmpList = new List<Employee>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetEmpList");
                sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        Employee obj = new Employee();
                        obj.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Id"]);
                        obj.Firstname = Convert.ToString(dataSet.Tables[0].Rows[i]["FirstName"]);
                        obj.Lastname = Convert.ToString(dataSet.Tables[0].Rows[i]["LastName"]);
                        obj.Contact = Convert.ToInt64(dataSet.Tables[0].Rows[i]["Contact"]);
                        obj.RoleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["RoleId"]);
                        getEmpList.Add(obj);

                    }

                }
            }
            return getEmpList;
        }

        public Employee Get(int id)
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;
            string connect = Configuration.GetConnectionString("DBConnection");
            var model = new Employee();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetEmpById");
                cmd.Parameters.AddWithValue("@Id", id);
                sqlDataAdapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Id"]);
                    model.Firstname = Convert.ToString(dataSet.Tables[0].Rows[0]["FirstName"]);
                    model.Lastname = Convert.ToString(dataSet.Tables[0].Rows[0]["LastName"]);
                    model.Contact = Convert.ToInt64(dataSet.Tables[0].Rows[0]["Contact"]);
                    model.RoleId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["RoleId"]);
                }
            }
            return model;

        }

        public Employee Edit(Employee model)
        {
            string connect = Configuration.GetConnectionString("DBConnection");

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "EditEmployee");
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@FirstName", model.Firstname);
                cmd.Parameters.AddWithValue("@LastName", model.Lastname);
                cmd.Parameters.AddWithValue("@Contact", model.Contact);
                cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                cmd.ExecuteNonQuery();
            }
            return model;
        }
    }
}