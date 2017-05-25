using SmartMarket.BLL;
using SmartMarket.BLL.Bank;
using SmartMarket.BLL.Managers;
using SmartMarket.BLL.Resources;
using SmartMarket.BLL.Services;
using SmartMarket.BLL.ViewModels;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Xml;

namespace SmartMarket.UI.Controllers
{
    public class PrivateController : Controller
    {
        IPrivateManager privateManager = new PrivateManager();

        private HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authorize(string card, string country)
        {
            card = "4149497847973767";
            country = "UA";

            var payment = $"<prop name=\"cardnum\" value=\"{card}\" />" +
                          $"<prop name=\"country\" value=\"{country}\" />";

            var data = $"<oper>cmt</oper>" +
                $"<wait>0</wait>" +
                $"<test>0</test>" +
                $"<payment id=\"\">{payment}</payment>";

            var sign = GenerateSign(data.ToString(), Private24.MerchantPassword);
            var merchant =  $"<id>{Private24.MerchantID}</id>" +
                            $"<signature>{sign}</signature>";

            var request = $"<merchant>{merchant}</merchant>" +
                $"<data>{data}</data>";

            var content = $"<request version=\"1.0\">{request}</request>";

            var result = sendXML("https://api.privatbank.ua/p24api/balance", content);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);

            var balance = xml.GetElementsByTagName("balance").Item(0).InnerText;
            var currency = xml.GetElementsByTagName("currency").Item(0).InnerText;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateBalance(CreateBalanceModel model)
        {
            model.CardNumber = GetDigits(model.CardNumber);

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var payment = $"<prop name=\"cardnum\" value=\"{model.CardNumber}\" />" +
                          $"<prop name=\"country\" value=\"{model.Country}\" />";

                    var data = $"<oper>cmt</oper>" +
                        $"<wait>0</wait>" +
                        $"<test>0</test>" +
                        $"<payment id=\"\">{payment}</payment>";

                    var sign = GenerateSign(data.ToString(), Private24.MerchantPassword);
                    var merchant = $"<id>{Private24.MerchantID}</id>" +
                                    $"<signature>{sign}</signature>";

                    var request = $"<merchant>{merchant}</merchant>" +
                        $"<data>{data}</data>";

                    var content = $"<request version=\"1.0\">{request}</request>";

                    var result = sendXML("https://api.privatbank.ua/p24api/balance", content);

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(result);

                    var balance = Convert.ToDecimal(xml.GetElementsByTagName("balance").Item(0).InnerText, CultureInfo.InvariantCulture);
                    //var currency = xml.GetElementsByTagName("currency").Item(0).InnerText;

                    privateManager.CreateBalance(model, balance);

                    var message = ResourceService.GetString(typeof(Strings), "BalanceCreated");
                    return JavaScript($"window.location = '{Url.Action("Index", "Home", new { message = message })}'");
                }
                return PartialView("~/Views/Partial/_CreateBalance.cshtml", model);
            }

            return RedirectToAction("Index", "Home");
        }

            private string GenerateSign(string data, string password)
        {
            string target = data + password;
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var md5 = System.Security.Cryptography.MD5.Create();

            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(target));
            var hash = ToHex(bytes);

            bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(hash));
            var result = ToHex(bytes);
            return result;
        }

        private string ToHex(byte[] bytes)
        {
            return bytes.Select(b => b.ToString("x2")).Aggregate("", (total, cur) => total + cur);
        }

        private string sendXML(string url, string xml)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            var bytes = Encoding.ASCII.GetBytes(xml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            var stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            var response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                stream = response.GetResponseStream();
                var result = new StreamReader(stream).ReadToEnd();
                return result;
            }
            return null;
        }

        private string GetDigits(string @string)
        {
            StringBuilder result = new StringBuilder();

            foreach (var @char in @string)
            {
                if (Char.IsDigit(@char))
                    result.Append(@char);
            }

            return result.ToString();
        }
    }
}