using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using currency_exchange.Models;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Net.Http;
using System.Xml.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace currency_exchange.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            ExchangeViewModel model = new ExchangeViewModel();
            model.codes = new List<string>();
            model.passedData = new PassedData();
            model.valCurs = new ValCurs();
            
             string date = "01.11.2021";
            
            string url = "https://www.cbar.az/currencies/" + date + ".xml";


            WebClient webClient = new WebClient();
            string xmlData = webClient.DownloadString(url);


            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ValCurs));
            using (StringReader xmlReader = new StringReader(xmlData))
            {
                model.valCurs = (ValCurs)(xmlSerializer.Deserialize(xmlReader));
            }


            for (int i = 0; i < model.valCurs.ValType[1].Valute.Count; i++)
            {
                model.codes.Add(model.valCurs.ValType[1].Valute[i].Code);
            }

            
            /*PassedData p = new PassedData();
            string date = null;
            date = p.Date;
            ValCurs valCurs = new ValCurs();
            if (date!=null)
            {
                date = p.Date;
            }
            else
            {
                date = "01.11.2021";
            }
            string url = "https://www.cbar.az/currencies/"+date+".xml";
            

            WebClient webClient = new WebClient();
            string xmlData = webClient.DownloadString(url);


            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ValCurs));
            using (StringReader xmlReader = new StringReader(xmlData))
            {
                valCurs = (ValCurs)(xmlSerializer.Deserialize(xmlReader));
            }

            foreach (var valType in valCurs.ValType)
            {
                if (valType.Type == "Xarici valyutalar")
                {
                    ViewBag.Code = valType.Valute[0].Code;
                }
            }

            ViewData["ValCurs"] = valCurs;
            ViewData["PassedData"] = p;
            ViewBag.Date = p.Date;*/


            return View(model);

            /*System.IO.File.WriteAllText("file.xml", xmlData);

            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));


            XmlSerializer ser = new XmlSerializer(valCurs.GetType(), new XmlRootAttribute("ValCurs"));

            valCurs = ser.Deserialize(System.IO.File.OpenRead("file.xml")) as ValCurs;*/
            
        }


        [HttpPost]
        public ActionResult Index(ExchangeViewModel model)
        {
            model.valCurs = new ValCurs();
            model.codes = new List<string>();
            //DateTime date = new DateTime();
            string dt = model.passedData.Date;
            DateTime Date = Convert.ToDateTime(dt);
            if (model.passedData.Date == null)
            {
                Date = DateTime.Now;
            }
            string date = Date.ToShortDateString();
            model.passedData.Date = date;

            
            
            string url = "https://www.cbar.az/currencies/"+ date +".xml";


            WebClient webClient = new WebClient();
            string xmlData = webClient.DownloadString(url);


            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ValCurs));
            using (StringReader xmlReader = new StringReader(xmlData))
            {
                model.valCurs = (ValCurs)(xmlSerializer.Deserialize(xmlReader));
            }

            for (int i = 0; i < model.valCurs.ValType[1].Valute.Count; i++)
            {
                model.codes.Add(model.valCurs.ValType[1].Valute[i].Code);
            }

            double AmountMoney = model.passedData.AmountMoney;
            double AmountMoneyFromTo = 0;
            double ValueFrom = 0;
            double ValueTo = 0;

            foreach (var valType in model.valCurs.ValType)
            {
                if (valType.Type == "Xarici valyutalar")
                {
                    for (int i = 0; i < valType.Valute.Count; i++)
                    {
                        
                        if (valType.Valute[i].Code == model.passedData.FromValute)
                        {
                            ValueFrom = valType.Valute[i].Value;
                            ViewBag.IndexFromValute = i;
                        }
                        else if (valType.Valute[i].Code == model.passedData.ToValute)
                        {
                            ValueTo = valType.Valute[i].Value;
                            ViewBag.IndexToValute = i;
                        }
                        

                    }
                }
            }
            double PriceFromToValute = ValueFrom / ValueTo;
                       
            
            if (model.passedData.FromValute == "AZN")
            {
                AmountMoneyFromTo = AmountMoney / ValueTo ;
            }
            else if (model.passedData.ToValute == "AZN")
            {
                AmountMoneyFromTo = AmountMoney * ValueTo;
            }
            else if (model.passedData.FromValute == model.passedData.ToValute)
            {
                PriceFromToValute = 1;
                AmountMoneyFromTo = AmountMoney;
            }
            else 
            {
                AmountMoneyFromTo = PriceFromToValute * AmountMoney;
            }

            ViewBag.PriceFromToValute = PriceFromToValute;
            ViewBag.AmountMoneyFromTo = AmountMoneyFromTo;            
            


            /*List<ValuteCode> ListCodes = new List<ValuteCode>()
            {
                new ValuteCode() {Id = 1, Name="IT" },
                new Department() {Id = 2, Name="HR" },
                new Department() {Id = 3, Name="Payroll" },
            };*/

            /*--------List<ValuteCode> ListValuteCodes = new List<ValuteCode>();

            for (int i = 0; i < valCurs.ValType[1].Valute.Count ; i++)
            {
                ListValuteCodes.Add(new ValuteCode { Code = valCurs.ValType[1].Valute[i].Code, Value = i.ToString() });
            }

            ViewBag.ListValuteCodes = new SelectList(ListValuteCodes,"Code", "Value");
            ViewData["ListValuteCodes"] = new SelectList(ListValuteCodes,"Value");
            ViewBag.codes = ListValuteCodes;*/


            return View(model);


        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
