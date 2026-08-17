using gnsastaapi.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using STAapi.Models;
using System;
using System.ComponentModel.Design;
using System.Data;
using static gnsastaapi.STADataAccess.CommonModel;

namespace STAReportsAPI.STADataAccess
{
    public class DividendMasterData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        string constring1 = "";

        public DataSet getdividendlist(DividendListRequest insObj, string constring)            
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>(); // if no params, leave empty
                parameters.Add(dbManager.CreateParameter("p_action", insObj.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_dividend_gid", insObj.dividendgid,DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedurelist("sp_staweb_trn_tdividenddetails_get", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_staweb_trn_tdividenddetails_get Error Message: " + ex.Message);
            }

            return ds;
        }

        public DataSet getCompany(CompanyDetailsModel inscObj, string constring)
        {

            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();                
                ds = dbManager.execStoredProcedurelist("sp_sta_web_company_details", CommandType.StoredProcedure, parameters.ToArray());              
            }
            catch (Exception ex)
            {
                // Log error if any
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_sta_web_company_details" + " Error Message:" + ex.Message);
            }

            return ds; // Return the DataSet with results
        }

        public DataSet IudDividend(DividendMasterModel insObj, string constring)
        {
            try
            {

                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("p_dividend_gid", insObj.dividendgid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("p_company_name", insObj.companyName, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_isin", insObj.isin, DbType.String));               
                parameters.Add(dbManager.CreateParameter("p_rbicode", insObj.rbicode, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_dividend_year", insObj.dividendYear, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_dividend_type", insObj.dividendType, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_remarks", insObj.remarks, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_dividend_rate", insObj.dividendRate, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_face_value", insObj.faceValue, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_dividend_per_share", insObj.dividendPerShare, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_bankname", insObj.bankName, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_ifsccode", insObj.ifscCode, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_dividend_account_no", insObj.dividendAccountNo, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_dividend_cutoff_date", insObj.cutoffDate, DbType.Date));
                parameters.Add(dbManager.CreateParameter("p_dividend_payment_date", insObj.paymentDate, DbType.Date));
                parameters.Add(dbManager.CreateParameter("p_user", insObj.user, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_action", insObj.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedurelist("sp_staweb_trn_tdividenddetails_iud", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_staweb_trn_tdividenddetails_iud" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        

    }
}