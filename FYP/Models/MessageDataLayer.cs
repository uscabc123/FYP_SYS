using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace FYP.Models
{
    public class MessageDataLayer
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FYPDB"].ToString();

        public void AddMessage(Messenge messageboard)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SenderID", messageboard.Sender);
                cmd.Parameters.AddWithValue("@ReceiverID", messageboard.Receiver);
                cmd.Parameters.AddWithValue("@MContent", messageboard.MessageContent);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<Messenge> GetMessage(Messenge messageboard)
        {
            List<Messenge> mlist = new List<Messenge>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SenderID", messageboard.Sender);
                cmd.Parameters.AddWithValue("@ReceiverID", messageboard.Receiver);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Messenge messengedetail = new Messenge();
                    messengedetail.MessageID = Convert.ToInt32(rdr["remark"]);
                    messengedetail.MessageContent = rdr["MessageContent"].ToString();
                    messengedetail.MessageDateTime = (DateTime)rdr["messagedatetime"];
                    messengedetail.Sender = rdr["Sender"].ToString();
                    messengedetail.Receiver = rdr["Receiver"].ToString();
                    mlist.Add(messengedetail);
                    messengedetail.messageData = mlist;
                }
                con.Close();
            }
            return mlist;
        }
    }
}