using System.Data;
using System.Data.SqlClient;
using Employee_Application.Model;

namespace Core_WebApi.Model
{
    public class EmployeeDB
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-Q21IKUJ\SQLEXPRESS;database=Employee;Integrated security=true");
        public string InsertDB(Employee obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EmpInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empna", obj.ename);
                cmd.Parameters.AddWithValue("@empaddr", obj.eaddr);
                cmd.Parameters.AddWithValue("@empsal", obj.esal);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Inserted Successfully";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public string DeleteDB(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EmpDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Ok...";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public List<Employee> SelectDB()
        {
            var list = new List<Employee>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_SelectAllEmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var o = new Employee
                    {
                        id = Convert.ToInt32(sdr["Emp_Id"]),
                        ename = sdr["Emp_Name"].ToString(),
                        eaddr = sdr["Emp_Address"].ToString(),
                        esal = sdr["Emp_Salary"].ToString()
                    };
                    list.Add(o);
                }
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return list;
        }
        public string updateDB(Employee emp)
        {
            string retVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateEmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", emp.id);
                cmd.Parameters.AddWithValue("@empsal", emp.esal);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                retVal = "Salary Updated";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message);
            }
            return retVal;
        }
    }
}
