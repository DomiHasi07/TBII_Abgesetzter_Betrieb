using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq.Expressions;

namespace TBII_Abgesetzter_Betrieb
{
    public partial class Form1 : Form
    {
        DataTable dt_Einträge = new DataTable();
        string current_file;

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32.dll")]
        static extern IntPtr CloseClipboard();

        [DllImport("user32.dll")]
        static extern IntPtr GetOpenClipboardWindow();

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_ABSOLUTE = 0x08000;
        private const int MOUSEEVENTF_MOVE = 0x01;

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        int anz_Zeilen = 0;

        public Form1()
        {
            InitializeComponent();
            dt_Einträge.Columns.Add("Name");
            dt_Einträge.Columns.Add("IP_Adresse");
            dt_Einträge.Columns.Add("Port");
            Btn_Send.Enabled = false;
            Btn_Save.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd_1.Filter = "CSV Files (*.csv)|*.csv";
            ofd_1.FileName = Path.GetFileNameWithoutExtension(ofd_1.FileName);

            if (ofd_1.ShowDialog() == DialogResult.OK)
            {
                if (String.Equals(Path.GetExtension(ofd_1.FileName), ".csv", StringComparison.OrdinalIgnoreCase))
                {
                    Daten_einlesen(ofd_1.FileName);
                }
                else
                {
                    MessageBox.Show("Dieser Dateityp wird nicht unterstützt. Bitte ein csv File wählen", "Falscher Dateityp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void wait(int milliseconds)
        {
            System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            t1.Interval = milliseconds;
            t1.Enabled = true;
            t1.Start();
            t1.Tick += (s, e) =>
            {
                t1.Enabled = false;
                t1.Stop();
            };
            while (t1.Enabled)
            {
                Application.DoEvents();
            }
        }

        public void Daten_übertragen()
        {
            DataTable transfer = (DataTable)(dgv_1.DataSource);

            Process[] arr_p = Process.GetProcessesByName("agb");
            if (arr_p.Length != 0)
            {
                Process p = arr_p[0];
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);

                if(Fenster_auswählen(h))
                {
                    
                    foreach (Control ctrl in this.Controls[0].Controls)
                    {
                        if (ctrl is Button && (ctrl.Name != "Btn_Stop"))
                        {
                            this.Invoke(new Action(() =>
                            {
                                ctrl.Enabled = false;
                            }));
                        }
                    }
                    anz_Zeilen = dgv_1.Rows.Count - 1;

                    SendKeys.Send("%");
                    SendKeys.Send("{RIGHT}");
                    wait(20);
                    for (int i = 0; i < 19; i++)
                        SendKeys.Send("{DOWN}");
                    wait(20);
                    SendKeys.Send("{RIGHT}");
                    wait(20);
                    SendKeys.Send("{ENTER}");

                    SendKeys.Send("^n");
                    SendKeys.Send((dgv_1.Rows.Count - 1).ToString());
                    SendKeys.Send("{ENTER}");
                    wait(100);
                    Start_Position(1);
                    wait(50);

                    for (int i = 0; i < dgv_1.Rows.Count - 1; i++)
                    {
                        SendKeys.SendWait(transfer.Rows[i]["Name"].ToString());
                        SendKeys.SendWait("{DOWN}");
                    }

                    Start_Position(2);
                    wait(100);

                    for (int i = 0; i < dgv_1.Rows.Count - 1; i++)
                    {
                        SendKeys.SendWait(transfer.Rows[i]["IP_Adresse"].ToString());
                        SendKeys.SendWait("{DOWN}");
                    }

                    Start_Position(3);
                    wait(100);

                    for (int i = 0; i < dgv_1.Rows.Count - 1; i++)
                    {
                        SendKeys.SendWait(transfer.Rows[i]["Port"].ToString());
                        SendKeys.SendWait("{DOWN}");
                    }
                    foreach (Control ctrl in this.Controls[0].Controls)
                    {
                        if (ctrl is Button)
                        {
                            this.Invoke(new Action(() =>
                            {
                                ctrl.Enabled = true;
                            }));
                        }
                    }
                    
                }
            }
            else
            {
                Programm_nicht_gefunden();
            }
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            Daten_übertragen();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < dgv_1.RowCount - 1; i++)
            {
                var cells = dgv_1.Rows[i].Cells.Cast<DataGridViewCell>();
                sb.AppendLine(String.Join(";", cells.Select(cell => cell.Value).ToArray()));
            }

            sfd_1.Filter = "CSV Files (*.csv)|*.csv";

            if (sfd_1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd_1.FileName, sb.ToString());
                current_file = Path.GetFileNameWithoutExtension(sfd_1.FileName);
                this.Text = "Telefonliste von " + current_file;
            }

        }

        private void dgv_1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                änderung();
            }
        }

