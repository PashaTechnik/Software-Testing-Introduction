
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Twitch_Test
{
    public class BingMainPage
    {
        private readonly IWebDriver driver;
        private readonly string url = "https://www.twitch.tv/";
 
        public BingMainPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }
        
        public BingMainPage()
        {
            driver = new ChromeDriver("/usr/local/bin/");
        }
        

        [FindsBy(How = How.CssSelector, Using = ".ScInputBase-sc-1wz0osy-0.ScInput-m6vr9t-0.idebMs.tw-border-bottom-left-radius-large.tw-border-bottom-right-radius-none.tw-border-top-left-radius-large.tw-border-top-right-radius-none.tw-font-size-5.tw-input.tw-input--large.tw-pd-l-1.tw-pd-r-1.tw-pd-y-05")]
        public IWebElement SearchField { get; set; }

        [FindsBy(How = How.Id, Using = "//*[@id=\"root\"]/div/div[2]/nav/div/div[2]/div/div/div/div/div[1]/div/button")]
        public IWebElement SearchButton { get; set; }
 
        
 
        public void Navigate()
        {
            driver.Navigate().GoToUrl(this.url);
        }

        public void Close()
        {
            Thread.Sleep(10000);
            driver.Close();
        }

        public void Search(string textToType)
        {
            
            SearchField.SendKeys(textToType);
            SearchButton.Click();
        }
 
        public void ValidateResultsCount(string expectedUrl)
        {
            Assert.IsTrue(driver.Url.Contains(expectedUrl), "Something Wrong");
        }
    }
}


