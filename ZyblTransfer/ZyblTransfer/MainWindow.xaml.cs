using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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

namespace ZyblTransfer {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {

        const string tagHtmlDef = "<html>";
        const string tagBodyDef = "<body>";
        const string tagTitleDef = "</title>";
        const string tagBodyDayNightDef = "</body>";

        const string wcss_path = "\"https://www.zybuluo.com/static/assets/template-theme-white.css\"";
        const string bcss_path = "\"https://www.zybuluo.com/static/assets/template-theme-black.css\"";
        const string jquery_path = "\"http://apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js\"";

        const string tagHtmlWhite = "<html class=\"theme theme-white\">";
        const string tagBodyWhite = "<body class=\"theme theme-white\">";
        const string tagBodyDayNight = "<div title=\"切换主题\" class=\"qiehuan\" id=\"btn\">\n" +
                "<div class=\"i1\"></div>\n" +
                "<div class=\"i2\"></div>\n" +
                "</div>\n" +
                "<div title=\"返回顶部\" class=\"gotop\" id=\"btn_gotop\">\n" +
                "<svg t=\"1515054971049\" class=\"icon\" style=\"\" viewBox=\"0 0 1024 1024\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" p-id=\"1864\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" width=\"18\" height=\"18\">\n" +
                "<defs><style type=\"text/css\"></style></defs>\n" +
                "<path d=\"M512 992a480 480 0 1 0 0-960 480 480 0 0 0 0 960z m169.856-392l44.288-46.144L501.824 338.56l-205.056 216.32 46.464 43.968L504 429.44l177.856 170.624zM512 928a416 416 0 1 1 0-832 416 416 0 0 1 0 832z\" p-id=\"1865\"></path>\n" +
                "</svg>\n" +
                "</div>\n" +
                "</body>\n" +
                "<script src="+jquery_path+"></script>\n" +
                "<script type=\"text/javascript\">\n" +
                "\tfunction includeLinkStyle(url) {\n" +
                "\t\tvar link = document.createElement(\"link\");\n" +
                "\t\tlink.href = url;\n" +
                "\t\tlink.rel = \"stylesheet\";\n" +
                "\t\tlink.media = \"screen\";\n" +
                "\t\tlink.class = \"lk-black\"\n" +
                "\t\tdocument.getElementsByTagName(\"head\")[0].appendChild(link);\n" +
                "\t}\n" +
                "\tfunction removeLinkStyle() {\n" +
                "\t\t$(\"link\").remove(\".lk-white\");\n" +
                "\t}\n" +
                "\tvar btn_switch=document.getElementById('btn');\n" +
                "\tvar i=true;\n" +
                "\tbtn_switch.onclick=function(){\n" +
                "\t\tif(i){\n" +
                "\t\t\tremoveLinkStyle();\n" +
                "\t\t\tincludeLinkStyle("+bcss_path+");\n" +
                "\t\t\t$('code').removeClass('code-white').addClass('code-black');\n" +
                "\t\t\t$('.theme').removeClass(\"theme-white\").addClass(\"theme-black\");\n" +
                "\t\t\t$('.white-blockquote').removeClass(\"white-blockquote\").addClass('black-blockquote');\n" +
                "\t\t\t$('.table-striped-white').removeClass('table-striped-white').addClass('table-striped-black');\n" +
                "\t\t\t$('pre').removeClass(\"code-white\").addClass(\"code-black\");\n" +
                "\t\t\ti=false;\n" +
                "\t\t\t$('#btn').css(\"background-color\",\"#f9f9f5\");\n" +
                "\t\t\t$('h1').css(\"color\",\"#d6dbdf\");\n" +
                "\t\t\t}else{\n" +
                "\t\t\t$('#btn').css(\"background-color\",\"#444444\");\n" +
                "\t\t\t$('h1').css(\"color\",\"#333333\");\n" +
                "\t\t\twindow.location.reload();\n" +
                "\t\t}\n" +
                "\t}\n" +
                "\tvar btn_gotop = document.getElementById('btn_gotop');\n" +
                "\tbtn_gotop.onclick = function(){    \n" +
                "\t\tvar timer = setInterval(function() {\n" +
                "\t\twindow.scrollBy(0, -100);\n" +
                "\t\tif (scrollTop == 0) \n" +
                "\t\t\tclearInterval(timer);\n" +
                "\t\t}, 2);\n" +
                "\t\twindow.onscroll = function() {\n" +
                "\t\tscrollTop = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;\n" +
                "\t\t\tobj.style.display = (scrollTop >= 300) ? \"block\" : \"none\";\n" +
                "\t\t}\n" +
                "\t}\n" +
                "</script>";
        const string tagTitleWhite = "</title>\n" +
            "<link href="+wcss_path+" rel=\"stylesheet\" media=\"screen\">\n" +
            "<style type=\"text/css\">\n" +
            "#wmd-preview h1  {\n" +
            "    color: #333333; /* 将标题改为黑色 */\n" +
            "}\n" +
            ".theme-white{\n" +
            "\tbackground-color: #ffffff;\n" +
            "}\n" +
            "</style>";
        const string tagTitleDayNight = "</title>\n" +
                "<link href=" + wcss_path + " rel=\"stylesheet\" media=\"screen\" class=\"lk-white\">\n" +
                "<style type=\"text/css\">\n" +
                "#wmd-preview h1  {\n" +
                "\tcolor: #333333; /* 将标题改为黑色 */\n" +
                "}\n" +
                ".qiehuan{\n" +
                "\tposition: fixed;\n" +
                "\tbottom: 100px;\n" +
                "\tright: 60px;\n" +
                "\tdisplay: block;\n" +
                "\twidth: 18px;\n" +
                "\theight: 18px;\n" +
                "\tborder: 0px solid #ffffff;\n" +
                "\tborder-radius: 50%;\n" +
                "\ttext-align: center;\n" +
                "\toverflow:hidden;\n" +
                "\tbackground-color: #444444\n" +
                "}\n" +
                ".gotop{\n" +
                "\tposition: fixed;\n" +
                "\tbottom: 60px;\n" +
                "\tright: 60px;\n" +
                "\tdisplay: block;\n" +
                "}\n" +
                "</style>";

