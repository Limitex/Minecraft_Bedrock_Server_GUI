using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Minecraft_Bedrock_Server_GUI
{
    public partial class Form1 : Form
    {
        Process ServerProcess;
        StreamWriter ServerStreamWriter;
        Thread ChartWriteThread;
        delegate void Delegate_string(string vs);
        delegate void Delegate_int(int i);
        delegate void Delegate_bool(bool d);
        delegate void Delegate_GraphData(float Bytes, int Player);
        delegate void Delegate_NoArguments();

        string writeInfomationBuffer = string.Empty;
        List<string> Player;

        const int GRAPH_MAX_SIZE_X = 300;
        int PlayerGraphMaxSize_Y = 200;
        int MemoryGraphMaxSize_Y = 200;

        ChartArea PlayerArea;
        ChartArea MemoryArea;
        Series PlayerSeries;
        Series MemorySeries;

        List<float> MemoryBuffer;
        List<int> PlayerBuffer;

        public static Uri ServerDownloadLink;

        public static bool infomationWriteFlag = true;
        public static bool chartThreadFlag = true;
        public static bool RunningFlag = false;
        public static bool UpdateFlag = false;
        public static bool InstallingFlag = false;

        readonly public static string ServerAppricationName = "bedrock_server";
        readonly public static string ServerAppricationPath = Path.Combine(Application.StartupPath, ServerAppricationName + ".exe");
        readonly public static string DownloadFolderPath = Path.Combine(Application.StartupPath, "buffer_" + new Random().Next(100000000, 1000000000));
        readonly public static string DownloadZipPath = Path.Combine(DownloadFolderPath, "bedrock_server.zip");
        readonly public static string FindGlovalIP_Link = "http://httpbin.org/ip";
        readonly public static string ServerDownloadSite_Link = "https://www.minecraft.net/en-us/download/server/bedrock";
        readonly public static string ServerDownloadSearch_Link = "https://minecraft.azureedge.net/bin-win/";
        readonly public static string WorldDirectoryName = Path.Combine(Application.StartupPath, "worlds");
        readonly public static string PlayerAriaName = "Player";
        readonly public static string MemoryAriaName = "MemoryOccupancy";
        readonly public static string[] ShowInfomation = 
            { "Version", "Session ID", "Level Name", "Game mode", "Difficulty", "IPv4 supported", "IPv6 supported" };
        readonly static string[] NewFileDaletes =
        {
            Path.Combine(DownloadFolderPath, @"permissions.json"),
            Path.Combine(DownloadFolderPath, @"server.properties"),
            Path.Combine(DownloadFolderPath, @"whitelist.json"),
            Path.Combine(DownloadFolderPath, @"bedrock_server_how_to.html"),
            Path.Combine(DownloadFolderPath, @"release-notes.txt")
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName(ServerAppricationName).Length > 0)
            {
                MessageBox.Show("The server is already running.\nClose the your running server.", "warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                Action<Axis> setAxis = (axisInfo) =>
                {
                    axisInfo.LabelAutoFitMaxFontSize = 8;
                    axisInfo.LabelStyle.ForeColor = Color.Black;
                    axisInfo.MajorGrid.Enabled = true;
                    axisInfo.MajorGrid.LineColor = ColorTranslator.FromHtml("#C0C0C0");
                    axisInfo.MinorGrid.Enabled = true;
                    axisInfo.MinorGrid.LineColor = ColorTranslator.FromHtml("#C0C0C0");
                };
                {
                    MainChart.Series.Clear();
                    MainChart.ChartAreas.Clear();
                    MainChart.Titles.Clear();
                    MainChart.Legends.Clear();

                    PlayerArea = new ChartArea(PlayerAriaName);
                    PlayerArea.AxisX.Title = "Seconds [s]";
                    PlayerArea.AxisY.Title = "Player [p]";
                    PlayerArea.AxisX.Minimum = 0;
                    PlayerArea.AxisX.Maximum = GRAPH_MAX_SIZE_X;
                    PlayerArea.AxisY.Maximum = PlayerGraphMaxSize_Y;
                    setAxis(PlayerArea.AxisX);
                    setAxis(PlayerArea.AxisY);

                    MemoryArea = new ChartArea(MemoryAriaName);
                    MemoryArea.AxisX.Title = "Seconds [s]";
                    MemoryArea.AxisY.Title = "Capacity [MB]";
                    MemoryArea.AxisX.Minimum = 0;
                    MemoryArea.AxisX.Maximum = GRAPH_MAX_SIZE_X;
                    MemoryArea.AxisY.Maximum = MemoryGraphMaxSize_Y;
                    setAxis(MemoryArea.AxisX);
                    setAxis(MemoryArea.AxisY);

                    PlayerSeries = new Series();
                    PlayerSeries.LegendText = "Player [person]";
                    PlayerSeries.ChartType = SeriesChartType.Area;
                    PlayerSeries.Color = ColorTranslator.FromHtml("#3333ff");
                    PlayerSeries.ChartArea = PlayerAriaName;

                    MemorySeries = new Series();
                    MemorySeries.LegendText = "Used Memory [MB]";
                    MemorySeries.ChartType = SeriesChartType.Area;
                    MemorySeries.Color = ColorTranslator.FromHtml("#FFA500");
                    MemorySeries.ChartArea = MemoryAriaName;

                    MainChart.ChartAreas.Add(PlayerArea);
                    MainChart.ChartAreas.Add(MemoryArea);
                    MainChart.Series.Add(PlayerSeries);
                    MainChart.Series.Add(MemorySeries);
                }//Chart setting

                UI_Enabled(0);

                FindGlovalIP();

                if (!File.Exists(ServerAppricationPath))
                {
                    DialogResult dr = MessageBox.Show("Server application is not found.\nWould you like to download a new server?",
                        "Infomation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        UI_Enabled();
                        InstallingFlag = true;
                        Thread thread = new Thread(new ThreadStart(() => 
                        {
                            ServerInstaller();
                            Invoke(new Delegate_int(UI_Enabled), 0);
                            InstallingFlag = false;
                        }));
                        thread.Start();
                    }
                    if (dr == DialogResult.No) 
                    {
                        this.Close();
                    }
                }
            }
        }
        private void Form_Closing_evemt(object sender, FormClosingEventArgs e)
        {
            if (RunningFlag || UpdateFlag || InstallingFlag)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    string str = string.Empty;
                    if (RunningFlag)
                        str = "server";
                    if (UpdateFlag)
                        str = "updater";
                    if (InstallingFlag)
                        str = "installer";
                    MessageBox.Show("The " + str + " is running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }
        private void ServerStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infomationWriteFlag = true;
            chartThreadFlag = true;
            RunningFlag = true;
            UI_Enabled(1);

            FindGlovalIP();

            MemoryBuffer = new List<float>(GRAPH_MAX_SIZE_X);
            PlayerBuffer = new List<int>(GRAPH_MAX_SIZE_X);
            Player = new List<string>();

            for (int i = 0; i < GRAPH_MAX_SIZE_X; i++)
            {
                MemoryBuffer.Add(0);
                PlayerBuffer.Add(0);
            }

            ServerProcess = new Process();
            ServerProcess.StartInfo.FileName = ServerAppricationPath;
            ServerProcess.StartInfo.CreateNoWindow = true;
            ServerProcess.StartInfo.UseShellExecute = false;
            ServerProcess.StartInfo.RedirectStandardInput = true;
            ServerProcess.StartInfo.RedirectStandardOutput = true;
            ServerProcess.StartInfo.RedirectStandardError = true;
            ServerProcess.EnableRaisingEvents = true;
            ServerProcess.Exited += new EventHandler((EHsender , EHe) => 
            {
                Invoke(new Delegate_string(Invoke_WriteConsole), "The server has closed.\n");
                Invoke(new Delegate_int(UI_Enabled), 0);
                Invoke(new Delegate_NoArguments(() =>
                {
                    PlayerSeries.Points.Clear();
                    MemorySeries.Points.Clear();
                    InfomationRichtextBox.Text = string.Empty;
                    PlayerListRichtextBox.Text = string.Empty;
                }));
                chartThreadFlag = false;
                RunningFlag = false;
            });
            ServerProcess.ErrorDataReceived += new DataReceivedEventHandler(EventHandler_ConsoleOutputHandler);
            ServerProcess.OutputDataReceived += new DataReceivedEventHandler(EventHandler_ConsoleOutputHandler);
            ServerProcess.Start();
            ServerStreamWriter = ServerProcess.StandardInput;
            ServerProcess.BeginOutputReadLine();
            ServerProcess.BeginErrorReadLine();

            ChartWriteThread = new Thread(new ThreadStart(() => 
            {
                while (chartThreadFlag)
                {
                    ServerProcess.Refresh();
                    try
                    {
                        Invoke(new Delegate_GraphData((bytes, player) =>
                        {
                            PlayerBuffer.RemoveAt(PlayerBuffer.Count - 1);
                            PlayerBuffer.Insert(0, player);
                            MemoryBuffer.RemoveAt(MemoryBuffer.Count - 1);
                            MemoryBuffer.Insert(0, bytes);

                            int playerValMax = 0;
                            int MemoryValMax = 0;
                            foreach (int i in PlayerBuffer)
                            {
                                playerValMax = playerValMax < i ? i : playerValMax;
                            }
                            foreach (int i in MemoryBuffer)
                            {
                                MemoryValMax = MemoryValMax < i ? i : MemoryValMax;
                            }
                            PlayerGraphMaxSize_Y = playerValMax + 5;
                            MemoryGraphMaxSize_Y = MemoryValMax + MemoryGraphMaxSize_Y / 3;

                            PlayerArea.AxisY.Maximum = PlayerGraphMaxSize_Y;
                            MemoryArea.AxisY.Maximum = MemoryGraphMaxSize_Y / 1000;

                            PlayerSeries.Points.Clear();
                            MemorySeries.Points.Clear();

                            for (int i = GRAPH_MAX_SIZE_X - 1; i >= 0; i--)
                            {
                                PlayerSeries.Points.Add(new DataPoint(i, PlayerBuffer[i]));
                                MemorySeries.Points.Add(new DataPoint(i, MemoryBuffer[i] / 1000f));
                            }
                        }), (float)ServerProcess.WorkingSet64 / 1000, Player.Count());
                    }
                    catch (InvalidOperationException)
                    {
                        break;
                    }

                    Thread.Sleep(1000);
                }
            }));
            ChartWriteThread.Start();
        }
        private void openDirectoryInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath);
        }
        private void ServerCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Player.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Player is in the server now.\nDo you close it ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    ServerStreamWriter.WriteLine("stop");
                }
            }
            else
            {
                ServerStreamWriter.WriteLine("stop");
            }
        }
        private void serverUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to perform the update?", "Update", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                UI_Enabled();
                UpdateFlag = true;
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    ServerInstaller("update");
                    Invoke(new Delegate_int(UI_Enabled), 0);
                    UpdateFlag = false;

                }));
                thread.Start();
            }
        }

        private void ConsoleInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ServerStreamWriter.WriteLine(ConsoleInputTextBox.Text);
                ConsoleInputTextBox.Text = string.Empty;
            }
        }
        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GlovalIPTextBox.Text);
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerAppConsoleRichtextBox.Text = string.Empty;
        }

        private void EventHandler_ConsoleOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                string vs = outLine.Data + "\n";

                if (vs.Contains("[") && vs.Contains("]"))
                {
                    vs = vs.Remove(vs.IndexOf("["), vs.IndexOf("]") + 2);
                }

                if (vs.Contains("Server started"))
                {
                    Invoke(new Delegate_NoArguments(() => 
                        InfomationRichtextBox.Text = writeInfomationBuffer.TrimEnd('\n')));
                    writeInfomationBuffer = string.Empty;
                    infomationWriteFlag = false;
                }
                if (infomationWriteFlag)
                {
                    foreach (string data in ShowInfomation)
                    {
                        if (vs.Contains(data))
                        {
                            writeInfomationBuffer += vs;
                        }
                    }
                }
                if (vs.Contains("Player") && vs.Contains("connected"))
                {
                    string playerBuffer = vs;
                    if (playerBuffer.Contains("Player connected"))
                    {
                        Player.Add(playerBuffer.Remove(0, 18));
                    }
                    if (playerBuffer.Contains("Player disconnected"))
                    {
                        Player.Remove(playerBuffer.Remove(0, 21));
                    }
                    Invoke(new Delegate_NoArguments(() => 
                    {
                        PlayerListRichtextBox.Text = string.Empty;
                        foreach (string i in Player)
                        {
                            PlayerListRichtextBox.Text += i + "\n";
                        }
                    }));
                }

                Invoke(new Delegate_string(Invoke_WriteConsole), vs);
            }
        }

        private void FindGlovalIP()
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    WebClient GetGlovalIPWC = new WebClient();
                    string text1 = GetGlovalIPWC.DownloadString(FindGlovalIP_Link);
                    int FirstHit_GIP = text1.IndexOf(":") + 3;
                    Invoke(new Delegate_NoArguments(() => GlovalIPTextBox.Text =
                        text1.Substring(FirstHit_GIP, text1.IndexOf("\"", FirstHit_GIP) - FirstHit_GIP)));
                }
                catch (WebException)
                {
                    MessageBox.Show("Connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Invoke(new Delegate_NoArguments(() => this.Close()));
                }
            }));
            thread.Start();
        }

        private void Invoke_WriteConsole(string vs)
        {
            if (!String.IsNullOrEmpty(vs))
            {
                ServerAppConsoleRichtextBox.Text += "[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "] " + vs;
                ServerAppConsoleRichtextBox.SelectionStart = ServerAppConsoleRichtextBox.MaxLength;
                ServerAppConsoleRichtextBox.ScrollToCaret();
            }
        }

        public void UI_Enabled(int i = -1)
        {
            serverStartToolStripMenuItem.Enabled = i >= 0 && i == 0;
            serverCloseToolStripMenuItem.Enabled = i >= 0 && i != 0;
            serverUpdateToolStripMenuItem.Enabled = i >= 0 && i == 0;
            clearToolStripMenuItem.Enabled = i >= 0;
            ConsoleInputTextBox.Enabled = i >= 0 && i != 0;
        }

        public void ServerInstaller(string str = "")
        {
            Invoke(new Delegate_string(Invoke_WriteConsole), "Downloading a new server app...\n");

            WebClient wc = new WebClient();
            try
            {
                string SiteDownloadString = wc.DownloadString(ServerDownloadSite_Link);
                int FirstHit = SiteDownloadString.IndexOf(ServerDownloadSearch_Link);
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

            if (Directory.Exists(DownloadFolderPath))
                DeleteDirectory(DownloadFolderPath);
            Directory.CreateDirectory(DownloadFolderPath);
            using (FileStream sr = new FileStream(DownloadZipPath, FileMode.Create)) { }
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(ServerDownloadLink, DownloadZipPath);

            Invoke(new Delegate_string(Invoke_WriteConsole), "Extract the downloaded ZIP file...\n");

            ZipFile.ExtractToDirectory(DownloadZipPath, DownloadFolderPath);
            File.Delete(DownloadZipPath);
            if (str == "update")
            {
                foreach (string sr in NewFileDaletes)
                {
                    File.Delete(sr);
                }
            }

            Invoke(new Delegate_string(Invoke_WriteConsole), "Installing...\n");

            DirectoryCopy(DownloadFolderPath, Application.StartupPath);
            DeleteDirectory(DownloadFolderPath);

            Invoke(new Delegate_string(Invoke_WriteConsole), "Done.\n");
        }
        
        public static void DeleteDirectory(string dir)
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            //ReadonlyAttribute(di, FileAttributes.ReadOnly, FileAttributes.Normal);
            di.Delete(true);
        }

        public static void DirectoryCopy(string sourcePath, string destinationPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
            DirectoryInfo destinationDirectory = new DirectoryInfo(destinationPath);
            if (destinationDirectory.Exists == false)
            {
                destinationDirectory.Create();
                //destinationDirectory.Attributes = sourceDirectory.Attributes;
            }

            foreach (FileInfo fileInfo in sourceDirectory.GetFiles())
                fileInfo.CopyTo(destinationDirectory.FullName + @"\" + fileInfo.Name, true);
            foreach (DirectoryInfo directoryInfo in sourceDirectory.GetDirectories()) 
            {
                string destinationDirec = destinationDirectory.FullName + @"\" + directoryInfo.Name;
                if (destinationDirec != WorldDirectoryName)
                {
                    DirectoryCopy(directoryInfo.FullName, destinationDirec);
                }
            }
        }

        //public static void ReadonlyAttribute(DirectoryInfo dirInfo, FileAttributes OldFA, FileAttributes NewFA)
        //{
        //    if ((dirInfo.Attributes & OldFA) == OldFA)
        //        dirInfo.Attributes = NewFA;
        //    foreach (FileInfo fi in dirInfo.GetFiles())
        //        if ((fi.Attributes & OldFA) == OldFA)
        //            fi.Attributes = NewFA;
        //    foreach (DirectoryInfo di in dirInfo.GetDirectories())
        //        ReadonlyAttribute(di, OldFA, NewFA);
        //}
    }
}