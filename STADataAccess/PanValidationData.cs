using gnsastaapi.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using STAapi.Models;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Text;
using static gnsastaapi.STADataAccess.CommonModel;

namespace STAReportsAPI.STADataAccess
{
    public class PanValidationData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;
        string constring1 = "";

        public DataSet getpandetails(PanValidationModel insObj, string constring)            
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>(); // if no params, leave empty               
                parameters.Add(dbManager.CreateParameter("p_dividendgid", insObj.dividendgid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("p_comp_gid", insObj.compgrpgid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("p_benpost_date",insObj.benposdate.ToString("yyyy-MM-dd"),DbType.String));                
                ds = dbManager.execStoredProcedurelist("sp_staweb_get_holder_pan_details", CommandType.StoredProcedure, parameters.ToArray());
                int expectedCount = ds.Tables[0].Rows.Count;
                SaveExpectedCount(insObj.dividendgid, expectedCount, constring);
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_staweb_get_holder_pan_details Error Message: " + ex.Message);
            }

            return ds;
        }


        public DataSet getpanupdatedetails(PanUpdateModel insObj, string constring)
        {
            DataSet ds = new DataSet();

            using (var conn = new MySqlConnection(constring))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // ✅ STEP 1: Clear Temp Table
                        using (var cmdClear = new MySqlCommand("sp_staweb_clear_pan_temp", conn, transaction))
                        {
                            cmdClear.CommandType = CommandType.StoredProcedure;
                            cmdClear.Parameters.AddWithValue("p_dividendgid", insObj.dividendgid);
                            cmdClear.ExecuteNonQuery();
                        }

                        // ✅ STEP 2: Bulk Insert Temp Table (ONE QUERY)
                        if (insObj.panList != null && insObj.panList.Count > 0)
                        {
                            StringBuilder sql = new StringBuilder();

                            sql.Append("INSERT INTO staweb_trn_pan_validation_temp ");
                            sql.Append("(dividendgid, pan, pan_status) VALUES ");

                            for (int i = 0; i < insObj.panList.Count; i++)
                            {
                                sql.Append($"(@div{i}, @pan{i}, @status{i})");

                                if (i < insObj.panList.Count - 1)
                                    sql.Append(",");
                            }

                            using (var cmdInsert = new MySqlCommand(sql.ToString(), conn, transaction))
                            {
                                for (int i = 0; i < insObj.panList.Count; i++)
                                {
                                    cmdInsert.Parameters.AddWithValue($"@div{i}", insObj.dividendgid);
                                    cmdInsert.Parameters.AddWithValue($"@pan{i}", insObj.panList[i].PAN);
                                    cmdInsert.Parameters.AddWithValue($"@status{i}", insObj.panList[i].PAN_Status);
                                }

                                cmdInsert.ExecuteNonQuery();
                            }
                        }

                        // ✅ STEP 3: Bulk Update Main Table
                        using (var cmdUpdate = new MySqlCommand("sp_staweb_bulk_pan_update", conn, transaction))
                        {
                            cmdUpdate.CommandType = CommandType.StoredProcedure;
                            cmdUpdate.Parameters.AddWithValue("p_dividendgid", insObj.dividendgid);

                            using (var adapter = new MySqlDataAdapter(cmdUpdate))
                            {
                                adapter.Fill(ds);
                            }
                        }

                        // ✅ Commit Transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        CommonHeader objlog = new CommonHeader();
                        objlog.logger("Bulk PAN Update Error: " + ex.Message);

                        throw;
                    }
                }
            }

            return ds;
        }




        public void SaveExpectedCount(int dividendgid, int expectedCount, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);

                List<IDbDataParameter> param = new List<IDbDataParameter>();

                param.Add(dbManager.CreateParameter("p_dividendgid", dividendgid, DbType.Int32));
                param.Add(dbManager.CreateParameter("p_expected_count", expectedCount, DbType.Int32));

                dbManager.execStoredProcedurelist("sp_staweb_save_expected_pan_count", CommandType.StoredProcedure, param.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SaveExpectedCount Error: " + ex.Message);
            }
        }

        public int GetExpectedPanCount(int dividendgid, string constring)
        {
            int expectedCount = 0;

            try
            {
                DBManager dbManager = new DBManager(constring);

                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter( "p_dividendgid", dividendgid,DbType.Int32 ));
                DataSet ds = dbManager.execStoredProcedurelist( "sp_staweb_get_expectedcount",  CommandType.StoredProcedure, parameters.ToArray());

                // ✅ Read Expected Count
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    expectedCount = Convert.ToInt32(ds.Tables[0].Rows[0]["expected_count"]);
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_staweb_get_expectedcount Error: " + ex.Message);
            }

            return expectedCount;
        }

    }
}