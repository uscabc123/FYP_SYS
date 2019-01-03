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
                cmd.Parameters.AddWithValue("@ConsultDateTime", consultation.ConsultationDate);
                cmd.Parameters.AddWithValue("@ConsultationStatus", consultation.followup);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}