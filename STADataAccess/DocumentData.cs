using log4net;
using STAapi.Controllers;
using STAapi.Models;
using STAReportsAPI.STADataAccess;
using System.Data;

namespace STAapi.STADataAccess
{
    public class DocumentData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        CommonHeader objlog = new CommonHeader();


        public DataSet getdoclist(string presaleid, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_presale_gid", presaleid, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sectype_gid", 0, DbType.Int32));
                ds = dbManager.execStoredProcedurelist("pr_get_document_new", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                
                objlog.logger("SP:pr_get_document_new - getdoclist - Method Name " + "Error Message:" + ex.Message);
                return ds;
            }

        }
        public DataSet iuddoc(DocumentModel iudObj, string constring)
        {

            try
            {
                //throw new Exception("Log4net test error");

                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();


                parameters.Add(dbManager.CreateParameter("in_doc_gid", iudObj.doc_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_doc_presale_gid", iudObj.doc_presale_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_doc_sectype_gid", iudObj.doc_sectype_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_doc_security_type", iudObj.doc_security_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_doc_type", iudObj.doc_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_doc_file_name", iudObj.doc_file_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_doc_path", iudObj.doc_path, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_doc_remarks", iudObj.doc_remarks, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_doc_version", iudObj.doc_version, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_queuestatus", iudObj.queuestatus, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_insert_by", iudObj.doc_insert_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_update_by", iudObj.doc_update_by, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_action", iudObj.action, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_doc_gst_add", iudObj.doc_gst_add, DbType.String)); 
                parameters.Add(dbManager.CreateParameter("in_doc_date",iudObj.doc_date.HasValue ? iudObj.doc_date : (object)DBNull.Value,DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_doc_presales_gid", iudObj.doc_presales_gid, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_doc_versions", iudObj.doc_versions, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_iud_document_new", CommandType.StoredProcedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                objlog.logger("SP:pr_iud_document_new - getdoclist - Method Name " + "Error Message:" + ex.Message);
                throw;
                
            }
            return ds;
        }
    }
}
