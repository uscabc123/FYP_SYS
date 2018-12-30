using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace FYP.Models
{
    public class AccountDataLayer
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FYPDB"].ToString();

        public UserProfile Profile(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                UserProfile user = new UserProfile();
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
                    user.Gender = Convert.ToInt32(rdr["Gender"]);

                }
                return user;
            }
        }


        public int UpdateProfile(UserProfile user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("spUpdateUserProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@Phone_no", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@FirstName", user.FName);
                cmd.Parameters.AddWithValue("@LastName", user.LName);
                cmd.Parameters.AddWithValue("@IC_Passport", user.ICPassport);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);

                con.Open();

                SqlParameter returnParameter = cmd.Parameters.Add("@UpdateProfileresult", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                int id = (int)returnParameter.Value;

                con.Close();
                return id;

            }
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
                    userdetail.UserID = rdr["UserID"].ToString();
                    userdetail.Fname = rdr["FirstName"].ToString();
                    userdetail.Lname = rdr["LastName"].ToString();
                    userdetail.role = Convert.ToInt32(rdr["RoleID"]);
                    userdetail.status = Convert.ToInt32(rdr["StatusID"]);
                    userdetail.gender = Convert.ToInt32(rdr["Gender"]);
                }
                return userdetail;

            }

        }
    }
}