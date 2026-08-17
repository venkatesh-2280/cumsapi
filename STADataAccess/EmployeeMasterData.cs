using MySql.Data.MySqlClient;
using STAReportsAPI.Models;
using System.ComponentModel.Design;
using System.Data;
using static gnsastaapi.STADataAccess.CommonModel;

namespace STAReportsAPI.STADataAccess
{
    public class EmployeeMasterData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        string constring1 = "";
        public DataSet EmployeeDetails(int EmpId,string sAction,string constring)
        {
            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("In_user_id", EmpId, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("In_Action", sAction, DbType.String));
                ds = dbManager.execStoredProcedurelist("SP_GetAllEmployeeDetails", CommandType.StoredProcedure,parameters.ToArray());
               
            }
            catch (Exception ex)
            {

                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:SP_GetAllEmployeeDetails" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        public DataSet GetTreeNodes( string constring)
        {
            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                ds = dbManager.execStoredProcedurelist("GetTreeNodes", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:GetTreeNodes" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        public DataSet GetEmpcheckedvaluedb(string empid,string constring)
        {
            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("In_empid", empid, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_checkedvalues", CommandType.StoredProcedure,parameters.ToArray());

            }
            catch (Exception ex)
            {

                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_checkedvalues" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        public DataSet SaveEmployee(EmployeeMaster_Model EDtlObj, string constring)
        {
            try
            {

                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("In_Action", EDtlObj.sAction, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_EmpID", EDtlObj.EmployeeID, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("In_EmpCode", EDtlObj.EmployeeCode, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_EmpName", EDtlObj.EmployeeName, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_EmpTitle", "0", DbType.String));
                parameters.Add(dbManager.CreateParameter("In_GradeID", EDtlObj.GradeID, DbType.Int32));
           
                
                if (EDtlObj.DOJ == null || EDtlObj.DOJ == "")
                {
                    parameters.Add(dbManager.CreateParameter("In_DOJ", DateTime.Now.ToString("dd/MM/yyyy"), DbType.DateTime));
                }
                else
                {
                    string strdt = EDtlObj.DOJ.Substring(4, 12);
                    parameters.Add(dbManager.CreateParameter("In_DOJ", Convert.ToDateTime(strdt).ToString("dd/MM/yyyy"), DbType.DateTime));
                }
                if (EDtlObj.Address == null)
                {
                    EDtlObj.Address = "";
                }
                if (EDtlObj.Pin == null)
                {
                    EDtlObj.Pin = "0";
                }
                if (EDtlObj.LanNo == null)
                {
                    EDtlObj.LanNo = "";
                }
                parameters.Add(dbManager.CreateParameter("In_Address", EDtlObj.Address, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_CityID", EDtlObj.CityID, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("In_PinID", EDtlObj.Pin, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("In_StateID", EDtlObj.StateID, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("In_RegionID", EDtlObj.RegionID, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("In_MobileNo", EDtlObj.MobileNo, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_LanNo", EDtlObj.LanNo, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_EmailID", EDtlObj.EmailID, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_CreatedBy", EDtlObj.UserID, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("In_EmpType", EDtlObj.TypeID, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_Password", EDtlObj.Password, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_UserGroupID", EDtlObj.UserGroupID, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("In_status", EDtlObj.Status, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_OrgLevelMax", EDtlObj.OrgLevelMax, DbType.String));
                ds = dbManager.execStoredProcedurelist("SP_EmployeeSaveUpdateDelete_new", CommandType.StoredProcedure, parameters.ToArray());


            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:SP_EmployeeSaveUpdateDelete_new" + "Error Message:" + ex.Message);
            }
            return ds;
        }
    }
}
