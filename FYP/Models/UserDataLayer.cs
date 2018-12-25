using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace FYP.Models
{
    public class UserDataLayer
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FYPDB"].ToString();


        public List<User> GetAccountStatusData()
        {

            List<User> ComboAccValue = new List<User>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAccStatusComboValue", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    User status = new User();

                    status.UserStatusListID = Convert.ToInt32(rdr["ID"]);
                    status.UserStatusListValue = rdr["StatusDescription"].ToString();
                    ComboAccValue.Add(status);


                }
                con.Close();
            }
            return ComboAccValue;
        }
        public List<User> GetRoleData()
        {

            List<User> ComboRolesValue = new List<User>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetRolesComboValue", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User role = new User();

                    role.UserRoleListID = Convert.ToInt32(rdr["ID"]);
                    role.UserRoleListValue = rdr["RoleDescription"].ToString();

                    ComboRolesValue.Add(role);


                }
                con.Close();
            }
            return ComboRolesValue;
        }
        public int AddUser(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Phone_no", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@FirstName", user.FName);
                cmd.Parameters.AddWithValue("@LastName", user.LName);
                cmd.Parameters.AddWithValue("@IC_Passport", user.ICPassport);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.ICPassport);
                cmd.Parameters.AddWithValue("@User_Role", user.UserRoleID);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@AccountStatus", user.UserStatusID);
                con.Open();


                SqlParameter returnParameter = cmd.Parameters.Add("@AddUserresult", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                int id = (int)returnParameter.Value;
              

                con.Close();
                return id;

            }
        }

        public User EditUser(string id)
        {
            User user = new User();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spPasswordUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User userdetail = new User();
                    userdetail.UserID = rdr["UserID"].ToString();
                    userdetail.Email = rdr["Email"].ToString();
                    userdetail.ICPassport = rdr["IC_Passport"].ToString();
                    userdetail.PhoneNumber = rdr["Phone_no"].ToString();
                    userdetail.FName = rdr["FirstName"].ToString();
                    userdetail.LName = rdr["LastName"].ToString();
                    userdetail.UserStatusListValue = rdr["StatusDescription"].ToString();
                    userdetail.UserRoleListValue = rdr["RoleDescription"].ToString();
                }
                cmd.ExecuteNonQuery();

                con.Close();
                return user;
            }
        }
        public List<User> GetAllUser()
        {
                 List<User> userlist = new List<User>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                User model = new User();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User userdetail = new User();
                    userdetail.UserID = rdr["UserID"].ToString();
                    userdetail.Email = rdr["Email"].ToString();
                    userdetail.ICPassport = rdr["IC_Passport"].ToString();
                    userdetail.PhoneNumber = rdr["Phone_no"].ToString();
                    userdetail.FName = rdr["FirstName"].ToString();
                    userdetail.LName = rdr["LastName"].ToString();
                    userdetail.UserStatusListValue = rdr["StatusDescription"].ToString();
                    userdetail.UserRoleListValue = rdr["RoleDescription"].ToString();
                    userlist.Add(userdetail);
                }

                con.Close();
            }
                return userlist;            
        }


        public int login(Login login)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

              
                cmd.Parameters.AddWithValue("@Email", login.Username);
                cmd.Parameters.AddWithValue("@Password", login.Password);

                con.Open();
                SqlParameter returnParameter = cmd.Parameters.Add("@responseMessage", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                int message = (int)returnParameter.Value;

            
                con.Close();

                return message;

            }

        }

        public Login getUserData(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Login userdetail = new Login();
                SqlCommand cmd = new SqlCommand("spGetUserProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@loginemail", email);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    userdetail.Fname = rdr["FirstName"].ToString();
                    userdetail.Lname = rdr["LastName"].ToString();
                    userdetail.role = Convert.ToInt32(rdr["RoleID"]);
                    userdetail.status = Convert.ToInt32(rdr["StatusID"]);
                    userdetail.gender = Convert.ToInt32(rdr["Gender"]);
                }
                return userdetail;

            }

        }


        public User Profile(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                User user = new User();
                SqlCommand cmd = new SqlCommand("spGetUserProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@loginemail", email);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.UserID = rdr["UserID"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.ICPassport = rdr["IC_Passport"].ToString();
                    user.PhoneNumber = rdr["Phone_no"].ToString();
                    user.FName = rdr["FirstName"].ToString();
                    user.LName = rdr["LastName"].ToString();
                }

                return user;

            }
        }







    }
}