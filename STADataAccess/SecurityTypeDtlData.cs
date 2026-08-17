using STAapi.Models;
using STAReportsAPI.STADataAccess;
using System.Data;

namespace STAapi.STADataAccess
{
    public class SecurityTypeDtlData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;

        public DataSet getsectypdtllist(Int32 sectype_gid, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_sectype_gid", sectype_gid, DbType.Int32));
                ds = dbManager.execStoredProcedurelist("pr_get_securitytypedtl", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_securitytypedtl - getsectypdtllist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }
        public DataSet iudsectypedtl(SecurityTypeDtlModel iudObj, string constring)
        {

            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();


                parameters.Add(dbManager.CreateParameter("in_sectypdtl_gid", iudObj.sectypdtl_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_sectype_gid", iudObj.sectypdtl_sectype_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_gst_percentage", iudObj.sectypdtl_gst_percentage, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_gst_amount", iudObj.sectypdtl_gst_amount, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_total_amount", iudObj.sectypdtl_total_amount, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_received_amount", iudObj.sectypdtl_received_amount, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_due_amount", iudObj.sectypdtl_due_amount, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_payable_amount", iudObj.sectypdtl_payable_amount, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_description", iudObj.sectypdtl_description, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_remarks", iudObj.sectypdtl_remarks, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_version", iudObj.sectypdtl_version, DbType.Int32));
                //parameters.Add(dbManager.CreateParameter("in_sectypdtl_received_date", iudObj.sectypdtl_received_date.ToString("yyyy-mm-dd") ?? string.Empty , DbType.String));
                //string formattedDate = iudObj.sectypdtl_received_date.ToString("yyyy-MM-dd");
                //parameters.Add(dbManager.CreateParameter("in_sectypdtl_received_date", formattedDate, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sectypdtl_received_date", iudObj.sectypdtl_received_date, DbType.String));
                //parameters.Add(dbManager.CreateParameter("in_sectypdtl_received_date", iudObj.sectypdtl_received_date, DbType.String)); 
                parameters.Add(dbManager.CreateParameter("in_queuestatus", iudObj.queuestatus, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_insert_by", iudObj.insert_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_update_by", iudObj.update_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_action", iudObj.action, DbType.String));

                ds = dbManager.execStoredProcedurelist("pr_iud_securitytypedtl", CommandType.StoredProcedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_iud_securitytypedtl - iudsectypedtl - Method Name " + "Error Message:" + ex.Message);

            }
            return ds;
        }
    }
}
