using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace FYP.Models
{
    public class AnnouncementDataLayer
    {

        string connectionString = ConfigurationManager.ConnectionStrings["FYPDB"].ToString();

        public int AddAnnouncement(Announcement announcementdata)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAnnouncement", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", announcementdata.Title);
                cmd.Parameters.AddWithValue("@Content", announcementdata.Content);
                cmd.Parameters.AddWithValue("@APath", announcementdata.Path);
                cmd.Parameters.AddWithValue("@CreatedBy", announcementdata.CreatedBy);
                cmd.Parameters.AddWithValue("@ADate", announcementdata.AnnouncementDate);
                cmd.Parameters.AddWithValue("@StatusID", announcementdata.Status);

                con.Open();


                SqlParameter returnParameter = cmd.Parameters.Add("@responseMessage", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                int id = (int)returnParameter.Value;


                con.Close();
                return id;

            }
        }
        public List<Announcement> GetStatusData()
        {

            List<Announcement> StatusData = new List<Announcement>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAnnouncmentComboValue", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Announcement combostatus = new Announcement();
                    combostatus.comboStatusID = Convert.ToInt32(rdr["ID"]);
                    combostatus.comboStatusValue = rdr["AStatus"].ToString();

                    StatusData.Add(combostatus);


                }
                con.Close();
            }
            return StatusData;
        }

    }
}