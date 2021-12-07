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
    public class EmployeeToProjectRepository : IEmployeeToProject
    {
        private readonly IConfiguration Configuration;



        public EmployeeToProjectRepository()
        {
        }



        public EmployeeToProjectRepository(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public EmployeeToProject Create(EmployeeToProject model)
        {

            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "insertProjectEmpRelation");
                cmd.Parameters.AddWithValue("@ProjectId", model.ProjectId);
                cmd.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                cmd.ExecuteNonQuery();
            }
            return model;
        }



        public EmployeeToProject Delete(int id)
        {
            EmployeeToProject emp = Get(id);
            string connect = Configuration.GetConnectionString("DBConnection");
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "deleteProjectEmpRelationByProjectId");
                cmd.Parameters.AddWithValue("@ProjectId", id);
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.ExecuteNonQuery();
            }
            return emp;
        }



        public EmployeeToProject Get(int id)
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;
            string connect = Configuration.GetConnectionString("DBConnection");
            var model = new EmployeeToProject();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "getAllEmployeeByProjectId");
                cmd.Parameters.AddWithValue("@ProjectId", id);
                sqlDataAdapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    model.ProjectId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ProjectId"]);
                    model.EmployeeName = Convert.ToString(dataSet.Tables[0].Rows[0]["EmployeeName"]);

                }
            }
            return model;
        }



        public IEnumerable<EmployeeToProject> GetAll()
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;
            string connect = Configuration.GetConnectionString("DBConnection");
            IList<EmployeeToProject> getEmpList = new List<EmployeeToProject>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "getAllProjectEmployee");
                sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        EmployeeToProject obj = new EmployeeToProject();
                        obj.ProjectId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["ProjectId"]);
                        obj.EmployeeName = Convert.ToString(dataSet.Tables[0].Rows[i]["EmployeeName"]);
                        getEmpList.Add(obj);



                    }



                }
            }
            return getEmpList;



        }



        public EmployeeToProject Edit(EmployeeToProject obj)
        {
            throw new NotImplementedException();
        }
    }
}