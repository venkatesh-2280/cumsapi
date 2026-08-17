using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAapimodels;
using Newtonsoft.Json;

namespace STAReportsAPI.STADataAccess
{
	public class QcdmastersData
	{
		DataSet ds = new DataSet();
		DataTable result = new DataTable();		
		List<IDbDataParameter> parameters;
		public DataTable QcdModeldataRead(QcdmasterModel Objmodel, headerValue headerval, string constring)
		{
			try
			{
				DBManager dbManager = new DBManager(constring);
				Dictionary<string, Object> values = new Dictionary<string, object>();
				MySqlDataAccess con = new MySqlDataAccess(Objmodel.in_user_code);
				parameters = new List<IDbDataParameter>();
				parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
				ds = dbManager.execStoredProcedure("pr_get_qcdparent", CommandType.StoredProcedure, parameters.ToArray());
				result = ds.Tables[0];
				return result;
			}
			catch (Exception ex)
			{
                CommonHeader objlog = new CommonHeader();
				objlog.logger("SP:pr_get_qcdparent" + "Error Message:" + ex.Message);
				//objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(headerval), "pr_get_qcdparent", headerval.user_code, constring);
				return result;
			}
		}
	
		public DataTable QcdModeldataGridRead(Qcdgridread objgridread, headerValue headerval, string constring)
		{
			try
			{
				DBManager dbManager = new DBManager(constring);
				Dictionary<string, Object> values = new Dictionary<string, object>();
				MySqlDataAccess con = new MySqlDataAccess(objgridread.in_user_code);
				parameters = new List<IDbDataParameter>();
				parameters.Add(dbManager.CreateParameter("in_master_code", objgridread.in_master_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
				ds = dbManager.execStoredProcedure("pr_get_qcdmaster", CommandType.StoredProcedure, parameters.ToArray());
				result = ds.Tables[0];
				return result;
			}
			catch (Exception ex)
			{
                CommonHeader objlog = new CommonHeader();
				objlog.logger("SP:pr_get_qcdmaster" + "Error Message:" + ex.Message);
				//objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgridread), "pr_get_qcdmaster", headerval.user_code, constring);
				return result;
			}
		}

		public DataTable QcdMaster(mainQCDMaster objmaster, headerValue headerval, string constring)
		{
			try
			{
				DBManager dbManager = new DBManager(constring);
				Dictionary<string, Object> values = new Dictionary<string, object>();
				parameters = new List<IDbDataParameter>();
				parameters.Add(dbManager.CreateParameter("in_master_gid", objmaster.masterGid, DbType.Int64));
				parameters.Add(dbManager.CreateParameter("in_master_syscode", objmaster.masterSyscode, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_master_code", objmaster.masterCode, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_master_short_code", objmaster.masterShortCode, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_master_name", objmaster.masterName, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_master_multiple_name", objmaster.mastermutiplename, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_parent_master_syscode", objmaster.ParentMasterSyscode, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_depend_parent_master_syscode", objmaster.depend_parent_master_syscode, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_depend_master_syscode", objmaster.depend_master_syscode, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_depend_flag", objmaster.depend_flag, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_active_status", objmaster.active_status, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_action", objmaster.action, DbType.String));
				parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
				parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
				ds = dbManager.execStoredProcedure("pr_staweb_mst_tmaster", CommandType.StoredProcedure, parameters.ToArray());
				result = ds.Tables[0];
				return result;
			}
			catch (Exception ex)
			{
                CommonHeader objlog = new CommonHeader();
				objlog.logger("SP:pr_recon_mst_tmaster" + "Error Message:" + ex.Message);
				//objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objmaster), "pr_recon_mst_tmaster", headerval.user_code, constring);
				return result;
			}
		}


		public DataTable getallqcddata(Qcdgridread objgridread, headerValue headerval, string constring)
		{
			try
			{
				DBManager dbManager = new DBManager(constring);
				Dictionary<string, Object> values = new Dictionary<string, object>();
				MySqlDataAccess con = new MySqlDataAccess(objgridread.in_user_code);
				parameters = new List<IDbDataParameter>();
				parameters.Add(dbManager.CreateParameter("in_master_code", objgridread.in_master_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
				parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
				ds = dbManager.execStoredProcedure("pr_get_allqcdmaster", CommandType.StoredProcedure, parameters.ToArray());
				result = ds.Tables[0];
				return result;
			}
			catch (Exception ex)
			{
				CommonHeader objlog = new CommonHeader();
				objlog.logger("SP:pr_get_allqcdmaster" + "Error Message:" + ex.Message);
				//objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgridread), "pr_get_allqcdmaster", headerval.user_code, constring);
				return result;
			}
		}


	}
}
