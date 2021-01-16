
namespace ServerUpdater
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StateLabel1_Label = new System.Windows.Forms.Label();
            this.StateLabel2_Label = new System.Windows.Forms.Label();
            this.StateLabel3_Label = new System.Windows.Forms.Label();
            this.StateLabel4_Label = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.StateLabel1 = new System.Windows.Forms.Label();
            this.StateLabel2 = new System.Windows.Forms.Label();
            this.StateLabel3 = new System.Windows.Forms.Label();
            this.StateLabel4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Updating the server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "State";
            // 
            // StateLabel1_Label
            // 
            this.StateLabel1_Label.AutoSize = true;
            this.StateLabel1_Label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel1_Label.Location = new System.Drawing.Point(68, 118);
            this.StateLabel1_Label.Name = "StateLabel1_Label";
            this.StateLabel1_Label.Size = new System.Drawing.Size(126, 19);
            this.StateLabel1_Label.TabIndex = 2;
            this.StateLabel1_Label.Text = "File download";
            // 
            // StateLabel2_Label
            // 
            this.StateLabel2_Label.AutoSize = true;
            this.StateLabel2_Label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel2_Label.Location = new System.Drawing.Point(68, 137);
            this.StateLabel2_Label.Name = "StateLabel2_Label";
            this.StateLabel2_Label.Size = new System.Drawing.Size(126, 19);
            this.StateLabel2_Label.TabIndex = 3;
            this.StateLabel2_Label.Text = "Extract files";
            // 
            // StateLabel3_Label
            // 
            this.StateLabel3_Label.AutoSize = true;
            this.StateLabel3_Label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel3_Label.Location = new System.Drawing.Point(68, 156);
            this.StateLabel3_Label.Name = "StateLabel3_Label";
            this.StateLabel3_Label.Size = new System.Drawing.Size(117, 19);
            this.StateLabel3_Label.TabIndex = 4;
            this.StateLabel3_Label.Text = "Installation";
            // 
            // StateLabel4_Label
            // 
            this.StateLabel4_Label.AutoSize = true;
            this.StateLabel4_Label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel4_Label.Location = new System.Drawing.Point(68, 175);
            this.StateLabel4_Label.Name = "StateLabel4_Label";
            this.StateLabel4_Label.Size = new System.Drawing.Size(135, 19);
            this.StateLabel4_Label.TabIndex = 5;
            this.StateLabel4_Label.Text = "End processing";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 229);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(400, 23);
            this.ProgressBar.TabIndex = 6;
            // 
            // StateLabel1
            // 
            this.StateLabel1.AutoSize = true;
            this.StateLabel1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel1.Location = new System.Drawing.Point(44, 118);
            this.StateLabel1.Name = "StateLabel1";
            this.StateLabel1.Size = new System.Drawing.Size(0, 19);
            this.StateLabel1.TabIndex = 7;
            // 
            // StateLabel2
            // 
            this.StateLabel2.AutoSize = true;
            this.StateLabel2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel2.Location = new System.Drawing.Point(44, 137);
            this.StateLabel2.Name = "StateLabel2";
            this.StateLabel2.Size = new System.Drawing.Size(0, 19);
            this.StateLabel2.TabIndex = 8;
            // 
            // StateLabel3
            // 
            this.StateLabel3.AutoSize = true;
            this.StateLabel3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel3.Location = new System.Drawing.Point(44, 156);
            this.StateLabel3.Name = "StateLabel3";
            this.StateLabel3.Size = new System.Drawing.Size(0, 19);
            this.StateLabel3.TabIndex = 9;
            // 
            // StateLabel4
            // 
            this.StateLabel4.AutoSize = true;
            this.StateLabel4.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel4.Location = new System.Drawing.Point(44, 175);
            this.StateLabel4.Name = "StateLabel4";
            this.StateLabel4.Size = new System.Drawing.Size(0, 19);
            this.StateLabel4.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(424, 264);
            this.Controls.Add(this.StateLabel4);
            this.Controls.Add(this.StateLabel3);
            this.Controls.Add(this.StateLabel2);
            this.Controls.Add(this.StateLabel1);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.StateLabel4_Label);
            this.Controls.Add(this.StateLabel3_Label);
            this.Controls.Add(this.StateLabel2_Label);
            this.Controls.Add(this.StateLabel1_Label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Updater";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label StateLabel1_Label;
        private System.Windows.Forms.Label StateLabel2_Label;
        private System.Windows.Forms.Label StateLabel3_Label;
        private System.Windows.Forms.Label StateLabel4_Label;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label StateLabel1;
        private System.Windows.Forms.Label StateLabel2;
        private System.Windows.Forms.Label StateLabel3;
        private System.Windows.Forms.Label StateLabel4;
    }
}

