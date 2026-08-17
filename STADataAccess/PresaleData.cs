using System.Data;
using STAReportsAPI.STADataAccess;
using MySql.Data.MySqlClient;
using System;
using gnsastaapi.Models;
using STAapi.Models;

namespace STAapi.STADataAccess
{
    public class PresaleData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;

		public DataSet getpresalelist(string ActionTab, string constring)
		{
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
				parameters.Add(dbManager.CreateParameter("in_tabname", ActionTab, DbType.String));
				ds = dbManager.execStoredProcedurelist("pr_get_presale_new", CommandType.StoredProcedure, parameters.ToArray());
				return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_presale - getpresalelist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet getpresaleisinlist(string ActionTab, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_tabname", ActionTab, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_presale_new", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_presale - getpresalelist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet getauditlist(string presaleid, string constring)
		{
			try
			{
				DBManager dbManager = new DBManager(constring);
				Dictionary<string, Object> values = new Dictionary<string, object>();
				MySqlDataAccess con = new MySqlDataAccess("");
				parameters = new List<IDbDataParameter>();

				parameters.Add(dbManager.CreateParameter("in_presale_gid", presaleid, DbType.Int32));				
				ds = dbManager.execStoredProcedurelist("pr_get_audittrail", CommandType.StoredProcedure, parameters.ToArray());
				return ds;
			}
			catch (Exception ex)
			{

				
				return ds;
			}

		}

		public DataSet iudpresale(PresaleModel iudObj, string constring)
        {
            
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_presale_gid", iudObj.presale_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_presale_company_name", iudObj.presale_company_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_contact_person", iudObj.presale_contact_person, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_contact_no", iudObj.presale_contact_no, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_email_id", iudObj.presale_email_id, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_company_addr", iudObj.presale_company_addr, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_depository_type", iudObj.presale_depository_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_designation", iudObj.presale_designation, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_cin", iudObj.presale_cin, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_company_type", iudObj.presale_company_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_gst_no", iudObj.presale_gst_no, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_listing_status", iudObj.presale_listing_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_ref_by", iudObj.presale_ref_by, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_ref_contact_no", iudObj.presale_ref_contact_no, DbType.Int64));
				parameters.Add(dbManager.CreateParameter("in_presale_maturity_date",iudObj.presale_maturity_date ?? (object)DBNull.Value,DbType.DateTime));
				parameters.Add(dbManager.CreateParameter("in_presale_ref_email", iudObj.presale_ref_email, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_status", iudObj.presale_status, DbType.String));
				//parameters.Add(dbManager.CreateParameter("in_presale_ops_status", iudObj.presale_ops_status, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_presale_sec_type", iudObj.presale_sec_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_secsub_type", iudObj.presale_secsub_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_version", iudObj.presale_version, DbType.Int64));
                parameters.Add(dbManager.CreateParameter("in_presale_add2", iudObj.presale_add2, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_add3", iudObj.presale_add3, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_country", iudObj.presale_country, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_state", iudObj.presale_state, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_city", iudObj.presale_city, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_pincode", iudObj.presale_pincode, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_role", iudObj.user_role, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_insert_by", iudObj.insert_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_update_by", iudObj.update_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_action", iudObj.action, DbType.String));

                ds = dbManager.execStoredProcedurelist("pr_iud_presale", CommandType.StoredProcedure, parameters.ToArray());
                
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_iud_presale - iudpresale - Method Name " + "Error Message:" + ex.Message);
                
            }
            return ds;
        }

        public DataSet getmailtrigger(mailModel iudObj1, string constring)
        {

            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_presale_gid", iudObj1.presale_gid.HasValue ? iudObj1.presale_gid.Value : DBNull.Value, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_presale_version", iudObj1.presale_version, DbType.Int32));               

                ds = dbManager.execStoredProcedurelist("pr_get_mailtrigger", CommandType.StoredProcedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_mailtrigger - pr_get_mailtrigger - Method Name " + "Error Message:" + ex.Message);

            }
            return ds;
        }

        public DataSet InsEmailDetails(mailinsertModel insObj, string constring)
        {
            try
            {

               // constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_doc_gid", insObj.doc_gid, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_doc_file_name", insObj.doc_file_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_to", insObj.mail_to, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_cc", insObj.mail_cc, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_sub", insObj.mail_sub, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_msg", insObj.mail_msg, DbType.String));                
                ds = dbManager.execStoredProcedurelist("pr_ins_maildetails", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ins_maildetails" + "Error Message:" + ex.Message);
            }
            return ds;
        }

        public DataSet InsEmailDetails1(isinmailinsertModel insObj, string constring)
        {
            try
            {

                // constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                
                parameters.Add(dbManager.CreateParameter("in_mail_to", insObj.mail_to, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_cc", insObj.mail_cc, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_sub", insObj.mail_sub, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_msg", insObj.mail_msg, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_file_name", insObj.mail_file_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_mail_file_path", insObj.mail_file_path, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_presale_gid", insObj.presale_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrg_gid", insObj.chrg_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_doctype_gid", insObj.doctype_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_docsubtype_gid", insObj.docsubtype_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_insert_by", insObj.insert_by, DbType.Int32));
                ds = dbManager.execStoredProcedurelist("pr_ins_isinmaildetails", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ins_maildetails" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        public DataSet getpresalerenewallist(string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                ds = dbManager.execStoredProcedurelist("pr_get_presalerenewal", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_presalerenewal - getpresalerenewallist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet getmstdoclist(int doc_gid, string in_action,string in_docsub_category, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_doc_gid", doc_gid, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action", in_action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_docsub_category", in_docsub_category, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_mstdoclist", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_mstdoclist - getmsdoclist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet getisinlist(int chrg_presale_gid, int chrg_gid,string start_date,string end_date,string status, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_presale_gid", chrg_presale_gid, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_chrg_gid", chrg_gid, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_start_date", start_date, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_end_date", end_date, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_status", status, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_isisnservicecharges", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_isisnservicecharges - getmsdoclist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet getsauditlist(int chrg_presale_gid,string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_presale_gid", chrg_presale_gid, DbType.String));              
                ds = dbManager.execStoredProcedurelist("pr_get_isinservicechargeslog", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_isinservicechargeslog - getmsdoclist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet iudisinserchrg(isinservicecharges iudObj, string constring)
        {

            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_chrg_gid", iudObj.chrg_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrg_presale_gid", iudObj.chrg_presale_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrg_docsub_gid", iudObj.docsub_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrg_gst_percentage", iudObj.chrg_gst_percentage, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrg_total_amt", iudObj.chrg_total_amt, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_chrg_gst_amt", iudObj.chrg_gst_amt, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_chrg_payable_amt", iudObj.chrg_payable_amt, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_chrg_received_amt", iudObj.chrg_received_amt, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_chrg_due_amt", iudObj.chrg_due_amt, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_chrg_date", iudObj.chrg_date, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_chrg_received_date", iudObj.chrg_received_date, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_chrg_share_count", iudObj.chrg_share_count, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrg_status", iudObj.chrg_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_chrg_doc_status", iudObj.chrg_doc_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_insert_by", iudObj.insert_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_update_by", iudObj.update_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_action", iudObj.in_action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_chrg_remarks", iudObj.chrg_remarks, DbType.String));

                ds = dbManager.execStoredProcedurelist("pr_iud_isinservicecharges", CommandType.StoredProcedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_iud_isinservicecharges - iudisinserchrg - Method Name " + "Error Message:" + ex.Message);

            }
            return ds;
        }
        public DataSet getisinserchrgdtl(string chrg_gid, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_chrg_gid", chrg_gid, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_isisnservicecharges", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_isisnservicecharges - getisinserchrgdtl - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }
        public DataSet getisinserchrgdocdtl(int chrgdoc_chrg_gid, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_chrg_gid", chrgdoc_chrg_gid, DbType.Int32));
                ds = dbManager.execStoredProcedurelist("pr_get_isisnservicechargesdoc", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_isisnservicechargesdoc - getisinserchrgdocdtl - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }
        public DataSet iudisinserchrgdoc(isinservicechargesdoc iudObj, string constring)
        {

            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_gid", iudObj.chrgdoc_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_chrg_gid", iudObj.chrgdoc_chrg_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_doc_gid", iudObj.chrgdoc_doc_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_docsub_gid", iudObj.chrgdoc_docsub_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_doc_type", iudObj.chrgdoc_doc_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_invno", iudObj.chrgdoc_invno, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_chrg_invdate", iudObj.chrg_invdate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_file_name", iudObj.chrgdoc_file_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_path", iudObj.chrgdoc_path, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_chrgdoc_docno", iudObj.chrgdoc_docno, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_remarks", iudObj.remarks, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_insert_by", iudObj.insert_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_update_by", iudObj.update_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_action", iudObj.in_action, DbType.String));

                ds = dbManager.execStoredProcedurelist("pr_iud_isinservicechargesdoc", CommandType.StoredProcedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_iud_isinservicechargesdoc - iudisinserchrgdoc - Method Name " + "Error Message:" + ex.Message);

            }
            return ds;
        }
        public DataSet getisinmaildtl(int isinmail_gid, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_mail_gid", isinmail_gid, DbType.Int32));
                ds = dbManager.execStoredProcedurelist("pr_get_isinmaildtl", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_isinmaildtl - getisinmaildtl - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }
        public DataSet getmstseclist(string in_action, int sectype_gid ,string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                
                parameters.Add(dbManager.CreateParameter("in_fetch_value", in_action, DbType.String)); 
                parameters.Add(dbManager.CreateParameter("in_sectype_gid", sectype_gid, DbType.String));
                ds = dbManager.execStoredProcedurelist("sp_get_security_master", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_get_security_master - getmstseclist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet getmstlist(string in_action, int country_gid,int state_gid ,string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_action", in_action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_country_gid", country_gid, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_state_gid", state_gid, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_masterlist", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_masterlist - getmstlist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }
    }
}