        private void dgv_1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            änderung();
        }

        public void änderung()
        {
            Btn_Save.Enabled = true;
            this.Text = "Telefonliste von " + current_file + "*";
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            if (dgv_1.DataSource != null)
            {
                dgv_1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgv_1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (dgv_1.DataSource != null)
            {
                dgv_1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv_1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void Daten_einlesen(string filepath)
        {
            current_file = Path.GetFileNameWithoutExtension(filepath);

            dt_Einträge.Clear();

            DataRow rw = dt_Einträge.NewRow();

            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(new Char[] { ',', ';' });

                    rw = dt_Einträge.NewRow();
                    rw["Name"] = values[0].ToString();
                    rw["IP_Adresse"] = values[1].ToString();
                    rw["Port"] = values[2].ToString();
                    dt_Einträge.Rows.Add(rw);

                }
                dgv_1.DataSource = dt_Einträge;
                foreach (DataGridViewColumn column in dgv_1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                Btn_Send.Enabled = true;
                this.Text = "Telefonliste von " + current_file;
            }
        }

        private void dgv_1_DragDrop(object sender, DragEventArgs e)
        {
            string[] tmp_files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Daten_einlesen(tmp_files[0]);
        }

        private void dgv_1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Btn_TBII_einlesen_Click(object sender, EventArgs e)
        {
            Boolean anz_zeilen_is_numeric = false;

            Process[] arr_p = Process.GetProcessesByName("agb");
            if (arr_p.Length != 0)
            {
                Process p = arr_p[0];
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                if (Fenster_auswählen(h))
                {
                    p = Process.GetCurrentProcess();
                    IntPtr main = p.MainWindowHandle;
                    SetForegroundWindow(main);
                    do
                    {
                        string temp_anz = Microsoft.VisualBasic.Interaction.InputBox("Bitte Zeilenanzahl eingeben. ", "Zeilenanzahl eingeben", "Zeilenanzahl");

                        if (temp_anz != "")
                            anz_zeilen_is_numeric = temp_anz.All(char.IsNumber);
                        else
                            return;

                        if (anz_zeilen_is_numeric)
                            anz_Zeilen = Int16.Parse(temp_anz);
                        else
                            MessageBox.Show("Bitte nur Zahlen eingeben");

                    }
                    while (!anz_zeilen_is_numeric);

                    dt_Einträge.Clear();
                    dgv_1.DataSource = null;
                    dgv_1.Rows.Clear();
                    dgv_1.Refresh();

                    foreach (Control ctrl in this.Controls[0].Controls)
                    {
                        if (ctrl is Button)
                            ctrl.Enabled = false;
                    }

                    try
                    {
                        SetForegroundWindow(h);

                        Start_Position(1);

                        DataRow rw = dt_Einträge.NewRow();

                        for (int i = 0; i < anz_Zeilen + 1; i++)
                        {
                            rw = dt_Einträge.NewRow();
                            rw["Name"] = Werte_einlesen_von_TB();
                            dt_Einträge.Rows.Add(rw);
                            SendKeys.Send("{DOWN}");
                        }

                        Start_Position(2);

                        for (int i = 0; i < anz_Zeilen + 1; i++)
                        {
                            dt_Einträge.Rows[i]["IP_Adresse"] = Werte_einlesen_von_TB();
                            SendKeys.Send("{DOWN}");
                        }

                        Start_Position(3);

                        for (int i = 0; i < anz_Zeilen + 1; i++)
                        {
                            dt_Einträge.Rows[i]["Port"] = Werte_einlesen_von_TB();
                            SendKeys.Send("{DOWN}");

                        }

                        Werte_überprüfen();

                        dgv_1.DataSource = dt_Einträge;
                        foreach (DataGridViewColumn column in dgv_1.Columns)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Bitte erneut probieren");
                    }


                    foreach (Control ctrl in this.Controls[0].Controls)
                    {
                        if (ctrl is Button)
                            ctrl.Enabled = true;
                    }
                }
            }
            else
            {
                Programm_nicht_gefunden();
            }
            
        }

        private void Programm_nicht_gefunden()
        {
            if (MessageBox.Show("Der Abgesetzte Betrieb ist nicht gestartet. Soll der Abgesetzte Betrieb gestartet werden?", "Programm geschlossen", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start("agb.exe");
            }
        }

        private string Werte_einlesen_von_TB()
        {
            Process[] arr_p = Process.GetProcessesByName("agb");
            Process p = arr_p[0];
            IntPtr h = p.MainWindowHandle;
            SetForegroundWindow(h);

            string temp1 = "temp1";
            SendKeys.Send("{ENTER}");
            SendKeys.Send("+{F10}");
            SendKeys.Send("{DOWN}");
            SendKeys.Send("{DOWN}");
            SendKeys.Send("{DOWN}");
            SendKeys.Send("{ENTER}");
            temp1 = Clipboard.GetText();
            return temp1;
        }

        public void Werte_überprüfen()
        {
            Start_Position(1);

            string temp1 = "";
            for (int i = 0;i<=anz_Zeilen;i++)
            {
                wait(50);
                SendKeys.Send("{ENTER}");
                SendKeys.Send("+{F10}");
                wait(100);
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{ENTER}");
                wait(50);
                temp1 = Clipboard.GetText();

                if (!(temp1 == dt_Einträge.Rows[i]["Name"].ToString() && dt_Einträge.Rows[i]["Name"].ToString()!=""))
                {
                    dt_Einträge.Rows[i]["Name"] = temp1;
                }
                SendKeys.Send("{DOWN}");
                wait(50);
            }

            Start_Position(2);

            wait(100);

            temp1 = "";
            for (int i = 0; i <= anz_Zeilen; i++)
            {
                SendKeys.Send("{ENTER}");
                SendKeys.Send("+{F10}");
                wait(50);
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{ENTER}");
                wait(50);
                temp1 = Clipboard.GetText();

                if (!(temp1 == dt_Einträge.Rows[i]["IP_Adresse"].ToString() && dt_Einträge.Rows[i]["IP_Adresse"].ToString() != ""))
                {
                    dt_Einträge.Rows[i]["IP_Adresse"] = temp1;
                }
                SendKeys.Send("{DOWN}");
                wait(50);
            }

            Start_Position(3);

            wait(100);

            temp1 = "";
            for (int i = 0; i <= anz_Zeilen; i++)
            {
                SendKeys.Send("{ENTER}");
                SendKeys.Send("+{F10}");
                wait(50);
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{ENTER}");
                wait(50);
                temp1 = Clipboard.GetText();

                if (!(temp1 == dt_Einträge.Rows[i]["Port"].ToString() && dt_Einträge.Rows[i]["Port"].ToString() != ""))
                {
                    dt_Einträge.Rows[i]["Port"] = temp1;
                }
                SendKeys.Send("{DOWN}");
                wait(50);
            }
        }

        public void Start_Position(int Spalte)
        {
            for (int i = 0; i < 6; i++)
            {
                SendKeys.SendWait("{LEFT}");
            }

            for (int i = 0; i < anz_Zeilen+10 ; i++)
            {
                SendKeys.SendWait("{UP}");
            }

            wait(50);

            for (int i = 0; i < Spalte; i++)
                SendKeys.SendWait("{RIGHT}");

            SendKeys.SendWait("{DOWN}");
        }

        public Boolean Fenster_auswählen(IntPtr h)
        {
            Rect WindowSize = new Rect();
            GetWindowRect(h, ref WindowSize);

            
            int windowheight = WindowSize.Bottom - WindowSize.Top;
            int windowlenght = WindowSize.Right - WindowSize.Left;
            int unterRand = WindowSize.Top + windowheight - Screen.PrimaryScreen.WorkingArea.Height;

            if (unterRand < 0)
                unterRand = 0;

            if (windowheight > 265 && windowlenght > 340 && windowheight - unterRand > 265)
            {
                Point start_Mouse_Position = Cursor.Position;
                SetCursorPos(WindowSize.Left + 30, WindowSize.Bottom - 75 - unterRand);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                SetCursorPos(start_Mouse_Position.X, start_Mouse_Position.Y);
                return true;
            }
            else
            {
                Process p = Process.GetCurrentProcess();
                IntPtr main = p.MainWindowHandle;
                SetForegroundWindow(main);
                if(windowheight - unterRand < 265)
                {
                    MessageBox.Show("Fenster der Telefonliste bitte weiter nach oben verschieben");
                }
                else
                {
                    MessageBox.Show("Fenster der Telefonliste bitte vergrößern");
                }
                return false;
            }
        }
    }
}



