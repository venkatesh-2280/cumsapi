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
    public class RuleEngineData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        string constring1 = "";


        public DataSet getruleenginelist(RuleEngineListRequest insObj, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>(); // if no params, leave empty
                parameters.Add(dbManager.CreateParameter("p_action", insObj.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_rule_gid", insObj.rulegid, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedurelist("sp_staweb_trn_truleengine_get", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_staweb_trn_truleengine_get Error Message: " + ex.Message);
            }

            return ds;
        }

        public DataSet IudruleEngine(RuleEngineModel insObj, string constring)
        {
            try
            {

                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("p_rule_gid", insObj.rulegid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("p_rule_name", insObj.rulename, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_tds_type", insObj.tdstype, DbType.String));               
                parameters.Add(dbManager.CreateParameter("p_bene_category", insObj.benecategory, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_pan_status", insObj.panstatus, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_pan_category", insObj.pancat, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_tds_rate", insObj.tdsrate, DbType.String));               
                parameters.Add(dbManager.CreateParameter("p_user", insObj.user, DbType.String));               
                parameters.Add(dbManager.CreateParameter("p_action", insObj.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedurelist("sp_staweb_trn_ruleengine_iud", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_staweb_trn_ruleengine_iud" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        

    }
}