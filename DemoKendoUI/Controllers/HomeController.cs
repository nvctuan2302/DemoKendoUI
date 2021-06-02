using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoKendoUI.Models;

using System.Data;
using System.Data.SqlClient;

namespace DemoKendoUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public JsonResult GetAllData()
        {
            var query = "SELECT * FROM Internet..Location (nolock)";
            var strCon = @"Data Source=.\sqlexpress;Initial Catalog=Internet;Integrated Security=True";
            var con = new SqlConnection(strCon);
            var cmd = new SqlCommand(query, con);
            var adap = new SqlDataAdapter(cmd);

            con.Open();
            var dataTable = new DataTable();
            adap.Fill(dataTable);

            con.Close();

            return Json(ConvertDatatableToList(dataTable), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubParentDesc()
        {
            
            var query = "SELECT SubParentDescVN,SubParentDesc FROM Internet..Location (nolock) where SubParentDesc LIKE N'Vung%' GROUP BY SubParentDesc, SubParentDescVN";
            var strCon = @"Data Source=.\sqlexpress;Initial Catalog=Internet;Integrated Security=True";
            var con = new SqlConnection(strCon);
            var cmd = new SqlCommand(query, con);
            var adap = new SqlDataAdapter(cmd);

            con.Open();
            var dataTable = new DataTable();
            adap.Fill(dataTable);

            con.Close();

            return Json(ConvertDatatableToList(dataTable), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCity()
        {

            var query = @"SELECT ID, DescriptionVN, SubParentDesc FROM Internet.dbo.Location (NOLOCK) WHERE SubParentDesc LIKE N'Vung%'";
            var strCon = @"Data Source=.\sqlexpress;Initial Catalog=Internet;Integrated Security=True";
            var con = new SqlConnection(strCon);
            var cmd = new SqlCommand(query, con);
            var adap = new SqlDataAdapter(cmd);

            con.Open();
            var dataTable = new DataTable();
            adap.Fill(dataTable);

            con.Close();

            return Json(ConvertDatatableToList(dataTable), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistrict()
        {

            var query = @"SELECT lc.ID ,lc.Code,lc.Description,ds.FullNameVN,ds.LocationID, ds.ID FROM Internet..Location (nolock) lc JOIN Internet..District (nolock) ds ON ds.LocationID = lc.ID ";
            var strCon = @"Data Source=.\sqlexpress;Initial Catalog=Internet;Integrated Security=True";
            var con = new SqlConnection(strCon);
            var cmd = new SqlCommand(query, con);
            var adap = new SqlDataAdapter(cmd);

            con.Open();
            var dataTable = new DataTable();
            adap.Fill(dataTable);

            con.Close();

            return Json(ConvertDatatableToList(dataTable), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWard()
        {

            var query = @"SELECT wr.NameVN, ds.ID, wr.DistrictID FROM Internet..District (NOLOCK) ds JOIN Internet..Ward (NOLOCK) wr ON ds.ID = wr.DistrictID";
            var strCon = @"Data Source=.\sqlexpress;Initial Catalog=Internet;Integrated Security=True";
            var con = new SqlConnection(strCon);
            var cmd = new SqlCommand(query, con);
            var adap = new SqlDataAdapter(cmd);
            
            con.Open();
            var dataTable = new DataTable();
            adap.Fill(dataTable);

            con.Close();

            return Json(ConvertDatatableToList(dataTable), JsonRequestBehavior.AllowGet);
        }

        public List<Dictionary<string, object>> ConvertDatatableToList(DataTable dataTable)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dataTable.Rows)
            {
                var dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dataTable.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc]);
                }

                list.Add(dic);
            }

            return list;
        }
    }
}
