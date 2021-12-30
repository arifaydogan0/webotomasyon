using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace webotomasyon
{
    static class Program
    {

        static void Main()
        {
            IWebDriver webDriver = new ChromeDriver();
            var options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument("--disable-gpu");
        againAdd:
            webDriver.Navigate().GoToUrl(@"https:\\www.beymen.com\"); Thread.Sleep(1750);
            var anasayfa = webDriver.FindElement(By.XPath("/html/body/header/div/div/div[1]/a/img")); anasayfa.Click();
            var hesap = webDriver.FindElement(By.XPath("/html/body/header/div/div/div[3]/div/a[1]")); hesap.Click(); Console.Write(" hesap kontrol ediliyor.. "); Thread.Sleep(1750); webDriver.Navigate().Back(); Thread.Sleep(1750);
            var favoriler = webDriver.FindElement(By.XPath("/html/body/header/div/div/div[3]/div/a[2]")); favoriler.Click(); Console.Write(" favoriler kontrol ediliyor.. "); Thread.Sleep(1750); webDriver.Navigate().Back(); Thread.Sleep(1750);
            var sepetim = webDriver.FindElement(By.XPath("/html/body/header/div/div/div[3]/div/a[3]")); sepetim.Click(); Console.Write(" sepetim kontrol ediliyor.. "); Thread.Sleep(1750); webDriver.Navigate().Back(); Thread.Sleep(1750);


            webDriver.FindElement(By.XPath("/html/body/header/div/div/div[2]/div/div/div/input")).SendKeys("pantolon"); Thread.Sleep(1750);
            webDriver.FindElement(By.XPath("/html/body/div[3]")).Click(); Thread.Sleep(1750);
            webDriver.FindElement(By.XPath("/html/body/header/div/div/div[2]/div/button")).Click(); Thread.Sleep(1750);

            IJavaScriptExecutor js = webDriver as IJavaScriptExecutor;

            long initialHeight = (long)(js.ExecuteScript("return document.body.scrollHeight"));

            while (true)
            {
                js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");
                long sayac = (long)(js.ExecuteScript("return document.body.scrollHeight"));
                Thread.Sleep(2000);
                if (initialHeight == sayac)
                    break;
                initialHeight = sayac;
            }
            Thread.Sleep(750);

            webDriver.FindElement(By.Id("moreContentButton")).Click(); Thread.Sleep(1750);
            var linkList = webDriver.FindElements(By.CssSelector("a"));
            List<string> productLinkList = new List<string>();
            for (int i = 0; i < linkList.Count; i++)
            {
                string link = linkList[i].GetAttribute("href");
                if (link == null)
                    continue;

                if (link.Contains("pantolon"))
                    productLinkList.Add(linkList[i].GetAttribute("href"));

            }
            int index = new Random().Next(productLinkList.Count / 3, productLinkList.Count);
            webDriver.Navigate().GoToUrl(@productLinkList[index]); Thread.Sleep(1750);

            var list = webDriver.FindElements(By.CssSelector("span"));
            int a = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                string link = list[i].GetAttribute("class");

                if (link == "m-variation__item")
                {
                    switch (a)
                    {
                        case 0: webDriver.FindElement(By.XPath("/html/body/div[3]/div[1]/div/div[2]/div[2]/div[3]/div/div/span[1]")).Click(); break;
                        case 1: webDriver.FindElement(By.XPath("/html/body/div[3]/div[1]/div/div[2]/div[2]/div[3]/div/div/span[2]")).Click(); break;
                        case 2: webDriver.FindElement(By.XPath("/html/body/div[3]/div[1]/div/div[2]/div[2]/div[3]/div/div/span[3]")).Click(); break;
                        case 3: webDriver.FindElement(By.XPath("/html/body/div[3]/div[1]/div/div[2]/div[2]/div[3]/div/div/span[4]")).Click(); break;
                        case 4: webDriver.FindElement(By.XPath("/html/body/div[3]/div[1]/div/div[2]/div[2]/div[3]/div/div/span[5]")).Click(); break;
                        case 5: webDriver.FindElement(By.XPath("/html/body/div[3]/div[1]/div/div[2]/div[2]/div[3]/div/div/span[6]")).Click(); break;
                        default: goto againAdd;
                    }
                    Thread.Sleep(1750);
                    break;
                }
                if (link.Contains("m-variation__item"))
                    a++;
                if (i == list.Count() - 1)
                {
                    Console.WriteLine("Ürün tükenmek üzere, başka ürün aranıyor.");
                    goto againAdd;
                }
            }
            Thread.Sleep(1750);
            string fiyat = webDriver.FindElement(By.Id("priceNew")).Text;
            webDriver.FindElement(By.Id("addBasket")).Click(); Thread.Sleep(1750);
            webDriver.FindElement(By.XPath("/html/body/header/div/div/div[3]/div/a[3]/span")).Click(); Thread.Sleep(1750);

            var fiat = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[1]/div[2]/div[1]/div/div/div[1]/div[1]/div/div/span"));
            string fiyatyolu = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[1]/div[2]/div[1]/div/div/div[1]/div[1]/div/div/span")).GetAttribute("class");
            if (fiyatyolu != "m-productPrice__salePrice")
            {
                fiat = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[1]/div[2]/div[1]/div/div/div[1]/div[1]/div/div[2]/span[2]"));
            }
            if (fiyat == fiat.Text)
                Console.WriteLine("Liste fiyatıyla sepet fiyatı karşılaştırıldı, fiyatlar doğru.");

            if (webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[1]/div[2]/div[1]/div/div/div[1]/div[2]/ul/li[3]/div[2]/div/select/option[2]")).Text != null)
                Console.WriteLine("2. Adet Eklendi.");
            else
            {
                webDriver.FindElement(By.Id("removeCartItemBtn0")).Click(); Thread.Sleep(1750);
                goto againAdd;
            }

            webDriver.FindElement(By.Id("quantitySelect0")).Click(); Thread.Sleep(1750);
            webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[1]/div[2]/div[1]/div/div/div[1]/div[2]/ul/li[3]/div[2]/div/select/option[2]")).Click(); Thread.Sleep(1750);
            webDriver.FindElement(By.Id("removeCartItemBtn0")).Click(); Console.WriteLine("Sepet Boşaltıldı."); Thread.Sleep(1750);

            if ((webDriver.FindElement(By.ClassName("m-empty__messageTitle")).Text) == "SEPETINIZDE ÜRÜN BULUNMAMAKTADIR")
                Console.WriteLine("Sepetin boş olduğu teyit edildi.");

            Thread.Sleep(1750);
            Console.ReadLine();
        }
    }
}
