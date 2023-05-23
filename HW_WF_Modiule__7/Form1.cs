using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HW_WF_Modiule__7
{
    public partial class Form1 : Form
    {
        double a, b, h;
        double x, y;
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
            {
                MessageBox.Show("Выберите хотябы один график", "Внимание!");
                return;
            }
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Введены не все параметры. Применяются значения по умолчанию.");
                Default();
            }
            else
            {


                if (double.TryParse(textBox1.Text, out a))
                {
                    if (double.TryParse(textBox2.Text, out b))
                    {
                        if (textBox3.Text.Contains(".")) textBox3.Text = textBox3.Text.Replace(".", ",");
                        if (double.TryParse(textBox3.Text, out h)) ;
                        else
                        { 
                            MessageBox.Show("Некорректные данные"); return;
                        }
                    }
                    else
                    { 
                        MessageBox.Show("Некорректные данные"); return;
                    }
        
                }
                else
                {
                    MessageBox.Show("Некорректные данные");return; 
                }


            }

            if(checkBox1.Checked == true)
            {
                x = a;
                this.chart.Series[0].Points.Clear();
                while (x <= b)
                {
                    y = Math.Cos(x);
                    this.chart.Series[0].Points.AddXY(x, y);
                    x += h;
                }
            }
            else this.chart.Series[0].Points.Clear();
            if (checkBox2.Checked == true)
            {
                x = a;
                this.chart.Series[1].Points.Clear();
                while (x <= b)
                {
                    y = Math.Sin(x);
                    this.chart.Series[1].Points.AddXY(x, y);
                    x += h;
                }
            }
            else this.chart.Series[1].Points.Clear();
            if (checkBox3.Checked == true)
            {
                x = a;
                this.chart.Series[2].Points.Clear();
                while (x <= b)
                {
                    y = Math.Sqrt(x);
                    this.chart.Series[2].Points.AddXY(x, y);
                    x += h;
                }
            }
            else this.chart.Series[2].Points.Clear();
            if (checkBox4.Checked == true)
            {
                x = a;
                this.chart.Series[3].Points.Clear();
                while (x <= b)
                {
                    y = Math.Pow(x,2);
                    this.chart.Series[3].Points.AddXY(x, y);
                    x += h;
                }
            }
            else this.chart.Series[3].Points.Clear();






        }
        private void Default()
        {
            a = 1 - 0;
            b = 10;
            h = 0.1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.chart.Series[0].Points.Clear();
            this.chart.Series[1].Points.Clear();
            this.chart.Series[2].Points.Clear();
            this.chart.Series[3].Points.Clear();
            return;
        }
    }
}
