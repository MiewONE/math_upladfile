using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Keys = OpenQA.Selenium.Keys;
using System.IO;

namespace upload_file
{
    public partial class Form1 : Form
    {
        string[] files = null;
        string pwd = "my5341tutm",id="212062";
        IWebDriver dv = new ChromeDriver();
        //WebDriverWait wait = new WebDriverWait(dv, new TimeSpan(0, 0, 5));
        public Form1()
        {
            InitializeComponent();

        }
        public void run()
        {
            dv.Url = "https://inno.tu.ac.kr/tu/cop/";
            ///html/body/div[1]/div[1]/div[1]/div/div/div/div[2]/div/form/div[1]/span[1]/input
            IWebElement ele = dv.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[4]/div/div/form/div[1]/p[1]/input"));
            ele.Clear();
            ele.SendKeys(id);
            ele = dv.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[4]/div/div/form/div[1]/p[2]/input"));
            ele.Clear();
            ele.SendKeys(pwd);
            dv.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[4]/div/div/form/div[2]/p[1]/input[1]")).Click();
            isAlertPresent();
            dv.FindElement(By.XPath("/html/body/header/div/div/div[1]/div/div[1]/div/ul/li[1]/a")).Click();
            xpathClick("/html/body/div/div[1]/div[1]/div/div/div/div[2]/div/div/div[1]/a/span");
            isAlertPresent();

            isAlertPresent();
            dv.Url = "https://inno.tu.ac.kr/myspace/create/";

            xpathClick("/html/body/div/div/div/div/main/div/div/div/div[1]/ul/li[2]/a/i");//좌측 프레임 동영상 업로드 라벨 누르기

            for(int i=0;i<files.Length;i++)
            {
                upload(files[i]);
                //IWebElement eles = dv.FindElement(By.CssSelector("body"));
                Thread.Sleep(10000);
                //eles.SendKeys(Keys.Control + "t");
                //dv.SwitchTo().Window(dv.WindowHandles.Last());
                //dv.Navigate().GoToUrl("https://inno.tu.ac.kr/myspace/create/?mode=videoupload");
                dv.Navigate().Refresh();
                isAlertPresent();
                xpathClick("/html/body/div/div/div/div/main/div/div/div/div[1]/ul/li[2]/a/i");//좌측 프레임 동영상 업로드 라벨 누르기
            }
            

        }
        public void xpathClick(string xpath)
        {
            isAlertPresent();
            WaitForVisivle(dv, By.XPath(xpath));
            dv.FindElement(By.XPath(xpath)).Click();
        }
        public void upload(string file)
        {
            WaitForVisivle(dv, By.XPath("/html/body/div/div/div/div/main/div/div/div/div[3]/div[2]/div/div/div[1]/div/form/div[1]/input"));
            dv.FindElement(By.XPath("/html/body/div/div/div/div/main/div/div/div/div[3]/div[2]/div/div/div[1]/div/form/div[1]/input")).SendKeys(file);
            ///html/body/div/div/div/div/main/div/div/div/div[3]/div[2]/div/div/div[1]/div/form/div[1]/label
        }
        public bool isAlertPresent()
        {
            try
            {
                dv.SwitchTo().Alert().Accept();
                return true;
            }   // try 
            catch (NoAlertPresentException Ex)
            {
                return false;
            }   // catch 
        }   // isAlertPresent()
        private static bool WaitForVisivle(IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileread();
            run();
        }

        public void fileread()
        {
            int cnt = 0;
            
            string _path = textBox1.Text;
            string path = "C:\\Users\\dlsrk\\Documents\\카카오톡 받은 파일\\ㄷㄹ\\13";
            DirectoryInfo di = new DirectoryInfo(_path);
            cnt = di.GetFiles("*.*", SearchOption.AllDirectories).Length;
            files = new string[cnt];
            int i = 0;
            foreach(FileInfo fi in di.GetFiles())
            {
                files[i] = fi.FullName;
                i++;
            }
        }
    }
    //public static class WebDriverExtensions
    //{
    //    public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
    //    {
    //        if (timeoutInSeconds > 0)
    //        {
    //            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
    //            return wait.Until(drv => drv.FindElement(by));
    //        }
    //        return driver.FindElement(by);
    //    }
    //}
}