        const string tagHtmlBlack = "<html class=\"theme theme-black\">";
        const string tagBodyBlack = "<body class=\"theme theme-black\">";
        const string tagTitleBlack = "</title>\n" +
            "<link href=" + bcss_path + " rel=\"stylesheet\" media=\"screen\">\n" +
            "<style type=\"text/css\">\n" +
            "#wmd-preview h1  {\n" +
            "    color: #d6dbdf; /* 将标题改为灰色 */\n" +
            "}</style>";

        const string tagDivDef = "class=\"wmd-preview\"";
        const string tagDivStyle = "class=\"wmd-preview wmd-preview-full-reader\"";

        const string tagWhite1 = "-white";
        const string tagWhite2 = "white-";
        const string tagWhite3 = "class=\"\">";
        const string tagWhite4 = "<code>";
        const string tagWhite5 = "<pre data-anchor-id";

        const string tagBlack1 = "-black";
        const string tagBlack2 = "black-";
        const string tagBlack3 = "class=\"code-black\">";
        const string tagBlack4 = "<code class=\"code-black\">";
        const string tagBlack5 = "<pre class=\"code-black\" data-anchor-id";

        string filePath = string.Empty;
        public MainWindow() {
            InitializeComponent();
            //InitConfig();//配置初始化
            tb_out_path.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            
            base.OnClosing(e);
        }

        private void InitConfig() {

            bool isFirstRun = bool.Parse(ConfigurationManager.AppSettings["first_run"]);

            if (isFirstRun){ //首次运行
                string out_path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); 
                SaveUserConfig(out_path);
                SaveRadioButtonStatus(rb_name_a);
                SaveRadioButtonStatus(rb_name_b);
                SaveRadioButtonStatus(rb_name_c);
                SaveRadioButtonStatus(rb_them_a);
                SaveRadioButtonStatus(rb_them_b);
                SaveRadioButtonStatus(rb_them_c);
            }

            InitRadioButtonStatus(rb_name_a);
            InitRadioButtonStatus(rb_name_b);
            InitRadioButtonStatus(rb_name_c);
            InitRadioButtonStatus(rb_them_a);
            InitRadioButtonStatus(rb_them_b);
            InitRadioButtonStatus(rb_them_c);
            InitOutPath();
        }

        private void InitRadioButtonStatus(object sender)
        {
            RadioButton rb = sender as RadioButton;
            rb.IsChecked = bool.Parse(ConfigurationManager.AppSettings[rb.Name]);
        }

        private void SaveUserConfig(string out_path)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfg.AppSettings.Settings["first_run"].Value = "False";
            cfg.AppSettings.Settings["out_path"].Value = out_path;
            cfg.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void InitOutPath()
        {
            string dir = ConfigurationManager.AppSettings["out_path"];
            tb_out_path.Text = dir;
        }

