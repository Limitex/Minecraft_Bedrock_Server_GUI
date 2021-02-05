
namespace Minecraft_Bedrock_Server_GUI
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ConsoleGroupBox = new System.Windows.Forms.GroupBox();
            this.ConsoleInputTextBox = new System.Windows.Forms.TextBox();
            this.ServerAppConsoleRichtextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServerStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.MainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.InfomationGroupBox = new System.Windows.Forms.GroupBox();
            this.PlayerListLabel = new System.Windows.Forms.Label();
            this.GlovalIPLabel = new System.Windows.Forms.Label();
            this.ServerInfoLabel = new System.Windows.Forms.Label();
            this.CopyButton = new System.Windows.Forms.Button();
            this.GlovalIPTextBox = new System.Windows.Forms.TextBox();
            this.PlayerListRichtextBox = new System.Windows.Forms.RichTextBox();
            this.InfomationRichtextBox = new System.Windows.Forms.RichTextBox();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConsoleGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ServerStatusGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).BeginInit();
            this.InfomationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConsoleGroupBox
            // 
            this.ConsoleGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ConsoleGroupBox.Controls.Add(this.ConsoleInputTextBox);
            this.ConsoleGroupBox.Controls.Add(this.ServerAppConsoleRichtextBox);
            this.ConsoleGroupBox.Location = new System.Drawing.Point(12, 27);
            this.ConsoleGroupBox.Name = "ConsoleGroupBox";
            this.ConsoleGroupBox.Size = new System.Drawing.Size(412, 522);
            this.ConsoleGroupBox.TabIndex = 0;
            this.ConsoleGroupBox.TabStop = false;
            this.ConsoleGroupBox.Text = "Server Console";
            // 
            // ConsoleInputTextBox
            // 
            this.ConsoleInputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConsoleInputTextBox.Location = new System.Drawing.Point(6, 497);
            this.ConsoleInputTextBox.Name = "ConsoleInputTextBox";
            this.ConsoleInputTextBox.Size = new System.Drawing.Size(400, 19);
            this.ConsoleInputTextBox.TabIndex = 1;
            this.ConsoleInputTextBox.WordWrap = false;
            this.ConsoleInputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsoleInputTextBox_KeyDown);
            // 
            // ServerAppConsoleRichtextBox
            // 
            this.ServerAppConsoleRichtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ServerAppConsoleRichtextBox.BackColor = System.Drawing.Color.White;
            this.ServerAppConsoleRichtextBox.Location = new System.Drawing.Point(6, 18);
            this.ServerAppConsoleRichtextBox.Name = "ServerAppConsoleRichtextBox";
            this.ServerAppConsoleRichtextBox.ReadOnly = true;
            this.ServerAppConsoleRichtextBox.Size = new System.Drawing.Size(400, 473);
            this.ServerAppConsoleRichtextBox.TabIndex = 0;
            this.ServerAppConsoleRichtextBox.Text = "";
            this.ServerAppConsoleRichtextBox.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1084, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operationToolStripMenuItem,
            this.openDirectoryInExplorerToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.serverUpdateToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // operationToolStripMenuItem
            // 
            this.operationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverStartToolStripMenuItem,
            this.serverCloseToolStripMenuItem});
            this.operationToolStripMenuItem.Name = "operationToolStripMenuItem";
            this.operationToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.operationToolStripMenuItem.Text = "Operation";
            // 
            // serverStartToolStripMenuItem
            // 
            this.serverStartToolStripMenuItem.Name = "serverStartToolStripMenuItem";
            this.serverStartToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.serverStartToolStripMenuItem.Text = "Server Start";
            this.serverStartToolStripMenuItem.Click += new System.EventHandler(this.ServerStartToolStripMenuItem_Click);
            // 
            // serverCloseToolStripMenuItem
            // 
            this.serverCloseToolStripMenuItem.Name = "serverCloseToolStripMenuItem";
            this.serverCloseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.serverCloseToolStripMenuItem.Text = "Server Close";
            this.serverCloseToolStripMenuItem.Click += new System.EventHandler(this.ServerCloseToolStripMenuItem_Click);
            // 
            // openDirectoryInExplorerToolStripMenuItem
            // 
            this.openDirectoryInExplorerToolStripMenuItem.Name = "openDirectoryInExplorerToolStripMenuItem";
            this.openDirectoryInExplorerToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openDirectoryInExplorerToolStripMenuItem.Text = "Open directory in explorer";
            this.openDirectoryInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryInExplorerToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // serverUpdateToolStripMenuItem
            // 
            this.serverUpdateToolStripMenuItem.Name = "serverUpdateToolStripMenuItem";
            this.serverUpdateToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.serverUpdateToolStripMenuItem.Text = "Server update";
            this.serverUpdateToolStripMenuItem.Click += new System.EventHandler(this.serverUpdateToolStripMenuItem_Click);
            // 
            // ServerStatusGroupBox
            // 
            this.ServerStatusGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerStatusGroupBox.Controls.Add(this.MainChart);
            this.ServerStatusGroupBox.Location = new System.Drawing.Point(430, 27);
            this.ServerStatusGroupBox.Name = "ServerStatusGroupBox";
            this.ServerStatusGroupBox.Size = new System.Drawing.Size(424, 522);
            this.ServerStatusGroupBox.TabIndex = 2;
            this.ServerStatusGroupBox.TabStop = false;
            this.ServerStatusGroupBox.Text = "Server Status";
            // 
            // MainChart
            // 
            this.MainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.MainChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.MainChart.Legends.Add(legend1);
            this.MainChart.Location = new System.Drawing.Point(6, 18);
            this.MainChart.Name = "MainChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.MainChart.Series.Add(series1);
            this.MainChart.Size = new System.Drawing.Size(412, 498);
            this.MainChart.TabIndex = 0;
            this.MainChart.Text = "chart1";
            // 
            // InfomationGroupBox
            // 
            this.InfomationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfomationGroupBox.Controls.Add(this.PlayerListLabel);
            this.InfomationGroupBox.Controls.Add(this.GlovalIPLabel);
            this.InfomationGroupBox.Controls.Add(this.ServerInfoLabel);
            this.InfomationGroupBox.Controls.Add(this.CopyButton);
            this.InfomationGroupBox.Controls.Add(this.GlovalIPTextBox);
            this.InfomationGroupBox.Controls.Add(this.PlayerListRichtextBox);
            this.InfomationGroupBox.Controls.Add(this.InfomationRichtextBox);
            this.InfomationGroupBox.Location = new System.Drawing.Point(860, 27);
            this.InfomationGroupBox.Name = "InfomationGroupBox";
            this.InfomationGroupBox.Size = new System.Drawing.Size(212, 522);
            this.InfomationGroupBox.TabIndex = 3;
            this.InfomationGroupBox.TabStop = false;
            this.InfomationGroupBox.Text = "Infomation";
            // 
            // PlayerListLabel
            // 
            this.PlayerListLabel.AutoSize = true;
            this.PlayerListLabel.Location = new System.Drawing.Point(6, 240);
            this.PlayerListLabel.Name = "PlayerListLabel";
            this.PlayerListLabel.Size = new System.Drawing.Size(60, 12);
            this.PlayerListLabel.TabIndex = 6;
            this.PlayerListLabel.Text = "Player List";
            // 
            // GlovalIPLabel
            // 
            this.GlovalIPLabel.AutoSize = true;
            this.GlovalIPLabel.Location = new System.Drawing.Point(6, 203);
            this.GlovalIPLabel.Name = "GlovalIPLabel";
            this.GlovalIPLabel.Size = new System.Drawing.Size(51, 12);
            this.GlovalIPLabel.TabIndex = 5;
            this.GlovalIPLabel.Text = "Gloval IP";
            // 
            // ServerInfoLabel
            // 
            this.ServerInfoLabel.AutoSize = true;
            this.ServerInfoLabel.Location = new System.Drawing.Point(6, 15);
            this.ServerInfoLabel.Name = "ServerInfoLabel";
            this.ServerInfoLabel.Size = new System.Drawing.Size(95, 12);
            this.ServerInfoLabel.TabIndex = 4;
            this.ServerInfoLabel.Text = "Server Infomation";
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(131, 216);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 23);
            this.CopyButton.TabIndex = 3;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // GlovalIPTextBox
            // 
            this.GlovalIPTextBox.BackColor = System.Drawing.Color.White;
            this.GlovalIPTextBox.Location = new System.Drawing.Point(6, 218);
            this.GlovalIPTextBox.Name = "GlovalIPTextBox";
            this.GlovalIPTextBox.ReadOnly = true;
            this.GlovalIPTextBox.Size = new System.Drawing.Size(119, 19);
            this.GlovalIPTextBox.TabIndex = 2;
            this.GlovalIPTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GlovalIPTextBox.WordWrap = false;
            // 
            // PlayerListRichtextBox
            // 
            this.PlayerListRichtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PlayerListRichtextBox.BackColor = System.Drawing.Color.White;
            this.PlayerListRichtextBox.Location = new System.Drawing.Point(6, 255);
            this.PlayerListRichtextBox.Name = "PlayerListRichtextBox";
            this.PlayerListRichtextBox.ReadOnly = true;
            this.PlayerListRichtextBox.Size = new System.Drawing.Size(200, 261);
            this.PlayerListRichtextBox.TabIndex = 1;
            this.PlayerListRichtextBox.Text = "";
            this.PlayerListRichtextBox.WordWrap = false;
            // 
            // InfomationRichtextBox
            // 
            this.InfomationRichtextBox.BackColor = System.Drawing.Color.White;
            this.InfomationRichtextBox.Location = new System.Drawing.Point(6, 30);
            this.InfomationRichtextBox.Name = "InfomationRichtextBox";
            this.InfomationRichtextBox.ReadOnly = true;
            this.InfomationRichtextBox.Size = new System.Drawing.Size(200, 170);
            this.InfomationRichtextBox.TabIndex = 0;
            this.InfomationRichtextBox.Text = "";
            this.InfomationRichtextBox.WordWrap = false;
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.InfomationGroupBox);
            this.Controls.Add(this.ServerStatusGroupBox);
            this.Controls.Add(this.ConsoleGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1100, 600);
            this.Name = "Form1";
            this.Text = "Minecraft Bedrock Server GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing_evemt);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ConsoleGroupBox.ResumeLayout(false);
            this.ConsoleGroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ServerStatusGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).EndInit();
            this.InfomationGroupBox.ResumeLayout(false);
            this.InfomationGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ConsoleGroupBox;
        private System.Windows.Forms.TextBox ConsoleInputTextBox;
        private System.Windows.Forms.RichTextBox ServerAppConsoleRichtextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverCloseToolStripMenuItem;
        private System.Windows.Forms.GroupBox ServerStatusGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart MainChart;
        private System.Windows.Forms.GroupBox InfomationGroupBox;
        private System.Windows.Forms.Label PlayerListLabel;
        private System.Windows.Forms.Label GlovalIPLabel;
        private System.Windows.Forms.Label ServerInfoLabel;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.TextBox GlovalIPTextBox;
        private System.Windows.Forms.RichTextBox PlayerListRichtextBox;
        private System.Windows.Forms.RichTextBox InfomationRichtextBox;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
    }
}

