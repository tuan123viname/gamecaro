﻿namespace game_Caro_deadline_31
{
    partial class client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_PlayerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ConnectServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_PlayerName
            // 
            this.tb_PlayerName.Location = new System.Drawing.Point(24, 79);
            this.tb_PlayerName.Multiline = true;
            this.tb_PlayerName.Name = "tb_PlayerName";
            this.tb_PlayerName.Size = new System.Drawing.Size(169, 31);
            this.tb_PlayerName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(40, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nhập tên của bạn";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_ConnectServer
            // 
            this.btn_ConnectServer.Location = new System.Drawing.Point(40, 129);
            this.btn_ConnectServer.Name = "btn_ConnectServer";
            this.btn_ConnectServer.Size = new System.Drawing.Size(131, 47);
            this.btn_ConnectServer.TabIndex = 3;
            this.btn_ConnectServer.Text = "Kết nối đến server";
            this.btn_ConnectServer.UseVisualStyleBackColor = false;
            this.btn_ConnectServer.Click += new System.EventHandler(this.btn_ConnectServer_Click);
            // 
            // client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 215);
            this.Controls.Add(this.btn_ConnectServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_PlayerName);
            this.Name = "client";
            this.Text = "client";
            this.Load += new System.EventHandler(this.client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tb_PlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ConnectServer;
    }
}