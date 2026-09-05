using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Form form = new Form();
        form.Text = "Czasowy Wyłącznik Komputera";
        form.Size = new Size(450, 350);
        form.StartPosition = FormStartPosition.CenterScreen;
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.MaximizeBox = false;
        form.BackColor = Color.FromArgb(248, 249, 250);

        Label label = new Label();
        label.Location = new Point(30, 25);
        label.Size = new Size(375, 30);
        label.Text = "Podaj czas w minutach:";
        label.Font = new Font("Segoe UI", 13, FontStyle.Bold);
        label.TextAlign = ContentAlignment.MiddleCenter;
        form.Controls.Add(label);

        TextBox textBox = new TextBox();
        textBox.Location = new Point(137, 65);
        textBox.Size = new Size(160, 35);
        textBox.Text = "60";
        textBox.Font = new Font("Segoe UI", 14);
        textBox.TextAlign = HorizontalAlignment.Center;
        form.Controls.Add(textBox);

        Label lblStatus = new Label();
        lblStatus.Location = new Point(20, 235);
        lblStatus.Size = new Size(395, 30);
        lblStatus.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        form.Controls.Add(lblStatus);

        Button btnStart = new Button();
        btnStart.Location = new Point(75, 115);
        btnStart.Size = new Size(284, 45);
        btnStart.Text = "Ustaw wyłączenie";
        btnStart.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        btnStart.BackColor = Color.FromArgb(25, 135, 84);
        btnStart.ForeColor = Color.White;
        btnStart.FlatStyle = FlatStyle.Flat;
        btnStart.FlatAppearance.BorderSize = 0;
        btnStart.Click += (s, e) =>
        {
            double minuty;
            if (double.TryParse(textBox.Text, out minuty) && minuty > 0)
            {
                int sekundy = (int)Math.Round(minuty * 60);
                Process.Start("shutdown.exe", "/s /t " + sekundy);
                lblStatus.Text = "Wyłączenie zaplanowane za " + minuty + " min.";
                lblStatus.ForeColor = Color.FromArgb(25, 135, 84);
            }
            else
            {
                MessageBox.Show("Wpisz poprawną liczbę minut większą od 0.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        form.Controls.Add(btnStart);

        Button btnCancel = new Button();
        btnCancel.Location = new Point(75, 170);
        btnCancel.Size = new Size(284, 45);
        btnCancel.Text = "Anuluj wyłączenie";
        btnCancel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        btnCancel.BackColor = Color.FromArgb(220, 53, 69);
        btnCancel.ForeColor = Color.White;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.Click += (s, e) =>
        {
            Process.Start("shutdown.exe", "/a");
            lblStatus.Text = "Anulowano planowane wyłączenie.";
            lblStatus.ForeColor = Color.FromArgb(220, 53, 69);
        };
        form.Controls.Add(btnCancel);

        Application.Run(form);
    }
}