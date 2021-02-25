using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominator_of_steam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string Steam_Path;
        private void button1_Click(object sender, EventArgs e)
        {
            Start_Program();
            
        }

        private void Start_Program()
        {

            Steam_Get_Path();
            Steam_Close();
            Steam_Disk_Cleaner();
            Steam_Reg_Cleaner();

        }

        private void Steam_Reg_Cleaner()//step 4
        {
            try
            {
                Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\WOW6432Node\Valve");
                textBox1.Text += Environment.NewLine + "Запись №1 успешно удалена из реестра";
            }
            catch
            {
                textBox1.Text += Environment.NewLine + "Запись №1 в реестре не обнаружена";
            }
            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\Valve");
                textBox1.Text += Environment.NewLine + "Запись №2 успешно удалена из реестра";
            }
            catch
            {
                textBox1.Text += Environment.NewLine + "Запись №2 в реестре не обнаружена";
            }

        }

        private void Steam_Disk_Cleaner()//step 3
        {

            if (File.Exists(Steam_Path + "\\config\\loginusers.vdf") == true)
            {
                File.Delete(Steam_Path + "\\config\\loginusers.vdf");
                textBox1.Text += Environment.NewLine + "loginusers.vdf удален";
            }
            else
            {
                textBox1.Text += Environment.NewLine + "loginusers.vdf отсутствует";
            }
         
            try
            {
                Directory.Delete(Steam_Path + "\\appcache\\stats", true);
                textBox1.Text += Environment.NewLine + "stats удалена";
            }
            catch 
            {
                textBox1.Text += Environment.NewLine + "stats отсутствует";
            }
            try 
            {
                Directory.Delete(Steam_Path + "\\userdata", true);
                textBox1.Text += Environment.NewLine + "userdata удалена";
            }
            catch (DirectoryNotFoundException)
            {
                textBox1.Text += Environment.NewLine + "userdata отсутствует";
            }
            bool Exist_File = false;
            try
            {
                foreach (var f in Directory.GetFiles(Steam_Path))
                {
                    if (f.IndexOf("ssfn") != (-1))
                    {
                        File.Delete(f);
                        textBox1.Text += Environment.NewLine + f + " удален";
                        Exist_File = true;
                    }
                    if (Exist_File = false)
                    {
                        textBox1.Text += Environment.NewLine + "Файлы ssfn отсутствуют";
                    }
                }
            }
            catch
            {
               
            }
            



        }
        private void Steam_Get_Path() //steap 1
        {
            foreach (Process proc in Process.GetProcessesByName("steam"))
            {
                Steam_Path = proc.MainModule.FileName;
            }

            try
            {
                Steam_Path = Steam_Path.Substring(0, Steam_Path.Length - 10);
                textBox1.Text = "Путь к папке найден: " + Steam_Path;
            }
            catch (System.NullReferenceException)
            {
                textBox1.Text = "Steam не обнаружен!";
            }
            
           
        }
        private void Steam_Close() //steap 2
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("steam"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Point lastPoint;
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
