using AppDocumentation.Helpers;
using Bogus;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentation.Pages
{
    class AdoptionFormPage
    {
        [FindsBy]
        private IWebElement order_name = null, order_address = null, order_email = null, order_pay_type = null;
        [FindsBy(How = How.Name, Using = "commit")]
        private IWebElement commitBtn = null;
        private IWebDriver driver;
        public AdoptionFormPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public string OrderName { get { return order_name.GetText(); } private set { order_name.SetText(value); } }

        public string OrderAddress { get { return order_address.GetText(); } private set { order_address.SetText(value); } }

        public string OrderEmail { get { return order_email.GetText(); } private set { order_email.SetText(value); } }

        public string OrderPayType {
            get { return new SelectElement(order_pay_type).SelectedOption.GetText(); ; }
            private set { new SelectElement(order_pay_type).SelectByText(value); }
        }

        private IList<string> PayOptAvailable {
            get { return new SelectElement(order_pay_type).Options.Where(x => !x.GetText().Contains("Select a payment")).Select(x => x.GetText()).ToList(); }
        }

        public Faker<AdoptionFormPage> Generator()
        {
            var adoptionFormPage = new Faker<AdoptionFormPage>()
                .CustomInstantiator(f => new AdoptionFormPage(driver))
                .RuleSet("HappyPath",
                    (set) =>
                    {
                        set.StrictMode(true);
                        set.RuleFor(a => a.OrderName, f => f.Person.FirstName + " " + f.Person.LastName);
                        set.RuleFor(a => a.OrderAddress, f => f.Address.StreetAddress());
                        set.RuleFor(a => a.OrderEmail, f => f.Person.Email);
                        set.RuleFor(a => a.OrderPayType, f => f.PickRandom(PayOptAvailable));
                    })
                ;
            return adoptionFormPage;
        }

        public void Order() { commitBtn.Click(); }
    }

}
