namespace game_Caro_deadline_31
{
    partial class room
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
            this.btn_QuickJoin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_PlayerOnline = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // tb_PlayerName
            // 
            this.tb_PlayerName.Enabled = false;
            this.tb_PlayerName.Location = new System.Drawing.Point(641, 59);
            this.tb_PlayerName.Multiline = true;
            this.tb_PlayerName.Name = "tb_PlayerName";
            this.tb_PlayerName.Size = new System.Drawing.Size(118, 32);
            this.tb_PlayerName.TabIndex = 0;
            this.tb_PlayerName.TextChanged += new System.EventHandler(this.tb_PlayerName_TextChanged);
            // 
            // btn_QuickJoin
            // 
            this.btn_QuickJoin.Location = new System.Drawing.Point(633, 382);
            this.btn_QuickJoin.Name = "btn_QuickJoin";
            this.btn_QuickJoin.Size = new System.Drawing.Size(126, 33);
            this.btn_QuickJoin.TabIndex = 2;
            this.btn_QuickJoin.Text = "Tham gia nhanh";
            this.btn_QuickJoin.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(658, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Xin chào";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Danh sách người chơi online";
            // 
            // btn_PlayerOnline
            // 
            this.btn_PlayerOnline.Location = new System.Drawing.Point(633, 340);
            this.btn_PlayerOnline.Name = "btn_PlayerOnline";
            this.btn_PlayerOnline.Size = new System.Drawing.Size(126, 36);
            this.btn_PlayerOnline.TabIndex = 8;
            this.btn_PlayerOnline.Text = "Người chơi đang online";
            this.btn_PlayerOnline.UseVisualStyleBackColor = true;
            this.btn_PlayerOnline.Click += new System.EventHandler(this.btn_PlayerOnline_Click);
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.Location = new System.Drawing.Point(12, 88);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(157, 250);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btn_PlayerOnline);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_QuickJoin);
            this.Controls.Add(this.tb_PlayerName);
            this.Name = "room";
            this.Text = "room";
            this.Load += new System.EventHandler(this.room_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_PlayerName;
        private System.Windows.Forms.Button btn_QuickJoin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_PlayerOnline;
        private System.Windows.Forms.ListView listView1;
    }
}