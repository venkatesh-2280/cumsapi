using MySql.Data.MySqlClient;
using gnsastaapi.Models;
using System.ComponentModel.Design;
using System.Data;
using static gnsastaapi.STADataAccess.CommonModel;
using Microsoft.AspNetCore.Mvc;

namespace STAReportsAPI.STADataAccess
{
    public class UserManagementData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        string constring1 = "";

        public DataSet getuserlist(string constring)
        {
            DataSet ds = new DataSet();

            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>(); // if no params, leave empty

                ds = dbManager.execStoredProcedurelist("pr_get_allusers", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_allusers Error Message: " + ex.Message);
            }

            return ds;
        }

        public DataSet InsUser(UserManagementModel insObj, string constring)
        {
            try
            {

                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                //parameters.Add(dbManager.CreateParameter("in_empcode", insObj.empcode, DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_username", insObj.name, DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_useremail", insObj.email, DbType.String));               
                //parameters.Add(dbManager.CreateParameter("in_userpan", insObj.pan, DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_password", insObj.password, DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_userrole", insObj.userrole, DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_insert_date", DateTime.Now.ToString("dd/MM/yyyy"), DbType.DateTime));
                //parameters.Add(dbManager.CreateParameter("in_insert_by", "Superadmin", DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_update_date", DateTime.Now.ToString("dd/MM/yyyy"), DbType.DateTime));
                //parameters.Add(dbManager.CreateParameter("in_update_by", "Superadmin", DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_isremoved", "N", DbType.String));
                parameters.Add(dbManager.CreateParameter("in_empcode", insObj.empcode, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_username", insObj.name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_useremail", insObj.email, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_userpan", insObj.pan, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_usermobile", insObj.usermobile, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_userotp", insObj.userotp, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_isclient", insObj.isclient, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_userrole", insObj.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_password", insObj.userpwd, DbType.String)); 
                parameters.Add(dbManager.CreateParameter("in_lock_flag", insObj.lock_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_userpwdexpdays", insObj.userpwdexpdays, DbType.String));

                parameters.Add(dbManager.CreateParameter("in_insert_by", "1", DbType.String));

                //parameters.Add(dbManager.CreateParameter("in_update_date", DateTime.Now.ToString("dd/MM/yyyy"), DbType.DateTime));
                // parameters.Add(dbManager.CreateParameter("in_update_by", "Superadmin", DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_isremoved", "N", DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_ins_user", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ins_user" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        public DataSet UpdUsers(UserManagementModel insObj, string constring)
        {
            try
            {

                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_id", insObj.id, DbType.Int32)); 
                parameters.Add(dbManager.CreateParameter("in_empcode", insObj.empcode, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_username", insObj.name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_useremail", insObj.email, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_userpan", insObj.pan, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_usermobile", insObj.usermobile, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_userotp", insObj.userotp, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_isclient", insObj.isclient, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_userrole", insObj.userrole, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_password", insObj.userpwd, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_status", insObj.status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lock_flag", insObj.lock_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_userpwdexpdays", insObj.userpwdexpdays, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_update_by", "Superadmin", DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_upd_users", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_upd_users" + "Error Message:" + ex.Message);
            }
            return ds;
        }

    }
}