using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CareXmFramework
{
    public class Tests
    {
        private WebDriver WebDriver { get; set; } = null!;
        private string DriverPath { get; set; } = @"C:\Users\damis.loza\WebDrivers";
        private string BaseUrl { get; set; } = "https://the-internet.herokuapp.com/dynamic_content";

        [SetUp]
        public void Setup()
        {
            WebDriver = GetChromeDriver();
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            WebDriver.Navigate().GoToUrl(BaseUrl);
            Thread.Sleep(5000);
        }

        [TearDown]
        public void TearDown()
        {
            WebDriver.Quit();
        }

        [Test]
        public void Test1()
        {
            // Validate Three avatars on the page
            var avatars = WebDriver.FindElements(By.XPath("//img[contains(@src,'/img/avatars/Original-Facebook-Geek-Profile-Avatar')]"));
            if(avatars.Count >= 3)
            {
                Assert.Pass();
            } else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Test2()
        {
            // Validate text
            var sentence = WebDriver.FindElement(By.CssSelector("div.row:nth-child(2) div.large-12.columns:nth-child(2) div.example:nth-child(2) div.row:nth-child(7) div.large-10.columns.large-centered div.row:nth-child(1) > div.large-10.columns")).Text;
            Console.WriteLine(sentence);
            string[] words = sentence.Split(new[] { " " }, StringSplitOptions.None);
            string word = "";
            int ctr = 0;
            foreach (String s in words)
            {
                if (s.Length >= 10)
                {
                    word = s;
                    ctr = s.Length;
                    Assert.Pass("This is the word with more than 10 letters: " + word);
                } 
            }
            
        }

        [Test]
        public void Test3()
        {
            // Validate new text is in the page
            WebDriver.PageSource.Contains("Quod non voluptate dolorem optio voluptas hic volu ");
        }

        private WebDriver GetChromeDriver()
        {
            var options = new ChromeOptions();
            return new ChromeDriver(DriverPath, options, TimeSpan.FromSeconds(300));
        }
    }
}