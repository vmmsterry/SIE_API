using MVC_WebSite.Models;
using MVC_WebSite.Models.SIE;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebSite.Controllers
{
    public class SIE_APIController : Controller
    {
        string msjError = string.Empty;
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        ListaValores objLT = new ListaValores();
        string meses = string.Empty;
        string valores = string.Empty;
        static string actualYear = string.Empty;
        // GET: SIE_API
        public ActionResult Dashboard()
        {
            Serie objSerie = ClienteRest.Instance.serie;
            if (objSerie == null)
            {
                ModelState.AddModelError("", ClienteRest.Instance.error);
                return View("~/Views/Error.cshtml");
            }
            else
            {
                actualYear = DateTime.Now.Year.ToString();
                objLT.data = objSerie.Data.Where(f => f.Date.Split('/')[2].Equals(actualYear)).OrderBy(f => f.Date.Split('/')[1]).ToList();

                return View(objSerie);
            }
        }

        public JsonResult TablaDatos(string year)
        {
            year = year.Equals("0") ? actualYear : year;
            Serie objSerie = ClienteRest.Instance.serie;
            objLT.data = objSerie.Data.Where(f => f.Date.Split('/')[2].Equals(year)).OrderBy(f => f.Date.Split('/')[1]).ToList();

            string msjTabla = string.Format("Datos del año {0}", year);

            return Json(new { lt = objLT.data, msjTabla }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetValotes(string year)
        {
            year = year.Equals("0") ? actualYear : year;
            Serie objSerie = ClienteRest.Instance.serie;
            objLT.data = objSerie.Data.Where(f => f.Date.Split('/')[2].Equals(year)).OrderBy(f => f.Date.Split('/')[1]).ToList();

            foreach (var datos in objLT.data)
            {
                int mes = Convert.ToInt32(datos.Date.Split('/')[1]);
                meses += string.IsNullOrEmpty(meses) ? (mfi.GetMonthName(mes).ToString()) : "," + (mfi.GetMonthName(mes).ToString());
                valores += string.IsNullOrEmpty(valores) ? datos.Data : "," + datos.Data;
            }
            string msjGrafica = string.Format("Gráfica del año {0}", year);

            return Json(new { meses, valores, msjGrafica }, JsonRequestBehavior.AllowGet);
        }

    }
}