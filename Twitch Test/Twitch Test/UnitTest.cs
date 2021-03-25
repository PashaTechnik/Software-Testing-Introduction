using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;

namespace Twitch_Test
{
    public class Tests
    {
        public IWebDriver driver { get; set; }
        

        private readonly By _gameCategoryButton = By.CssSelector(".tw-align-items-center.tw-flex.tw-justify-content-between.vertical-selector__internal");
        private readonly By _registrationButton = By.ClassName("ScCoreButtonPrimary-sc-1qn4ixc-1");
        private readonly By _registrationNameField = By.XPath("//*[@id=\"signup-username\"]");
        private readonly By _errorCharactersField = By.XPath("/html/body/div[3]/div/div/div/div/div/div[1]/div/div/div[3]/form/div/div[1]/div/div[3]/div/div/p");
        private readonly By _chatTextField = By.ClassName("ScTextArea-sc-1ywwys8-0");
        private readonly By _sendToChat = By.CssSelector(".ScCoreButtonLabel-lh1yxp-0.xsINH.tw-core-button-label");
        private readonly By _logInButton =  By.XPath("//*[@id=\"root\"]/div/div[2]/nav/div/div[3]/div[3]/div/div[1]/div[1]/button/div/div");
        private readonly By _logInNameField = By.XPath("//*[@id=\"login-username\"]");
        private readonly By _logInEmailField = By.XPath("//*[@id=\"password-input\"]");
        private readonly By _logInButtonT = By.XPath("/html/body/div[3]/div/div/div/div/div/div[1]/div/div/div[3]/form/div/div[3]/button");
        private readonly By _searchField = By.CssSelector(".ScInputBase-sc-1wz0osy-0.ScInput-m6vr9t-0.idebMs.tw-border-bottom-left-radius-large.tw-border-bottom-right-radius-none.tw-border-top-left-radius-large.tw-border-top-right-radius-none.tw-font-size-5.tw-input.tw-input--large.tw-pd-l-1.tw-pd-r-1.tw-pd-y-05");
        private readonly By _searchButton = By.XPath("//*[@id=\"root\"]/div/div[2]/nav/div/div[2]/div/div/div/div/div[1]/div/button");
        
        
        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("/usr/local/bin/");
        }

        [Test]
        public void goToGameCategoryTest()
        {
            driver.Navigate().GoToUrl("https://www.twitch.tv/directory");
            Thread.Sleep(5000);
            var gameCategory = driver.FindElement(_gameCategoryButton);
            gameCategory.Click();
            Thread.Sleep(5000);
            Assert.IsTrue(driver.Url.Contains("https://www.twitch.tv/directory/gaming"), "Test Failed");
        }
        
        
        [Test]
        public void lessThanFourCharactersInNameTest()
        {
            driver.Navigate().GoToUrl("https://www.twitch.tv/");
            Thread.Sleep(3000);
            var gameCategory = driver.FindElement(_registrationButton);
            gameCategory.Click();
            Thread.Sleep(3000);
            var nameField = driver.FindElement(_registrationNameField);
            nameField.SendKeys("23");
            Thread.Sleep(3000);
        
            var actualErrorMessage = driver.FindElement(_errorCharactersField).Text;
            var expectedErrorMessage = "*Имя пользователя должно содержать от 4 до 25 символов.";
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            
        }
        
        [Test]
        public void sendMessageToChatErrorTest()
        {
            driver.Navigate().GoToUrl("https://www.twitch.tv/stray228");
            Thread.Sleep(3000);
        
            var chatTextField = driver.FindElement(_chatTextField);
            chatTextField.SendKeys("12345");
            var sendToChatButton = driver.FindElement(_sendToChat);
            sendToChatButton.Click();
            
        }
        
        
        [Test]
        public void test4()
        {
            driver.Navigate().GoToUrl("https://www.twitch.tv/");
            Thread.Sleep(3000);
        
            var logInButton = driver.FindElement(_logInButton);
            logInButton.Click();
            Thread.Sleep(3000);
            var logInNameField = driver.FindElement(_logInNameField);
            var logInEmailField =  driver.FindElement(_logInEmailField);
            
            
            logInNameField.SendKeys("1234");
            logInEmailField.SendKeys("1234");
            Thread.Sleep(3000);
        
            var but = driver.FindElement(_logInButtonT);
        
            Assert.IsFalse(but.Enabled, "Test Failed");
        }

        
        [Test]
        public void test5()
        {

            driver.Navigate().GoToUrl("https://www.twitch.tv/");
            Thread.Sleep(1000);
            
            var searchButton = driver.FindElement(_searchButton);
            var searchField = driver.FindElement(_searchField);
            
            Thread.Sleep(1000);
            
            searchField.SendKeys("1234");
            searchButton.Click();
            
        }
        
        [TearDown]
        public void closeBrowser()
        {
            Thread.Sleep(3000);
            driver.Close();
        }
    }
}