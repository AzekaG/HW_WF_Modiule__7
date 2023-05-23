using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Task2_Clock
{
    public partial class Form1 : Form
    {
        [DllImport("user32", CharSet = CharSet.Auto)]        //настройка перемещения окна с помощью мыши
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();            //и подключаем функции из библиотеки user32.dll , которые помогут переносить окно с помощью мышки



        int cx = 175, cy = 175; // центр картинки , задаем ось для стрелок
        int secHand = 100, minHand = 90, hrHand = 75; // длины стрелок


        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;            //делаем форму прозрачной
            this.AllowTransparency = true;
            this.BackColor = Color.AliceBlue;
            this.TransparencyKey = this.BackColor;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            int s = DateTime.Now.Second;
            int m = DateTime.Now.Minute;
            int h = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            Graphics g = pictureBox1.CreateGraphics();

            //сотрем предыдущее положение стрелки
            handCoord = msCoord(s, secHand + 4);
            g.DrawLine(new Pen(this.BackColor, 45f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = msCoord(m, minHand + 4);
            g.DrawLine(new Pen(this.BackColor, 45f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = hrCoord(h%12,m, hrHand + 4);
            g.DrawLine(new Pen(this.BackColor, 20f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));



            //рисуем стрелки 
            handCoord = hrCoord(h % 12, m, hrHand);
            g.DrawLine(new Pen(Color.Black, 4f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = msCoord(m ,  minHand);
            g.DrawLine(new Pen(Color.Yellow, 4f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = msCoord(s , secHand);
            g.DrawLine(new Pen(Color.Red, 4f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));
        }

 

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(this.timer1_Tick);
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // запуск тех самых функций для перемещния мыши
        {
            const uint WM_SYSCOMMAND = 0x0112;
            const uint DOMOVE = 0xF012;
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);

        }

        private int[] msCoord(int val, int hlen)   // координаты вторйо точки для минутной и секундной стрелок
        {   //val - количество секунд или минут , hlen - длина стрелки 
            int[] coord = new int[2];

            val *= 6;   //1 минута или 1 секунда отклоняет стрелку на  (360/60 = 6 градусов)

            if (val >= 0 && val <= 100)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            //получили угол в радианах т.к. math.Sin принмиает параметры в радианах
            return coord;

        }
        private int[] hrCoord(int hval, int mval, int hlen)   // координаты вторйо точки для минутной и секундной стрелок
        {
            int[] coord = new int[2];

            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 100)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }

            return coord;


        }
    }
}
