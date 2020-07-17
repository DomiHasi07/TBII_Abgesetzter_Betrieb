namespace TBII_Abgesetzter_Betrieb
{
    partial class Form1
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
            this.Btn_1 = new System.Windows.Forms.Button();
            this.ofd_1 = new System.Windows.Forms.OpenFileDialog();
            this.dgv_1 = new System.Windows.Forms.DataGridView();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.sfd_1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_TBII_einlesen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_1
            // 
            this.Btn_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_1.Location = new System.Drawing.Point(3, 3);
            this.Btn_1.Name = "Btn_1";
            this.Btn_1.Size = new System.Drawing.Size(121, 44);
            this.Btn_1.TabIndex = 0;
            this.Btn_1.Text = "Daten einlesen";
            this.Btn_1.UseVisualStyleBackColor = true;
            this.Btn_1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ofd_1
            // 
            this.ofd_1.DefaultExt = "csv";
            this.ofd_1.FilterIndex = 0;
            // 
            // dgv_1
            // 
            this.dgv_1.AllowDrop = true;
            this.dgv_1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgv_1, 4);
            this.dgv_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_1.Location = new System.Drawing.Point(3, 53);
            this.dgv_1.Name = "dgv_1";
            this.dgv_1.Size = new System.Drawing.Size(504, 331);
            this.dgv_1.TabIndex = 1;
            this.dgv_1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_1_CellValueChanged);
            this.dgv_1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgv_1_RowsRemoved);
            this.dgv_1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_1_DragDrop);
            this.dgv_1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgv_1_DragEnter);
            // 
            // Btn_Send
            // 
            this.Btn_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Send.Location = new System.Drawing.Point(130, 3);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(121, 44);
            this.Btn_Send.TabIndex = 2;
            this.Btn_Send.Text = "Nach Toolbox übertragen";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Save.Location = new System.Drawing.Point(384, 3);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(123, 44);
            this.Btn_Save.TabIndex = 3;
            this.Btn_Save.Text = "Daten speichern";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // sfd_1
            // 
            this.sfd_1.DefaultExt = "csv";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99812F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.Btn_1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Btn_Save, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_Send, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_TBII_einlesen, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(510, 387);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Btn_TBII_einlesen
            // 
            this.Btn_TBII_einlesen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_TBII_einlesen.Location = new System.Drawing.Point(257, 3);
            this.Btn_TBII_einlesen.Name = "Btn_TBII_einlesen";
            this.Btn_TBII_einlesen.Size = new System.Drawing.Size(121, 44);
            this.Btn_TBII_einlesen.TabIndex = 4;
            this.Btn_TBII_einlesen.Text = "Von Toolbox übertragen";
            this.Btn_TBII_einlesen.UseVisualStyleBackColor = true;
            this.Btn_TBII_einlesen.Click += new System.EventHandler(this.Btn_TBII_einlesen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 411);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Telefonliste";
            this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_1;
        private System.Windows.Forms.OpenFileDialog ofd_1;
        private System.Windows.Forms.DataGridView dgv_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn str_Name;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.SaveFileDialog sfd_1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Btn_TBII_einlesen;
    }
}

