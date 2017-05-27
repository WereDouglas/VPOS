namespace VPOS
{
    partial class TransactorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fileUrlTxtBx = new System.Windows.Forms.TextBox();
            this.nationalityTxt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.addressTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.typeCbx = new System.Windows.Forms.ComboBox();
            this.surnameTxt = new System.Windows.Forms.TextBox();
            this.contactTxt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.imgCapture = new System.Windows.Forms.PictureBox();
            this.bntStart = new System.Windows.Forms.Button();
            this.bntContinue = new System.Windows.Forms.Button();
            this.bntStop = new System.Windows.Forms.Button();
            this.bntCapture = new System.Windows.Forms.Button();
            this.imgVideo = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.04918F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.95082F));
            this.tableLayoutPanel1.Controls.Add(this.dtGrid, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.924915F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96.07509F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1098, 586);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dtGrid
            // 
            this.dtGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dtGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MistyRose;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrid.Location = new System.Drawing.Point(299, 25);
            this.dtGrid.Name = "dtGrid";
            this.dtGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtGrid.RowHeadersVisible = false;
            this.dtGrid.Size = new System.Drawing.Size(796, 558);
            this.dtGrid.TabIndex = 13;
            this.dtGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGrid_CellClick);
            this.dtGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGrid_CellEndEdit);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fileUrlTxtBx);
            this.groupBox1.Controls.Add(this.nationalityTxt);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.addressTxt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.typeCbx);
            this.groupBox1.Controls.Add(this.surnameTxt);
            this.groupBox1.Controls.Add(this.contactTxt);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.saveBtn);
            this.groupBox1.Location = new System.Drawing.Point(3, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 548);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // fileUrlTxtBx
            // 
            this.fileUrlTxtBx.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.fileUrlTxtBx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fileUrlTxtBx.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileUrlTxtBx.Location = new System.Drawing.Point(115, 510);
            this.fileUrlTxtBx.Name = "fileUrlTxtBx";
            this.fileUrlTxtBx.Size = new System.Drawing.Size(171, 19);
            this.fileUrlTxtBx.TabIndex = 150;
            // 
            // nationalityTxt
            // 
            this.nationalityTxt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.nationalityTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nationalityTxt.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nationalityTxt.Location = new System.Drawing.Point(115, 377);
            this.nationalityTxt.Name = "nationalityTxt";
            this.nationalityTxt.Size = new System.Drawing.Size(171, 19);
            this.nationalityTxt.TabIndex = 149;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 386);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 148;
            this.label11.Text = "Nationality";
            // 
            // addressTxt
            // 
            this.addressTxt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.addressTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.addressTxt.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressTxt.Location = new System.Drawing.Point(115, 402);
            this.addressTxt.Multiline = true;
            this.addressTxt.Name = "addressTxt";
            this.addressTxt.Size = new System.Drawing.Size(171, 50);
            this.addressTxt.TabIndex = 147;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 409);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 146;
            this.label9.Text = "Full address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 361);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 144;
            this.label8.Text = "Type";
            // 
            // typeCbx
            // 
            this.typeCbx.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.typeCbx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.typeCbx.FormattingEnabled = true;
            this.typeCbx.Items.AddRange(new object[] {
            "Supplier",
            "Customer",
            "Other"});
            this.typeCbx.Location = new System.Drawing.Point(115, 350);
            this.typeCbx.Name = "typeCbx";
            this.typeCbx.Size = new System.Drawing.Size(171, 21);
            this.typeCbx.TabIndex = 145;
            // 
            // surnameTxt
            // 
            this.surnameTxt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.surnameTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.surnameTxt.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.surnameTxt.Location = new System.Drawing.Point(115, 300);
            this.surnameTxt.Name = "surnameTxt";
            this.surnameTxt.Size = new System.Drawing.Size(171, 19);
            this.surnameTxt.TabIndex = 143;
            // 
            // contactTxt
            // 
            this.contactTxt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.contactTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contactTxt.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contactTxt.Location = new System.Drawing.Point(115, 325);
            this.contactTxt.Name = "contactTxt";
            this.contactTxt.Size = new System.Drawing.Size(171, 19);
            this.contactTxt.TabIndex = 142;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 332);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 141;
            this.label12.Text = "Phone number";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.imgCapture);
            this.groupBox2.Controls.Add(this.bntStart);
            this.groupBox2.Controls.Add(this.bntContinue);
            this.groupBox2.Controls.Add(this.bntStop);
            this.groupBox2.Controls.Add(this.bntCapture);
            this.groupBox2.Controls.Add(this.imgVideo);
            this.groupBox2.Location = new System.Drawing.Point(11, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 260);
            this.groupBox2.TabIndex = 140;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Camera";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button4.Location = new System.Drawing.Point(5, 136);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(98, 24);
            this.button4.TabIndex = 101;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // imgCapture
            // 
            this.imgCapture.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.imgCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCapture.Image = global::VPOS.Properties.Resources.temp;
            this.imgCapture.Location = new System.Drawing.Point(145, 136);
            this.imgCapture.Name = "imgCapture";
            this.imgCapture.Size = new System.Drawing.Size(110, 110);
            this.imgCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCapture.TabIndex = 100;
            this.imgCapture.TabStop = false;
            // 
            // bntStart
            // 
            this.bntStart.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.bntStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bntStart.FlatAppearance.BorderSize = 0;
            this.bntStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntStart.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bntStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntStart.Location = new System.Drawing.Point(6, 20);
            this.bntStart.Name = "bntStart";
            this.bntStart.Size = new System.Drawing.Size(97, 23);
            this.bntStart.TabIndex = 16;
            this.bntStart.Text = "Start";
            this.bntStart.UseVisualStyleBackColor = false;
            this.bntStart.Click += new System.EventHandler(this.bntStart_Click);
            // 
            // bntContinue
            // 
            this.bntContinue.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.bntContinue.FlatAppearance.BorderSize = 0;
            this.bntContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntContinue.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bntContinue.Location = new System.Drawing.Point(6, 78);
            this.bntContinue.Name = "bntContinue";
            this.bntContinue.Size = new System.Drawing.Size(97, 23);
            this.bntContinue.TabIndex = 18;
            this.bntContinue.Text = "Continue";
            this.bntContinue.UseVisualStyleBackColor = false;
            this.bntContinue.Click += new System.EventHandler(this.bntContinue_Click);
            // 
            // bntStop
            // 
            this.bntStop.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.bntStop.FlatAppearance.BorderSize = 0;
            this.bntStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntStop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bntStop.Location = new System.Drawing.Point(6, 49);
            this.bntStop.Name = "bntStop";
            this.bntStop.Size = new System.Drawing.Size(97, 23);
            this.bntStop.TabIndex = 17;
            this.bntStop.Text = "Stop";
            this.bntStop.UseVisualStyleBackColor = false;
            this.bntStop.Click += new System.EventHandler(this.bntStop_Click);
            // 
            // bntCapture
            // 
            this.bntCapture.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.bntCapture.FlatAppearance.BorderSize = 0;
            this.bntCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCapture.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bntCapture.Location = new System.Drawing.Point(6, 107);
            this.bntCapture.Name = "bntCapture";
            this.bntCapture.Size = new System.Drawing.Size(97, 23);
            this.bntCapture.TabIndex = 19;
            this.bntCapture.Text = "Capture Image";
            this.bntCapture.UseVisualStyleBackColor = false;
            this.bntCapture.Click += new System.EventHandler(this.bntCapture_Click);
            // 
            // imgVideo
            // 
            this.imgVideo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.imgVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgVideo.Location = new System.Drawing.Point(145, 20);
            this.imgVideo.Name = "imgVideo";
            this.imgVideo.Size = new System.Drawing.Size(109, 110);
            this.imgVideo.TabIndex = 98;
            this.imgVideo.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Snow;
            this.button2.Location = new System.Drawing.Point(115, 478);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 27);
            this.button2.TabIndex = 137;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 300);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 134;
            this.label16.Text = "Name";
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.saveBtn.FlatAppearance.BorderSize = 0;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.ForeColor = System.Drawing.Color.Snow;
            this.saveBtn.Location = new System.Drawing.Point(217, 478);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(2);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(69, 27);
            this.saveBtn.TabIndex = 135;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = global::VPOS.Properties.Resources.Cancel_24;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1076, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(19, 16);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TransactorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1130, 607);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransactorForm";
            this.Text = "TransactorForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox imgCapture;
        private System.Windows.Forms.Button bntStart;
        private System.Windows.Forms.Button bntContinue;
        private System.Windows.Forms.Button bntStop;
        private System.Windows.Forms.Button bntCapture;
        private System.Windows.Forms.PictureBox imgVideo;
        private System.Windows.Forms.TextBox surnameTxt;
        private System.Windows.Forms.TextBox contactTxt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox typeCbx;
        private System.Windows.Forms.TextBox nationalityTxt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox addressTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox fileUrlTxtBx;
        private System.Windows.Forms.DataGridView dtGrid;
    }
}