using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eMunching_Loyalty_DataManager;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace LoyaltyAdministration.Controllers
{
    public class AnalyticsController : Controller
    {
        //
        // GET: /Analytics/
        AnalyticsRepository _analyticsRepository = new AnalyticsRepository();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult BeersPunchedList(string startDate, string endDate, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                List<BeersPunched> beersPunched = _analyticsRepository.GetBeersPunchedBetweenDatesBySorting(startDate, endDate, 67, jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = beersPunched, TotalRecordCount = _analyticsRepository.GetBeersPunchedCount(startDate, endDate, 67) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult ExportToExcel(string startDate, string endDate)
        {
            var beers = new System.Data.DataTable("BeersPunched");
            List<BeersPunched> beersPunched = _analyticsRepository.GetAllBeersPunchedBetweenDates(startDate, endDate, 67);
            beers.Columns.Add("BillNumber", typeof(string));
            beers.Columns.Add("ItemCode", typeof(int));
            beers.Columns.Add("Quantity", typeof(int));
            beers.Columns.Add("EmailAddress", typeof(string));
            beers.Columns.Add("ItemName", typeof(string));
            beers.Columns.Add("DateCreated", typeof(string));
            foreach (BeersPunched beer in beersPunched)
            {
                beers.Rows.Add(beer.BillNumber, beer.ItemCode, beer.Quantity, beer.UserEmail, beer.ItemName, beer.Created);
            }

            var grid = new GridView();
            grid.DataSource = beers;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }

        public ActionResult Coupons()
        {
            return View("Coupons");
        }

        [HttpPost]
        public JsonResult CouponsGeneratedList(string startDate, string endDate, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                List<CouponsGenerated> coupons = _analyticsRepository.GetAllCouponsGeneratedBetweenDatesBySorting(startDate, endDate, 67, jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = coupons, TotalRecordCount = _analyticsRepository.GetCouponsGeneratedCount(startDate, endDate, 67) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult ExportCoupons(string startDate, string endDate)
        {
            var beersTable = new System.Data.DataTable("BeersPunched");
            List<CouponsGenerated> coupons = _analyticsRepository.GetAllCouponsGeneratedBetweenDates(startDate, endDate, 67);
            beersTable.Columns.Add("CouponCode", typeof(string));
            beersTable.Columns.Add("EmailAddress", typeof(string));
            beersTable.Columns.Add("IsAssigned", typeof(byte));
            beersTable.Columns.Add("IsRedeemed", typeof(byte));
            beersTable.Columns.Add("DateCreated", typeof(string));
            beersTable.Columns.Add("DateRedeemed", typeof(string));
            foreach (CouponsGenerated coupon in coupons)
            {
                beersTable.Rows.Add(coupon.CouponCode, coupon.EmailAddress, coupon.IsAssigned, coupon.IsRedeemed, coupon.DateCreated, coupon.DateRedeemed);
            }

            var grid = new GridView();
            grid.DataSource = beersTable;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }

        public ActionResult Bills()
        {
            return View("Bills");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult BillsList(string startDate, string endDate, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                List<Bills> billsProcessed = _analyticsRepository.GetBillsBetweenDatesBySorting(startDate, endDate, 67, jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = billsProcessed, TotalRecordCount = _analyticsRepository.GetBillsCount(startDate, endDate, 67) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult ExportBills(string startDate, string endDate)
        {
            var beersTable = new System.Data.DataTable("BillsSettled");
            List<Bills> bills = _analyticsRepository.GetBillBetweenDates(startDate, endDate, 67);
            beersTable.Columns.Add("BillNumber", typeof(string));
            beersTable.Columns.Add("EmailAddress", typeof(string));
            beersTable.Columns.Add("Amount", typeof(float));
            beersTable.Columns.Add("BeerCount", typeof(Int32));
            beersTable.Columns.Add("DateCreated", typeof(string));
            beersTable.Columns.Add("UniqueCode", typeof(string));
            foreach (Bills bill in bills)
            {
                beersTable.Rows.Add(bill.BillNumber, bill.UserEmail, bill.BillAmount, bill.BeerCount, bill.Created, bill.UniqueCode);
            }

            var grid = new GridView();
            grid.DataSource = beersTable;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }
    }
}
