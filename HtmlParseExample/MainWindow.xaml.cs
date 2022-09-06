using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;

namespace HtmlParseExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private HtmlDocument d;
        List<string> list = new List<string>();

        private  void btnGetSite_Click(object sender, RoutedEventArgs e)
        {
            d = RetrieveHtml(txtSiteUrl.Text);

            var text = d.DocumentNode.Descendants()
                .Where(x => x.NodeType == HtmlNodeType.Text && x.InnerText.Trim().Length > 10
                    && !(x.InnerHtml.Trim().Contains("function")) && !(x.InnerHtml.Trim().Contains("![CDATA["))
                    && !(x.InnerHtml.Trim().Contains("var")) && !(x.InnerHtml.Trim().Contains("http://"))
                    && !(x.InnerHtml.Trim().Contains("SyntaxHighlighter"))
                    && !(x.InnerHtml.Trim().Contains("class"))
                    )
                .Select(x => x.InnerText.Trim());


            foreach (var a in text.Select(var2 => var2.Split(' ')))
            {
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = Regex.Replace(a[i], "[^a-zA-Zęńśćąóżźł]+", "");

                    if (a[i] != "" && a[i].Length >= 5)
                    {
                        list.Add(a[i]);

                    }
                }
            }

            lbWords.ItemsSource = list;
        }

        private static HtmlDocument RetrieveHtml(string WebsiteUrl)
        {
            HtmlWeb hw = new HtmlWeb();
            return hw.Load(WebsiteUrl);
        }



     
    }
}
