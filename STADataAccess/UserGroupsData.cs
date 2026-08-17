using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using STAReportsAPI.Models;
using System.ComponentModel.Design;
using System.Data;
using static gnsastaapi.STADataAccess.CommonModel;
using static STAReportsAPI.Models.UserGroups_Model;

namespace STAReportsAPI.STADataAccess
{
    public class UserGroupsData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        string constring1 = "";
        public DataSet fetchUserGroups(string constring)
        {
            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");

                ds = dbManager.execStoredProcedurelist("pr_get_allroles", CommandType.StoredProcedure);
               
            }
            catch (Exception ex)
            {

                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_allroles" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        public DataSet fetchRoleMapping(string role_code, string app_code, string constring)
        {
            try
            {
                //Int64 userGrpId = role_code;
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("In_role_code", role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("In_app_code", app_code, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_Roleapp_permissions_bkp_02_06_26", CommandType.StoredProcedure, parameters.ToArray());
               
            }
            catch (Exception ex)
            {

                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_Roleapp_permissions_bkp_02_06_26" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        public DataSet SaveUserRoleMapData(UserGroups_Model.RoleMappings rolemap, string constring)
        {
            try
            {
                
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_role_code", rolemap.role_code, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_activity_code", rolemap.menu_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_rolemenu_rowid", rolemap.rolemenu_rowid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_add_perm", rolemap.add_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_mod_perm", rolemap.mod_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_delete_perm", rolemap.Delete_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_view_perm", rolemap.view_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_download_perm", rolemap.download_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_link_perm", rolemap.link_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_mail_perm", rolemap.mail_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_retreq_perm", rolemap.retreq_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_approve", rolemap.approve_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_Boachecklist_perm", rolemap.Boachecklist_perm, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_deny_perm", rolemap.deny_flag, DbType.Int32));
                if (rolemap.rolemenu_rowid > 0)
                {
                    parameters.Add(dbManager.CreateParameter("in_mode_flag", "U", DbType.String));
                }
                else
                {
                    parameters.Add(dbManager.CreateParameter("in_mode_flag", "I", DbType.String));
                }
                ds = dbManager.execStoredProcedurelist("SP_Save_UserRoles", CommandType.StoredProcedure, parameters.ToArray());

               
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:SP_Save_UserRoles" + "Error Message:" + ex.Message);
            }
            return ds;
        }
        public DataSet CreateUserGroups(int usergroup_gid, string usergroup_name, string action_by, string usergroup_code, string app_code, string usrgrpstatus, string constring)
        {
            try
            {

                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_role_gid", usergroup_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_role_name", usergroup_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", usergroup_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_app_code", app_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_active_status", usrgrpstatus, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action_by", action_by, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_ins_upd_Role", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:SP_SetUserGroup" + "Error Message:" + ex.Message);
            }
            return ds;
        }

        //
        public DataSet Application_List(string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>(); // if no params, leave empty 
                ds = dbManager.execStoredProcedurelist("pr_get_allapplicaion", CommandType.StoredProcedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_allapplicaion" + "Error Message:" + ex.Message);
                //objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgridread), "pr_get_allqcdmaster", headerval.user_code, constring);

            }
            return ds;
        }

        public int SaveRolePermissions(List<RolePermissionDto> roleMappings, string constring)
        {
            int savedCount = 0;

            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                conn.Open();

                string roleCode = roleMappings.First().role_code;
                string appCode = roleMappings.First().app_code;

                // 1️⃣ DELETE ONCE
                using (MySqlCommand cmd = new MySqlCommand("pr_save_role_permissions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("In_status", "delete");
                    cmd.Parameters.AddWithValue("In_role_code", roleCode);
                    cmd.Parameters.AddWithValue("In_app_code", appCode);
                    //cmd.Parameters.AddWithValue("In_menu_id", 0);
                    cmd.Parameters.AddWithValue("In_menu_code", "");
                    cmd.Parameters.AddWithValue("In_add", "N");
                    cmd.Parameters.AddWithValue("In_modify", "N");
                    cmd.Parameters.AddWithValue("In_delete", "N");
                    cmd.Parameters.AddWithValue("In_view", "N");
                    cmd.Parameters.AddWithValue("In_download", "N");
                    cmd.Parameters.AddWithValue("In_link", "N");
                    cmd.Parameters.AddWithValue("In_mail", "N");
                    cmd.Parameters.AddWithValue("In_retreq", "N");
                    cmd.Parameters.AddWithValue("In_approve", "N");
                    cmd.Parameters.AddWithValue("In_Boachecklist", "N");
                    cmd.Parameters.AddWithValue("In_deny", "N");

                    cmd.ExecuteNonQuery();
                }

                // 2️⃣ INSERT ONLY SELECTED MENUS
                foreach (var rolemap in roleMappings)
                {
                    bool hasPermission = new[]
                    {
                rolemap.Add, rolemap.Modify, rolemap.Delete,rolemap.View,
                rolemap.Download, rolemap.Link,
                rolemap.Mail, rolemap.RetReq, 
                rolemap.Approve,rolemap.Mail, rolemap.Boachecklist
            }.Any(x => x == "Y");

                    if (!hasPermission)
                        continue;

                    using (MySqlCommand cmd = new MySqlCommand("pr_save_role_permissions", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("In_status", "insert");
                        cmd.Parameters.AddWithValue("In_role_code", roleCode);
                        cmd.Parameters.AddWithValue("In_app_code", appCode);
                        //cmd.Parameters.AddWithValue("In_menu_id", rolemap.menu_id);
                        cmd.Parameters.AddWithValue("In_menu_code", rolemap.menu_code);
                        cmd.Parameters.AddWithValue("In_add", rolemap.Add ?? "N");
                        cmd.Parameters.AddWithValue("In_modify", rolemap.Modify ?? "N");
                        cmd.Parameters.AddWithValue("In_delete", rolemap.Delete ?? "N");
                        cmd.Parameters.AddWithValue("In_view", rolemap.View ?? "N");
                        cmd.Parameters.AddWithValue("In_download", rolemap.Download ?? "N");
                        cmd.Parameters.AddWithValue("In_link", rolemap.Link ?? "N");
                        cmd.Parameters.AddWithValue("In_mail", rolemap.Mail ?? "N");
                        cmd.Parameters.AddWithValue("In_retreq", rolemap.RetReq ?? "N");
                        cmd.Parameters.AddWithValue("In_approve", rolemap.Approve ?? "N");
                        cmd.Parameters.AddWithValue("In_Boachecklist", rolemap.Boachecklist ?? "N");
                        cmd.Parameters.AddWithValue("In_deny", "N");

                        cmd.ExecuteNonQuery();
                        savedCount++;
                    }
                }
            }

            return savedCount;
        }


    }
}
