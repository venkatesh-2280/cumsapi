using STAapi.Models;
using STAReportsAPI.STADataAccess;
using System.Data;

namespace STAapi.STADataAccess
{
    public class SecurityTypeData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;

        public DataSet getsectype(string presaleid,string presaleversion, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_presale_gid", presaleid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_presale_version", presaleversion, DbType.Int32));

                ds = dbManager.execStoredProcedurelist("pr_get_securitytype", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_securitytype - getsectype - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet iudsectype(SecurityTypeModel iudObj, string constring)
        {

            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_sectype_gid", iudObj.sectype_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_sectype_presale_gid", iudObj.sectype_presale_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_sectype_security_type", iudObj.sectype_security_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sectype_paidup_captital", iudObj.sectype_paidup_captital, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_sectype_face_value", iudObj.sectype_face_value, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_sectype_no_of_shares", iudObj.sectype_no_of_shares, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_sectype_no_of_shareholders", iudObj.sectype_no_of_shareholders, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_sectype_tenure", iudObj.sectype_tenure, DbType.Int32));				
				parameters.Add(dbManager.CreateParameter("in_sectype_service_start_month", iudObj.sectype_service_start_month, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_sectype_isin_no", iudObj.sectype_isin_no, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sectype_isin_start_date", iudObj.sectype_isin_start_date, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_sectype_amount", iudObj.sectype_amount, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_sectype_description", iudObj.sectype_description, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sectype_version", iudObj.sectype_version, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_queuestatus", iudObj.queuestatus, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_insert_by", iudObj.insert_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_update_by", iudObj.update_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_action", iudObj.action, DbType.String));

                // OUT parameter
                var outParam = dbManager.CreateParameter("get_sectype_gid", 0, DbType.Int32);
                outParam.Direction = ParameterDirection.Output;
                parameters.Add(outParam);

                ds = dbManager.execStoredProcedurelist("pr_iud_securitytype", CommandType.StoredProcedure, parameters.ToArray());
                var sectypeGid = outParam.Value;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_iud_securitytype - iudsectype - Method Name " + "Error Message:" + ex.Message);

            }
            return ds;
        }
    }
}
