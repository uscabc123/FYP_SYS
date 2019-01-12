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
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@UserID", id);
                User userdetail = new User();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    userdetail.UserID = rdr["UserID"].ToString();
                    userdetail.Email = rdr["Email"].ToString();
                    userdetail.ICPassport = rdr["IC_Passport"].ToString();
                    userdetail.PhoneNumber = rdr["Phone_no"].ToString();
                    userdetail.FName = rdr["FirstName"].ToString();
                    userdetail.LName = rdr["LastName"].ToString();
                    userdetail.Gender = Convert.ToInt32(rdr["Gender"]);
                    userdetail.UserRoleID = Convert.ToInt32(rdr["RoleID"]);
                    userdetail.UserStatusID = Convert.ToInt32(rdr["StatusID"]);

                }
                con.Close();
                return userdetail;
            }
        }

        public int UpdateUserProfile(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUserDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@Phone_no", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@FirstName", user.FName);
                cmd.Parameters.AddWithValue("@LastName", user.LName);
                cmd.Parameters.AddWithValue("@IC_Passport", user.ICPassport);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@User_Role", user.UserRoleID);
                cmd.Parameters.AddWithValue("@AccountStatus", user.UserStatusID);
                con.Open();
                SqlParameter returnParameter = cmd.Parameters.Add("@UpdateProfileresult", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                int id = (int)returnParameter.Value;
                con.Close();
                return id;
            }
        }

        public List<SearchUser> GetAllUser()
        {
            List<SearchUser> userlist = new List<SearchUser>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    SearchUser userdetail = new SearchUser();
                    userdetail.UserID = rdr["UserID"].ToString();
                    userdetail.Email = rdr["Email"].ToString();
                    userdetail.ICPassport = rdr["IC_Passport"].ToString();
                    userdetail.PhoneNumber = rdr["Phone_no"].ToString();
                    userdetail.FName = rdr["FirstName"].ToString();
                    userdetail.LName = rdr["LastName"].ToString();
                    userdetail.UserStatusListValue = rdr["StatusDescription"].ToString();
                    userdetail.UserRoleListValue = rdr["RoleDescription"].ToString();
                    userlist.Add(userdetail);
                    userdetail.userdata = userlist;
                }

                con.Close();
            }
            return userlist;
        }

        public List<SearchUser> GetSearchResult(SearchUser search)
        {
            List<SearchUser> userlist = new List<SearchUser>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Search", search.searchvalue);
                cmd.Parameters.AddWithValue("@UserSearch", search.AccountUserID);

                con.Open();
                User model = new User();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    SearchUser userdetail = new SearchUser();
                    userdetail.UserID = rdr["UserID"].ToString();
                    userdetail.Email = rdr["Email"].ToString();
                    userdetail.ICPassport = rdr["IC_Passport"].ToString();
                    userdetail.PhoneNumber = rdr["Phone_no"].ToString();
                    userdetail.FName = rdr["FirstName"].ToString();
                    userdetail.LName = rdr["LastName"].ToString();
                    userdetail.UserStatusListValue = rdr["StatusDescription"].ToString();
                    userdetail.UserRoleListValue = rdr["RoleDescription"].ToString();
                    userlist.Add(userdetail);
                    userdetail.userdata = userlist;
                }
                con.Close();
            }
            return userlist;
        }








    }
}