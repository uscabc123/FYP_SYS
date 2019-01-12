using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class ConsultationDataLayer
    {
        Consultation consult = new Consultation();
        string connectionString = ConfigurationManager.ConnectionStrings["FYPDB"].ToString();

        public void AddConsultation(Consultation consultation)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddConsultation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@doctorid", consultation.doctorid);
                cmd.Parameters.AddWithValue("@Diagnose", consultation.diagnose);
                cmd.Parameters.AddWithValue("@Symptoms", consultation.symptoms);
                cmd.Parameters.AddWithValue("@Remarks", consultation.remarks);
                cmd.Parameters.AddWithValue("@patientid", consultation.patientid);
                cmd.Parameters.AddWithValue("@ConsultationStatus", consultation.followup);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public List<ConsultationSearch> SearchConsultation(ConsultationSearch consultation)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSearchConsultation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ConsultationSearch> consultationlist = new List<ConsultationSearch>();


                cmd.Parameters.AddWithValue("@UserID", consultation.userid);
                cmd.Parameters.AddWithValue("@SearchValue", consultation.searchvalue);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    consultation.consultID = Convert.ToInt32(rdr["ID"]);
                    consultation.diagnose = rdr["Patient_Diagnose"].ToString();
                    consultation.patientid = rdr["ConsultPatient"].ToString();
                    consultation.remarks = rdr["Consultation_Remarks"].ToString();
                    consultation.symptoms = rdr["Patient_Symptoms"].ToString();
                    consultation.ConsultationDate = (DateTime)rdr["ConsultDateTime"];
                    consultationlist.Add(consultation);
                    consultation.consultsdata = consultationlist;
                }
                con.Close();

                return consultationlist;
            }
        }

        public Consultation ConsultationDetail(ConsultationSearch consultation)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetConsultationInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                Consultation ConsultDetail = new Consultation();

                cmd.Parameters.AddWithValue("@ConsultationID", consultation.consultID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ConsultDetail.ConsultationId = rdr["ID"].ToString();
                    ConsultDetail.diagnose = rdr["Patient_Diagnose"].ToString();
                    ConsultDetail.patientid = rdr["ConsultPatient"].ToString();
                    ConsultDetail.remarks = rdr["Consultation_Remarks"].ToString();
                    ConsultDetail.symptoms = rdr["Patient_Symptoms"].ToString();
                    ConsultDetail.ConsultationDate = (DateTime)rdr["ConsultDateTime"];
                    ConsultDetail.followup = Convert.ToInt32(rdr["ConsultationStatus"]);
                }
                con.Close();
                return ConsultDetail;
            }
        }


         public void UpdateConsultationDetail(Consultation consultation)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateConsultation", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ConsultationID", consultation.ConsultationId);
                    cmd.Parameters.AddWithValue("@Diagnose", consultation.diagnose);
                    cmd.Parameters.AddWithValue("@Symptoms", consultation.symptoms);
                    cmd.Parameters.AddWithValue("@Remarks", consultation.remarks);
                    cmd.Parameters.AddWithValue("@Status", consultation.followup);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                }
            }
    }
}