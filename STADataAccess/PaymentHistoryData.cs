using STAReportsAPI.STADataAccess;
using System.Data;

namespace STAapi.STADataAccess
{
    public class PaymentHistoryData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        public DataSet getpayhis(string presaleid, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_presale_gid", presaleid, DbType.Int32));

                ds = dbManager.execStoredProcedurelist("pr_get_paymenthistory", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_paymenthistory - getpayhis - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet getpayhisdtl(string sectypeid, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_sectype_gid", sectypeid, DbType.Int32));

                ds = dbManager.execStoredProcedurelist("pr_get_paymenthistorydtl", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_paymenthistorydtl - getpayhisdtl - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        //public DataSet getpayhisdtl(string sectypeid, string constring)
        //{
        //    try
        //    {
        //        DBManager dbManager = new DBManager(constring);
        //        Dictionary<string, Object> values = new Dictionary<string, object>();
        //        MySqlDataAccess con = new MySqlDataAccess("");
        //        parameters = new List<IDbDataParameter>();
        //        parameters.Add(dbManager.CreateParameter("in_sectype_gid", sectypeid, DbType.Int32));

        //        ds = dbManager.execStoredProcedurelist("pr_get_paymenthistorydtl", CommandType.StoredProcedure, parameters.ToArray());
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonHeader objlog = new CommonHeader();
        //        objlog.logger("SP:pr_get_paymenthistorydtl - getpayhisdtl - Method Name " + "Error Message:" + ex.Message);
        //        return ds;
        //    }

        //}
    }
}
