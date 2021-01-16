using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerUpdater
{
    public partial class Form1 : Form
    {
        delegate void Delegate_NoArguments();

        readonly static string ServerDownloadSiteLink = "https://www.minecraft.net/en-us/download/server/bedrock";
        readonly static string ServerDownloadSearchLink = "https://minecraft.azureedge.net/bin-win/";
        readonly static string ApplicationPath = Application.StartupPath;

        readonly static string SaveFolderPath = Path.Combine(Application.StartupPath, "buffer");
        readonly static string SaveFilePath = Path.Combine(SaveFolderPath, "bedrock_server.zip");
        readonly static string state = ">";
        readonly static string[] NewFileDaletes =
        {
            Path.Combine(SaveFolderPath, @"permissions.json"),
            Path.Combine(SaveFolderPath, @"server.properties"),
            Path.Combine(SaveFolderPath, @"whitelist.json"),
            Path.Combine(SaveFolderPath, @"bedrock_server_how_to.html"),
            Path.Combine(SaveFolderPath, @"release-notes.txt"),
            SaveFilePath
        };
        public static Uri ServerDownloadLink;
        public static bool TaskFlug = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StateLabel1.Text = state;
            ProgressBar.Value = 20;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                TaskFlug = true;
                WebClient wc = new WebClient();
                try
                {
                    string SiteDownloadString = wc.DownloadString(ServerDownloadSiteLink);
                    int FirstHit = SiteDownloadString.IndexOf(ServerDownloadSearchLink);
                    int SecondHit = SiteDownloadString.IndexOf("\"", FirstHit);
                    string ServerDownlaodLink = SiteDownloadString.Substring(FirstHit, SecondHit - FirstHit);
                    ServerDownloadLink = new Uri(ServerDownlaodLink);
                }
                catch (WebException)
                {
                    Invoke(new Delegate_NoArguments(() =>
                    {
                        MessageBox.Show("Please connect to the internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }));
                }

                if (Directory.Exists(SaveFolderPath))
                {
                    DeleteDirectory(SaveFolderPath);
                }

                Directory.CreateDirectory(SaveFolderPath);
                using (FileStream sr = new FileStream(SaveFilePath, FileMode.Create)) { }
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(ServerDownloadLink, SaveFilePath);

                Invoke(new Delegate_NoArguments(() =>
                {
                    StateLabel1.Text = string.Empty;
                    StateLabel2.Text = state;
                    ProgressBar.Value = 60;
                }));

                ZipFile.ExtractToDirectory(SaveFilePath, SaveFolderPath);
                foreach (string str in NewFileDaletes)
                {
                    File.Delete(str);
                }

                Invoke(new Delegate_NoArguments(() =>
                {
                    StateLabel2.Text = string.Empty;
                    StateLabel3.Text = state;
                    ProgressBar.Value = 90;
                }));

                DirectoryCopy(SaveFolderPath, Application.StartupPath);
                DeleteDirectory(SaveFolderPath);

                Invoke(new Delegate_NoArguments(() =>
                {
                    StateLabel3.Text = string.Empty;
                    StateLabel4.Text = state;
                    ProgressBar.Value = 100;
                }));
                Thread.Sleep(1000); //My bad lol
                TaskFlug = false;

                Invoke(new Delegate_NoArguments(() =>
                {
                    this.Close();
                }));
            }));
            thread.Start();
        }

        private void Form_Closing_Event(object sender, FormClosingEventArgs e)
        {
            if (TaskFlug)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                }
            }
        }

        public static void DeleteDirectory(string dir)
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            RemoveReadonlyAttribute(di, FileAttributes.ReadOnly, FileAttributes.Normal);
            di.Delete(true);
        }

        public static void RemoveReadonlyAttribute(DirectoryInfo dirInfo, FileAttributes OldFA, FileAttributes NewFA)
        {
            if ((dirInfo.Attributes & OldFA) == OldFA)
                dirInfo.Attributes = NewFA;
            foreach (FileInfo fi in dirInfo.GetFiles())
                if ((fi.Attributes & OldFA) == OldFA)
                    fi.Attributes = NewFA;
            foreach (DirectoryInfo di in dirInfo.GetDirectories())
                RemoveReadonlyAttribute(di, OldFA, NewFA);
        }

        public static void DirectoryCopy(string sourcePath, string destinationPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
            DirectoryInfo destinationDirectory = new DirectoryInfo(destinationPath);
            if (destinationDirectory.Exists == false)
            {
                destinationDirectory.Create();
                destinationDirectory.Attributes = sourceDirectory.Attributes;
            }
            foreach (FileInfo fileInfo in sourceDirectory.GetFiles())
                fileInfo.CopyTo(destinationDirectory.FullName + @"\" + fileInfo.Name, true);
            foreach (DirectoryInfo directoryInfo in sourceDirectory.GetDirectories())
                DirectoryCopy(directoryInfo.FullName, destinationDirectory.FullName + @"\" + directoryInfo.Name);
        }
    }
}
