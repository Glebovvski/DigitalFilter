using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DigitalFilter_DIPLOMA.Controllers
{
    public class HomeController : Controller
    {
        //int m = 50;
        const int d = 12;
        double b1k = 0;
        double b2 = 1 - Math.Pow(2, -d);
        double r;// = Math.Sqrt(b2);
        int k = 0;//k=0..m/2
        double xkk = 0;
        double x;//0,0.01..Pi
        double t;//r^m
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Index(int m = 50)
        {
            r = Math.Sqrt(b2);
            t = Math.Pow(r, m);
            double yH10 = 0;
            double yH18 = 0;
            double yH111 = 0;
            double yFull = 0;
            double yLogFull = 0;
            double akk = 0;
            Dictionary<string, double> H10 = new Dictionary<string, double>();
            Dictionary<string, double> H18 = new Dictionary<string, double>();
            Dictionary<string, double> H111 = new Dictionary<string, double>();
            Dictionary<string, double> Full = new Dictionary<string, double>();
            Dictionary<string, double> LogFull = new Dictionary<string, double>();

            for (double x = 0; x <= Math.PI; x += 0.01)
            {
                Complex Sum = 0;
                yH10 = Complex.Abs(1 - 1 * Complex.Exp(new Complex(0, -2 * x)));
                yH18 = Complex.Abs(1 - t * (Complex.Exp(new Complex(0, -m * x))));
                //nodes=Pi/(m/2)
                //show akk
                //set pp=кількість вузлів у зоні пропускання (2..20) default=4
                for (int k = 9; k <= 14; k++)
                {
                    switch (k)
                    {
                        case 9:
                        case 14:
                            akk = 0.35;
                            break;
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                            akk = 1;
                            break;
                    }

                    xkk = k * (2 * (Math.PI / m));
                    b1k = -2 * r * Math.Cos(xkk);
                    Sum += akk * (Math.Pow(-1, k) / (1 + b1k * Complex.Exp(new Complex(0, -x)) + b2 * Complex.Exp(new Complex(0, -2 * x))));

                }
                yH111 = Complex.Abs(Sum);
                yFull = yH10 * yH18 * (yH111 / m);
                yLogFull = -20 * Math.Log10(yFull);
                if (!Double.IsInfinity(yLogFull))
                { //yLogFull = 100;

                    H10.Add(x.ToString("0.00"), yH10);
                    H18.Add(x.ToString("0.00"), yH18);
                    H111.Add(x.ToString("0.00"), yH111);
                    Full.Add(x.ToString("0.00"), yFull);
                    LogFull.Add(x.ToString("0.00"), yLogFull);
                }
            }
            ViewBag.M = m;
            Serialize_H10(H10);
            Serialize_H18(H18);
            Serialize_H111(H111);
            Serialize_Full(Full);
            Serialize_LogFull(LogFull);
            return View();
        }

        private void CalculateGraphs(int m)
        {
            r = Math.Sqrt(b2);
            t = Math.Pow(r, m);
            double yH10 = 0;
            double yH18 = 0;
            double yH111 = 0;
            double yFull = 0;
            double yLogFull = 0;
            double akk = 0;
            Dictionary<string, double> H10 = new Dictionary<string, double>();
            Dictionary<string, double> H18 = new Dictionary<string, double>();
            Dictionary<string, double> H111 = new Dictionary<string, double>();
            Dictionary<string, double> Full = new Dictionary<string, double>();
            Dictionary<string, double> LogFull = new Dictionary<string, double>();

            for (double x = 0; x <= Math.PI; x += 0.01)
            {
                Complex Sum = 0;
                yH10 = Complex.Abs(1 - 1 * Complex.Exp(new Complex(0, -2 * x)));
                yH18 = Complex.Abs(1 - t * (Complex.Exp(new Complex(0, -m * x))));

                for (int k = 9; k <= 14; k++)
                {
                    switch (k)
                    {
                        case 9:
                        case 14:
                            akk = 0.35;
                            break;
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                            akk = 1;
                            break;
                    }

                    xkk = k * (2 * (Math.PI / m));
                    b1k = -2 * r * Math.Cos(xkk);
                    Sum += akk * (Math.Pow(-1, k) / (1 + b1k * Complex.Exp(new Complex(0, -x)) + b2 * Complex.Exp(new Complex(0, -2 * x))));

                }
                yH111 = Complex.Abs(Sum);
                yFull = yH10 * yH18 * (yH111 / m);
                yLogFull = -20 * Math.Log10(yFull);
                if (!Double.IsInfinity(yLogFull))
                { //yLogFull = 100;

                    H10.Add(x.ToString("0.00"), yH10);
                    H18.Add(x.ToString("0.00"), yH18);
                    H111.Add(x.ToString("0.00"), yH111);
                    Full.Add(x.ToString("0.00"), yFull);
                    LogFull.Add(x.ToString("0.00"), yLogFull);
                }
            }
            ViewBag.M = m;
            Serialize_H10(H10);
            Serialize_H18(H18);
            Serialize_H111(H111);
            Serialize_Full(Full);
            Serialize_LogFull(LogFull);
        }

        private void Serialize_H10(Dictionary<string, double> H10)
        {
            string[] arrX_H10 = H10.Keys.ToArray();
            string jsonX_H10 = Newtonsoft.Json.JsonConvert.SerializeObject(arrX_H10);
            ViewBag.jsonX_H10 = jsonX_H10;

            List<double> arrY_H10 = H10.Values.ToList();
            string jsonY_H10 = Newtonsoft.Json.JsonConvert.SerializeObject(arrY_H10);
            ViewBag.jsonY_H10 = jsonY_H10;
        }

        private void Serialize_H18(Dictionary<string, double> H18)
        {
            List<double> arrY_H18 = H18.Values.ToList();
            string jsonY_H18 = Newtonsoft.Json.JsonConvert.SerializeObject(arrY_H18);
            ViewBag.jsonY_H18 = jsonY_H18;
        }

        private void Serialize_H111(Dictionary<string, double> H111)
        {
            List<double> arrY_H111 = H111.Values.ToList();
            string jsonY_H111 = Newtonsoft.Json.JsonConvert.SerializeObject(arrY_H111);
            ViewBag.jsonY_H111 = jsonY_H111;
        }

        private void Serialize_Full(Dictionary<string, double> Full)
        {
            List<double> arrY_Full = Full.Values.ToList();
            string jsonY_Full = Newtonsoft.Json.JsonConvert.SerializeObject(arrY_Full);
            ViewBag.jsonY_Full = jsonY_Full;
        }

        private void Serialize_LogFull(Dictionary<string, double> LogFull)
        {
            List<double> arrY_LogFull = LogFull.Values.ToList();
            string jsonY_LogFull = Newtonsoft.Json.JsonConvert.SerializeObject(arrY_LogFull);
            ViewBag.jsonY_LogFull = jsonY_LogFull;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}