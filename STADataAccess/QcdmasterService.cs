
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAapimodels;

namespace STAReportsAPI.STADataAccess
{
	public class QcdmasterService
	{
		public static DataTable QcdMasterRead(QcdmasterModel objmodel, headerValue headerval, string constring)
		{
			DataTable ds = new DataTable();
			try
			{
				QcdmastersData objqcd = new QcdmastersData();
				ds = objqcd.QcdModeldataRead(objmodel, headerval, constring);
			}
			catch (Exception e)
			{ }
			return ds;
		}

		public static DataTable QcdMasterGridRead(Qcdgridread objgridread, headerValue headerval, string constring)
		{
			DataTable ds = new DataTable();
			try
			{
				QcdmastersData objqcd = new QcdmastersData();
				ds = objqcd.QcdModeldataGridRead(objgridread, headerval, constring);
			}
			catch (Exception e)
			{ }
			return ds;
		}

		public static DataTable QcdMasters(mainQCDMaster objmaster, headerValue headerval, string constring)
		{
			DataTable ds = new DataTable();
			try
			{
				QcdmastersData objqcd = new QcdmastersData();
				ds = objqcd.QcdMaster(objmaster, headerval, constring);
			}
			catch (Exception e)
			{ }
			return ds;
		}

		//getallqcddata
		public static DataTable getallqcdservice(Qcdgridread objgridread, headerValue headerval, string constring)
		{
			DataTable ds = new DataTable();
			try
			{
				QcdmastersData objqcd = new QcdmastersData();
				ds = objqcd.getallqcddata(objgridread, headerval, constring);
			}
			catch (Exception e)
			{ }
			return ds;
		}

	}
}
