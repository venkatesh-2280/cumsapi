using System.Data;

namespace STAReportsAPI.STADataAccess
{
	public class ReportsData
	{
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        CommonHeader objlog = new CommonHeader();

        public DataSet getmisreport(string start_date,string end_date, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_start_date", start_date, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_end_date", end_date, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_misreport", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {

                objlog.logger("SP:pr_get_misreport - getmisreport - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }

        public DataSet getcompanydetails(int line_no, string start_date, string end_date, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_line_no", line_no, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_start_date", DateTime.Parse(start_date), DbType.Date));
                parameters.Add(dbManager.CreateParameter("in_end_date", DateTime.Parse(end_date), DbType.Date));
                ds = dbManager.execStoredProcedurelist("pr_get_misreportdtl", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {

                objlog.logger("SP:pr_get_misreportdtl - getmisreport - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }
    }
}
