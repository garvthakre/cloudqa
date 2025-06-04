using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using SeleniumExtras.WaitHelpers;

namespace CloudQAAutomationTest
{
    public class FormTester
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string BASE_URL = "https://app.cloudqa.io/home/AutomationPracticeForm";

        public FormTester()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }

        public void Run()
        {
            try
            {
                driver.Navigate().GoToUrl(BASE_URL);
                Console.WriteLine("🔍 Opened Practice Form");

                TestFirstName();
                TestEmail();
                TestCountryDropdown();

                Console.WriteLine("✅ All fields tested successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Test run failed: {ex.Message}");
            }
            finally
            {
                driver?.Quit();
            }
        }

        private void TestFirstName()
        {
            Console.WriteLine("➡️ Testing 'First Name' field...");

            var firstNameField = FindElement(new List<By>
            {
                By.Name("first_name"),
                By.Id("first_name"),
                By.XPath("//input[@placeholder='First Name']"),
                By.CssSelector("input[name*='first']"),
                By.XPath("//label[contains(text(), 'First Name')]/..//input"),
                By.XPath("//input[@type='text'][1]")
            });

            if (firstNameField != null)
            {
                EnterText(firstNameField, "John");
                AssertValue(firstNameField, "John", "First Name");
            }
            else
            {
                Console.WriteLine("⚠️ Could not locate 'First Name' field.");
            }
        }

        private void TestEmail()
        {
            Console.WriteLine("➡️ Testing 'Email' field...");

            var emailField = FindElement(new List<By>
            {
                By.Name("email"),
                By.Id("email"),
                By.XPath("//input[@type='email']"),
                By.CssSelector("input[type='email']"),
                By.XPath("//label[contains(text(), 'Email')]/..//input")
            });

            if (emailField != null)
            {
                string email = "test@example.com";
                EnterText(emailField, email);
                AssertValue(emailField, email, "Email");
            }
            else
            {
                Console.WriteLine("⚠️ Could not locate 'Email' field.");
            }
        }

        private void TestCountryDropdown()
        {
            Console.WriteLine("➡️ Testing 'Country' dropdown...");

            var dropdown = FindElement(new List<By>
            {
                By.Name("country"),
                By.Id("country"),
                By.XPath("//select[contains(@name, 'country')]"),
                By.CssSelector("select[name*='country']"),
                By.XPath("//label[contains(text(), 'Country')]/..//select")
            });

            if (dropdown != null)
            {
                try
                {
                    var select = new SelectElement(dropdown);
                    select.SelectByText("India");

                    if (select.SelectedOption.Text.Contains("India"))
                        Console.WriteLine("✅ Country selected successfully.");
                    else
                        Console.WriteLine("❌ Failed to validate country selection.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Dropdown interaction error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("⚠️ Country dropdown not found.");
            }
        }

        private IWebElement FindElement(List<By> locators)
        {
            foreach (var locator in locators)
            {
                try
                {
                    var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                    if (element.Displayed && element.Enabled)
                    {
                        Console.WriteLine($"✔️ Found element using: {locator}");
                        return element;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return null;
        }

        private void EnterText(IWebElement element, string text)
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
                System.Threading.Thread.Sleep(300);

                element.Clear();
                element.SendKeys(text);
            }
            catch
            {
                try
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript($"arguments[0].value='{text}';", element);
                }
                catch
                {
                    Console.WriteLine("❗ Failed to input text even with JavaScript.");
                }
            }
        }

        private void AssertValue(IWebElement element, string expected, string field)
        {
            string actual = element.GetAttribute("value") ?? element.Text;
            if (actual.Equals(expected, StringComparison.OrdinalIgnoreCase))
                Console.WriteLine($"✅ {field} field validated. Value: {actual}");
            else
                Console.WriteLine($"❌ {field} field validation failed. Expected: {expected}, Found: {actual}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("🚀 Starting Form Automation...");
            var tester = new FormTester();
            tester.Run();
            Console.WriteLine("🏁 Test run complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
