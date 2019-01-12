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

        public List<Messenge> AddMessage(Messenge messageboard)
        {
            List<Messenge> mlist = new List<Messenge>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SenderID", messageboard.Sender);
                cmd.Parameters.AddWithValue("@ReceiverID", messageboard.Receiver);
                cmd.Parameters.AddWithValue("@MContent", messageboard.MessageContent);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Messenge messengedetail = new Messenge();
                    messengedetail.Remark= Convert.ToInt32(rdr["remark"]);
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

        public List<MessageList> GetMessage(MessageList messageboard)
        {
            List<MessageList> mlist = new List<MessageList>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetMessageList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SenderID", messageboard.UserID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                MessageList messengedetail = new MessageList();
                while (rdr.Read())
                {
                    
                    messengedetail.Sender = rdr["Sender"].ToString();
                    messengedetail.FirstName = rdr["FirstName"].ToString();
                    messengedetail.LastName = rdr["LastName"].ToString();
                    messengedetail.Sender_ReceiverID = rdr["UserID"].ToString();
                    messengedetail.Receiver = rdr["Receiver"].ToString();

                    mlist.Add(messengedetail);
                    messengedetail.messageList = mlist;
                }
                con.Close();
            }
            return mlist;
        }
    }
}