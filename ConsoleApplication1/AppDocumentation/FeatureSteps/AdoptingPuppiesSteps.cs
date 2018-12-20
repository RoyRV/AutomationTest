using AppDocumentation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace AppDocumentation.FeatureSteps
{
    [Binding]
    public class AdoptingPuppiesSteps
    {
        IWebDriver driver;

        [Given(@"I am on the puppy adoption site")]
        public void GivenIAmOnThePuppyAdoptionSite()
        {
            string baseURL = "http://puppies.herokuapp.com";
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.Manage().Window.Maximize();
        }
        
        [When(@"I complete the adoption of a puppy")]
        public void WhenICompleteTheAdoptionOfAPuppy()
        {
            driver.FindElement(By.CssSelector("input.rounded_button")).Click();
            driver.FindElement(By.CssSelector("input.rounded_button")).Click();
            driver.FindElement(By.Id("toy")).Click();
            driver.FindElement(By.Id("vet")).Click();
            driver.FindElement(By.XPath("//input[@value='Adopt Another Puppy']")).Click();
            driver.FindElement(By.XPath("(//input[@value='View Details'])[3]")).Click();
            driver.FindElement(By.CssSelector("input.rounded_button")).Click();
            driver.FindElement(By.XPath("(//input[@id='carrier'])[2]")).Click();
            driver.FindElement(By.Id("collar")).Click();
            driver.FindElement(By.CssSelector("input.rounded_button")).Click();

            var adoptionFormPage = new AdoptionFormPage(driver);
            adoptionFormPage.Generator()
                .StrictMode(false)
                .RuleFor(a => a.OrderName, f => "Leonard Espiritu")
                .Generate("HappyPath, Default");

            adoptionFormPage.Order();
        }
        
        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string p0)
        {
            Assert.AreEqual("Thank you for adopting a puppy!", driver.FindElement(By.Id("notice")).Text);
            driver.Quit();
        }
    }
}
