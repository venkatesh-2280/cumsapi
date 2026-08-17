using MySqlX.XDevAPI.Common;
using STAReportsAPI.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace STAReportsAPI.STADataAccess
{
    public class CompanyData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;

        public DataSet UpdateLogo(string sFilepath,int companygid, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("In_Filepath", sFilepath, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_companygid", companygid, DbType.Int64));
                
                ds = dbManager.execStoredProcedurelist("SP_UpdateCompanyLogo", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:SP_UpdateCompanyLogo" + "Error Message:" + ex.Message);
                return ds;
            }

        }
        public DataSet MailDetails(MailConfiguration_Model ModelObj, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("In_Action", ModelObj.ActionName, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_config_gid", ModelObj.Gid, DbType.Int64));
                parameters.Add(dbManager.CreateParameter("In_mailId", ModelObj.MailId, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_mailtype", "", DbType.String));
                parameters.Add(dbManager.CreateParameter("In_password", ModelObj.MailPwd, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_smtp", ModelObj.MailSmtp, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_port", ModelObj.MailPort, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_User_Id", ModelObj.CreateBy, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_companygid", ModelObj.CompanyGid, DbType.String));
                ds = dbManager.execStoredProcedurelist("SP_UpdateMailConfig", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:SP_UpdateMailConfig" + "Error Message:" + ex.Message);
                return ds;
            }

        }
        public DataSet CapitalStructure(Capital_Model ModelObj, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
				parameters.Add(dbManager.CreateParameter("in_compsubgrp_gid", ModelObj.Company_Gid, DbType.Int32));
				ds = dbManager.execStoredProcedurelist("pr_srp_fetch_companydtl", CommandType.StoredProcedure, parameters.ToArray());
				return ds;
				//parameters.Add(dbManager.CreateParameter("In_Action", ModelObj.ActionName, DbType.String));
				//parameters.Add(dbManager.CreateParameter("In_tmpcap_gid", ModelObj.cap_gid, DbType.Int64));
				//parameters.Add(dbManager.CreateParameter("In_Company_Gid", ModelObj.Company_Gid, DbType.Int64));
				//parameters.Add(dbManager.CreateParameter("In_ShareType", ModelObj.ShareTypeGid, DbType.Int64));
				//parameters.Add(dbManager.CreateParameter("In_ShareQty", ModelObj.ShareQty, DbType.Double));
				//parameters.Add(dbManager.CreateParameter("In_PaidUpValue", ModelObj.PaidUpValue, DbType.Double));
				//parameters.Add(dbManager.CreateParameter("In_ShareValue", ModelObj.ShareValue, DbType.Double));
				//parameters.Add(dbManager.CreateParameter("In_ISIN", ModelObj.ISIN, DbType.String));
				//parameters.Add(dbManager.CreateParameter("In_LoginUser_GID", ModelObj.User_GID, DbType.Int32));
				//ds = dbManager.execStoredProcedurelist("SP_DML_TmpCaptialStc", CommandType.StoredProcedure, parameters.ToArray());
				//return ds;
			}
			catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:SP_DML_TmpCaptialStc" + "Error Message:" + ex.Message);
                return ds;
            }

        }
		public DataSet DashboardDetails(Int32 companyGid ,string sIsinNumber, string constring)
		{
			try
			{
				DBManager dbManager = new DBManager(constring);
				Dictionary<string, Object> values = new Dictionary<string, object>();
				MySqlDataAccess con = new MySqlDataAccess("");
				parameters = new List<IDbDataParameter>();

				parameters.Add(dbManager.CreateParameter("in_comp_gid", companyGid, DbType.Int32));
				parameters.Add(dbManager.CreateParameter("in_isin_id", sIsinNumber, DbType.String));
				
				ds = dbManager.execStoredProcedurelist("pr_srp_get_dashboarddtl", CommandType.StoredProcedure, parameters.ToArray());
				return ds;
			}
			catch (Exception ex)
			{
				CommonHeader objlog = new CommonHeader();
				objlog.logger("SP:pr_srp_get_dashboarddtl" + "Error Message:" + ex.Message);
				return ds;
			}

		}


	}
}