        private void SaveOutPath(string path)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
           
            cfg.AppSettings.Settings["out_path"].Value = path;
            cfg.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void gd_transfer_Drop(object sender, DragEventArgs e) {
            filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //获得文件名后的操作...
            string suffix = string.Empty; //输出文件名后缀
            string fileName = string.Empty; //输入文件名
            string outFileName = string.Empty; //输出文件名
            string fileOutPath = string.Empty; //文件输出路径

            if (filePath.Contains(".") == false)
            {
                tbk_status.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xca, 51, 00)); //红色
                tbk_status.Text = "不是 HTML 文档，暂不支持转换！";
                return;
            }

            //从路径中截取出文件名
            fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1, filePath.LastIndexOf(".") - (filePath.LastIndexOf("\\") + 1));

            if (rb_name_a.IsChecked == true) {
                //convert后缀
                suffix = "-style";
            }
            if (rb_name_b.IsChecked == true) {
                //时间后缀
                suffix = DateTime.Now.ToString("-yy-MM-dd-HH-mm") + DateTime.Now.Millisecond;
            }
            if (rb_name_c.IsChecked == true) {
                //时间名称
                fileName = string.Empty;
                suffix = DateTime.Now.ToString("yy-MM-dd-HH-mm") + DateTime.Now.Millisecond;
            }

            outFileName = fileName + suffix;

            //从路径中截取出文件父路径
            fileOutPath = tb_out_path.Text+"\\"+ outFileName + ".html";

            if (isStyleHtml(filePath)) {
                tbk_status.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 21, 133, 21)); //绿色
                tbk_status.Text = "该文档已经具备样式，无需转换！";
                return;
            }

            if (isHtml(filePath) ==false) {
                tbk_status.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xca, 51, 00)); //红色
                tbk_status.Text = "不是 HTML 文档，暂不支持转换！";
                return;
            }

            //生成文件
            if (rb_them_a.IsChecked == true) {
                genderDayFile(filePath, suffix, fileOutPath);
            }
            if (rb_them_b.IsChecked == true) {
                genderNightFile(filePath, suffix, fileOutPath);
            }
            if (rb_them_c.IsChecked == true) {
                genderDayNightFile(filePath, suffix, fileOutPath);
            }
            tbk_status.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 21, 133, 21)); //绿色
            tbk_status.Text = "完成转换\n新文件位于：\n"+fileOutPath;
        }

        private bool isHtml(string filePath)
        {
            string suffix = filePath.Substring(filePath.LastIndexOf("."));
            if (suffix.Equals(".html") || suffix.Equals(".HTML")) {
                return true;
            }
            return false;
        }

        private bool isStyleHtml(string filePath)
        {
            //bool isStyleHtml = false;
            string strContent = string.Empty;
            String line = string.Empty;
            if (!System.IO.File.Exists(filePath))
            {
                tbk_status.Foreground = new SolidColorBrush(Color.FromArgb(0,0xff,45,00)); //红色
                tbk_status.Text = "文件路径有误！";
                return false;
            }
            StreamReader reader = new StreamReader(filePath);
            int i = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (i > 10) {
                    return false;
                }
                if (line.Contains("<html class=\"theme"))
                {
                    return true;
                }
                i++;
            }
            return false;
        }

        private void genderDayNightFile(string filePath, string suffix, string fileOutPath) {
            string strContent = string.Empty;
            String line = string.Empty;
            if (!System.IO.File.Exists(filePath)) {
                return;
            }

            StreamReader reader = new StreamReader(filePath);
            StreamWriter writer = new StreamWriter(fileOutPath);

            while ((line = reader.ReadLine()) != null) {
                string strnewLine = line;
                if (line.Contains(tagHtmlDef)) {
                    strnewLine = strnewLine.Replace(tagHtmlDef, tagHtmlWhite);
                }
                if (line.Contains(tagBodyDef)) {
                    strnewLine = strnewLine.Replace(tagBodyDef, tagBodyWhite);
                }
                if (line.Contains(tagTitleDef)) {
                    strnewLine = strnewLine.Replace(tagTitleDef, tagTitleDayNight);
                }
                if (line.Contains(tagDivDef)) {
                    strnewLine = strnewLine.Replace(tagDivDef, tagDivStyle);
                }
                if (line.Contains(tagBodyDayNightDef)) {
                    strnewLine = strnewLine.Replace(tagBodyDayNightDef, tagBodyDayNight);
                }
                writer.WriteLine(strnewLine);
            }
            writer.Flush();
            writer.Close();
            reader.Close();
        }


        private void genderNightFile(string filePath, string suffix, string fileOutPath) {
           
            string strContent = string.Empty;
            String line = string.Empty;
            if (!System.IO.File.Exists(filePath)) {
                return;
            }

            StreamReader reader = new StreamReader(filePath);
            StreamWriter writer = new StreamWriter(fileOutPath);

            while ((line = reader.ReadLine()) != null) {
                string strnewLine = line;
                if (line.Contains(tagHtmlDef)) {
                    strnewLine = strnewLine.Replace(tagHtmlDef, tagHtmlBlack);
                }
                if (line.Contains(tagBodyDef)) {
                    strnewLine = strnewLine.Replace(tagBodyDef, tagBodyBlack);
                }
                if (line.Contains(tagTitleDef)) {
                    strnewLine = strnewLine.Replace(tagTitleDef, tagTitleBlack);
                }
                if (line.Contains(tagDivDef)) {
                    strnewLine = strnewLine.Replace(tagDivDef, tagDivStyle);
                }
                if (line.Contains(tagWhite1)) {
                    strnewLine = strnewLine.Replace(tagWhite1, tagBlack1);
                }
                if (line.Contains(tagWhite2)) {
                    strnewLine = strnewLine.Replace(tagWhite2, tagBlack2);
                }
                if (line.Contains(tagWhite3)) {
                    strnewLine = strnewLine.Replace(tagWhite3, tagBlack3);
                }
                if (line.Contains(tagWhite4)) {
                    strnewLine = strnewLine.Replace(tagWhite4, tagBlack4);
                }
                if (line.Contains(tagWhite5)) {
                    strnewLine = strnewLine.Replace(tagWhite5, tagBlack5);
                }
                writer.WriteLine(strnewLine);
            }
            writer.Flush();
            writer.Close();
            reader.Close();
        }

        private void genderDayFile(string filePath, string suffix, string fileOutPath) {
            string strContent = string.Empty;
            String line = string.Empty;
            if (!System.IO.File.Exists(filePath)) {
                return;
            }

            StreamReader reader = new StreamReader(filePath);
            StreamWriter writer = new StreamWriter(fileOutPath);

            while ((line = reader.ReadLine()) != null) {
                string strnewLine = line;
                if (line.Contains(tagHtmlDef)) {
                    strnewLine = strnewLine.Replace(tagHtmlDef, tagHtmlWhite);
                }
                if (line.Contains(tagBodyDef)) {
                    strnewLine = strnewLine.Replace(tagBodyDef, tagBodyWhite);
                }
                if (line.Contains(tagTitleDef)) {
                    strnewLine = strnewLine.Replace(tagTitleDef, tagTitleWhite);
                }
                if (line.Contains(tagDivDef)) {
                    strnewLine = strnewLine.Replace(tagDivDef, tagDivStyle);
                }
                writer.WriteLine(strnewLine);
            }
            writer.Flush();
            writer.Close();
            reader.Close();
        }

        private void gd_transfer_DragLeave(object sender, DragEventArgs e) {
            tbk_status.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 21, 133, 21)); //绿色
            tbk_status.Text = "将文件拖到此处进行转换";
        }

        private void gd_transfer_DragOver(object sender, DragEventArgs e) {
            tbk_status.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 21, 133, 21)); //绿色
            tbk_status.Text = "松开立即转换";
        }

        private void gd_transfer_DragEnter(object sender, DragEventArgs e) {
        }

        private void btn_select_out_path_Click(object sender, RoutedEventArgs e) {
            string path = SelectPath();
            if (string.Empty != path) {
                tb_out_path.Text = path;
                SaveOutPath(tb_out_path.Text);
            }
        }

        private string SelectPath() //弹出一个选择目录的对话框
        {
            string path = string.Empty;
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                path = dialog.SelectedPath;
            }
            return path;
        }

        private void rb_changed(object sender, RoutedEventArgs e)
        {
            //SaveRadioButtonStatus(sender);
        }

        private void SaveRadioButtonStatus(object sender)
        {
            RadioButton rb = (sender as RadioButton);
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfg.AppSettings.Settings[rb.Name].Value = rb.IsChecked.ToString();
            cfg.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
