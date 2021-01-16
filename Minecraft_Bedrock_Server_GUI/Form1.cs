using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        Process UpdaterProcess;
        StreamWriter ServerStreamWriter;
        Thread StartUpThread;
        Thread ChartWriteThread;
        Thread UpdaterThread;
        delegate void Delegate_string(string vs);
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

        public static bool infomationWriteFlag = true;
        public static bool chartThreadFlag = true;
        public static bool StartupFlag = false;
        public static bool UpdateFlag = false;

        readonly public static string ServerAppricationName = "bedrock_server";
        readonly public static string UpdaterAppricationName = "ServerUpdater";
        readonly public static string ServerFileName = Path.Combine(Application.StartupPath, ServerAppricationName + ".exe");
        readonly public static string UpdaterFileName = Path.Combine(Application.StartupPath, UpdaterAppricationName + ".exe");
        readonly public static string FindGlovalIP_Link = "http://httpbin.org/ip";
        readonly public static string PlayerAriaName = "Player";
        readonly public static string MemoryAriaName = "MemoryOccupancy";
        readonly public static string[] ShowInfomation = 
            { "Version", "Session ID", "Level Name", "Game mode", "Difficulty", "IPv4 supported", "IPv6 supported" };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UI_Enabled();
            if (Process.GetProcessesByName(ServerAppricationName).Length > 0)
            {
                MessageBox.Show("The server is already running.\nClose the your running server.", "warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                if (File.Exists(ServerFileName))
                {
                    StartUpThread = new Thread(new ThreadStart(() =>
                    {
                        try
                        {
                            WebClient GetGlovalIPWC = new WebClient();
                            string text1 = GetGlovalIPWC.DownloadString(FindGlovalIP_Link);
                            int FirstHit_GIP = text1.IndexOf(":") + 3;
                            Invoke(new Delegate_string((ip) => GlovalIPTextBox.Text = ip), 
                                text1.Substring(FirstHit_GIP, text1.IndexOf("\"", FirstHit_GIP) - FirstHit_GIP));

                            Invoke(new Delegate_bool(UI_Enabled), false);
                        }
                        catch (WebException)
                        {
                            MessageBox.Show("Connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Invoke(new Delegate_NoArguments(() => this.Close()));
                        }
                    }));
                    StartUpThread.Start();

                    Action<Axis> setAxis = (axisInfo) =>
                    {
                        axisInfo.LabelAutoFitMaxFontSize = 8;
                        axisInfo.LabelStyle.ForeColor = Color.Black;
                        axisInfo.MajorGrid.Enabled = true;
                        axisInfo.MajorGrid.LineColor = ColorTranslator.FromHtml("#C0C0C0");
                        axisInfo.MinorGrid.Enabled = true;
                        axisInfo.MinorGrid.LineColor = ColorTranslator.FromHtml("#C0C0C0");
                    };

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
                }
                else
                {
                    MessageBox.Show("Server application is not found.\nPut the server application in the same directory.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
        private void Form_Closing_evemt(object sender, FormClosingEventArgs e)
        {
            if (StartupFlag)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    MessageBox.Show("The server is running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
            if (UpdateFlag)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    MessageBox.Show("The updater is running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }
        private void ServerStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infomationWriteFlag = true;
            chartThreadFlag = true;
            StartupFlag = true;
            UI_Enabled(true);

            MemoryBuffer = new List<float>(GRAPH_MAX_SIZE_X);
            PlayerBuffer = new List<int>(GRAPH_MAX_SIZE_X);
            Player = new List<string>();

            for (int i = 0; i < GRAPH_MAX_SIZE_X; i++)
            {
                MemoryBuffer.Add(0);
                PlayerBuffer.Add(0);
            }

            ServerProcess = new Process();
            ServerProcess.StartInfo.FileName = ServerFileName;
            ServerProcess.StartInfo.CreateNoWindow = true;
            ServerProcess.StartInfo.UseShellExecute = false;
            ServerProcess.StartInfo.RedirectStandardInput = true;
            ServerProcess.StartInfo.RedirectStandardOutput = true;
            ServerProcess.StartInfo.RedirectStandardError = true;
            ServerProcess.EnableRaisingEvents = true;
            ServerProcess.Exited += new EventHandler((EHsender , EHe) => 
            {
                Invoke(new Delegate_string(Invoke_WriteConsole), "The server has closed.\n");
                Invoke(new Delegate_bool(UI_Enabled), false);
                Invoke(new Delegate_NoArguments(() =>
                {
                    PlayerSeries.Points.Clear();
                    MemorySeries.Points.Clear();
                    InfomationRichtextBox.Text = string.Empty;
                    PlayerListRichtextBox.Text = string.Empty;
                }));
                chartThreadFlag = false;
                StartupFlag = false;
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
                        }),
                            (float)ServerProcess.WorkingSet64 / 1000, Player.Count());
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
                if (File.Exists(UpdaterFileName)) 
                {
                    UI_Enabled();
                    UpdateFlag = true;
                    UpdaterProcess = new Process();
                    UpdaterProcess.StartInfo.FileName = UpdaterFileName;
                    UpdaterProcess.StartInfo.Verb = "runas";
                    //UpdaterProcess.StartInfo.Arguments = "2010/10/10 00:00:00";
                    UpdaterProcess.StartInfo.ErrorDialog = true;
                    UpdaterProcess.StartInfo.ErrorDialogParentHandle = this.Handle;
                    UpdaterProcess.EnableRaisingEvents = true;
                    UpdaterProcess.Exited += new EventHandler((EHsender, EHe) =>
                    {
                        Invoke(new Delegate_string(Invoke_WriteConsole), "The updater has closed.\n");
                        Invoke(new Delegate_bool(UI_Enabled), false);
                        UpdateFlag = false;
                    });
                    UpdaterThread = new Thread(new ThreadStart(() =>
                    {
                        UpdaterProcess.Start();
                        UpdaterProcess.WaitForExit();
                    }));
                    try
                    {
                        UpdaterProcess.Start();
                    }
                    catch (Win32Exception)
                    {
                        ServerAppConsoleRichtextBox.Text += "The updater has closed.\n";
                        UI_Enabled(false);
                        UpdateFlag = false;
                    }
                }
                else
                {
                    MessageBox.Show("The updater application cannot be found. Please reinstall", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                Invoke(new Delegate_string(Invoke_WriteConsole), outLine.Data + "\n");
            }
        }

        private void Invoke_WriteConsole(string vs)
        {
            if (!String.IsNullOrEmpty(vs))
            {
                if (vs.Contains("Server started"))
                {
                    InfomationRichtextBox.Text = writeInfomationBuffer.TrimEnd('\n');
                    writeInfomationBuffer = string.Empty;
                    infomationWriteFlag = false;
                } 
                if (infomationWriteFlag) {
                    foreach (string data in ShowInfomation)
                    {
                        if (vs.Contains(data))
                        {
                            writeInfomationBuffer += RemoveLogTime(vs);
                        }
                    }
                }
                if (vs.Contains("Player") && vs.Contains("connected"))
                {
                    string playerBuffer = RemoveLogTime(vs);
                    if (playerBuffer.Contains("Player connected"))
                    {
                        Player.Add(playerBuffer.Remove(0, 18));
                    }
                    if (playerBuffer.Contains("Player disconnected"))
                    {
                        Player.Remove(playerBuffer.Remove(0, 21));
                    }
                    PlayerListRichtextBox.Text = string.Empty;
                    foreach (string i in Player)
                    {
                        PlayerListRichtextBox.Text += i + "\n";
                    }
                }
                ServerAppConsoleRichtextBox.Text += vs;
                ServerAppConsoleRichtextBox.SelectionStart = ServerAppConsoleRichtextBox.MaxLength;
                ServerAppConsoleRichtextBox.ScrollToCaret();
            }
        }
        public string RemoveLogTime(string vs)
        {
            return vs.Remove(vs.IndexOf("["), vs.IndexOf("]") + 2);
        }

        public void UI_Enabled(bool i)
        {
            serverStartToolStripMenuItem.Enabled = !i;
            serverCloseToolStripMenuItem.Enabled = i;
            serverUpdateToolStripMenuItem.Enabled = !i;
        }
        public void UI_Enabled()
        {
            serverStartToolStripMenuItem.Enabled = false;
            serverCloseToolStripMenuItem.Enabled = false;
            serverUpdateToolStripMenuItem.Enabled = false;
        }
    }
}