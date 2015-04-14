using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusyShop
{
    public partial class Form1 : Form
    {
        #region Members
        public bool NeedPaint;
        public int CustomersPerMinute { get; set; }
        public int PaintNumber;
        public Nikolaev.WorkSpace.CasseState PaintState;
        public int PaintQueue;
        public Nikolaev.WorkSpace.Shop shop = new Nikolaev.WorkSpace.Shop();
        private Image drawingArea;
        public bool Isclosing;
        #endregion Members
        public Form1()
        {
            CustomersPerMinute = 10;
            InitializeComponent();
            drawingArea = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = drawingArea;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int Width = pictureBox1.Width;
            int Height = pictureBox1.Height;
        }

        private void ScrollBarRight_Scroll(object sender, ScrollEventArgs e)
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            int Width = pictureBox1.Width;
            int Height = pictureBox1.Height;
            int oldValue = shop.AmountOfCasses;
            //принимаем старое значение скролла и меняем его на новое значение,получаем колличество семафоров на релиз
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                shop.AmountOfCasses = ScrollBarRight.Value;
                shop.ReleaseCount = shop.AmountOfCasses - shop.WorkingCasses();
                if (shop.ReleaseCount > 0)
                {
                    int das = shop.semaphore.Release(shop.ReleaseCount);
                }
            }
            Rectangle rect = new Rectangle(Width / 2, Height / 10, 30 * shop.AmountOfCasses, 40 * shop.AmountOfCasses);
            textBox2.Text = shop.AmountOfCasses.ToString();           
        }

        private void ScrollBarLeft_Scroll(object sender, ScrollEventArgs e)
        {
            //скролл устанавливает скорость наплыва покупателей и обновляет таймер
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                shop.AmountOfCustomers = ScrollBarLeft.Value;
            }
            timer1.Interval = 600 / (shop.AmountOfCustomers + 1);
            textBox1.Text = shop.AmountOfCustomers.ToString();
        }
        private void shop_ReadyForWork(object sender, Nikolaev.WorkSpace.CasseEventArgs eventArgs)
        {
            ReDrawCasseState(eventArgs);         
        }
        //важная конструкция
        private delegate void ReDrawCasseStateDelegate(Nikolaev.WorkSpace.CasseEventArgs e);

        private void ReDrawCasseState(Nikolaev.WorkSpace.CasseEventArgs e)
        {
            if (Isclosing)
            
                return;
            System.Diagnostics.Debug.WriteLine("Entered ={0} is closing = {1}", e.State,Isclosing);
            if (this.InvokeRequired)
            {
                this.BeginInvoke((ReDrawCasseStateDelegate)ReDrawCasseState, new object[] { e });
                return;
            }

            var g = Graphics.FromImage(pictureBox1.Image);
            int Width = pictureBox1.Width;
            int Height = pictureBox1.Height;
            //если касса работает перекрашиваем ее в зеленый цвет 
            if (e.State == Nikolaev.WorkSpace.CasseState.Working)
            {
                PaintNumber = e.CasseNumber;
                PaintState = e.State;
                PaintQueue = e.QueueLenth;
                g.FillRectangle(Brushes.Green, Width / 2, Height / 10 + 25 * PaintNumber, 20, 20);
                for (int i = 1; i < PaintQueue; i++)
                {
                    g.DrawEllipse(Pens.Red, Width / 2 - i * 15, Height / 10 + 25 * PaintNumber, 15, 15);
                }
                pictureBox1.Invalidate();
            }
            //при добавление кастомера рисуем его красным кружочком
            if (e.State == Nikolaev.WorkSpace.CasseState.Adding)
            {
                PaintNumber = e.CasseNumber;
                PaintState = e.State;
                PaintQueue = e.QueueLenth;
                for (int i = 1; i < PaintQueue; i++)
                {
                    g.DrawEllipse(Pens.Red, Width / 2 - i * 15, Height / 10 + 25 * PaintNumber, 15, 15);
                }
                pictureBox1.Invalidate();
            }
            //если касса нагружена рисуем ее красным
            if (e.State == Nikolaev.WorkSpace.CasseState.IsBusy)
            {
                shop.ReleaseCount += 1;
                PaintNumber = e.CasseNumber;
                PaintState = e.State;
                PaintQueue = e.QueueLenth;
                g.FillRectangle(Brushes.Red, Width / 2, Height / 10 + 25 * PaintNumber, 20, 20);
                for (int i = 1; i < PaintQueue; i++)
                {
                    g.DrawEllipse(Pens.Red, Width / 2 - i * 15, Height / 10 + 25 * PaintNumber, 15, 15);
                }
                pictureBox1.Invalidate();
            }
            //если касса закончила работу она перекрашивается в черный и закрашивает свою очередь
            if (e.State == Nikolaev.WorkSpace.CasseState.DoneWorking)
            {
                PaintNumber = e.CasseNumber;
                PaintState = e.State;
                PaintQueue = e.QueueLenth;
                g.FillRectangle(Brushes.Black, Width / 2, Height / 10 + 25 * PaintNumber, 20, 20);

                ScrollBarRight.Value -= 1;

                for (int i = 1; i < PaintQueue; i++)
                {
                    g.DrawRectangle(Pens.White, Width / 2 - i * 15, Height / 10 + 25 * PaintNumber, Width / 2 - PaintQueue * 15, Height / 10 + 25 * PaintNumber);
                }
                pictureBox1.Invalidate();
            }
            //когда касса отработала кастомера она закрашивает его белым цветом
            if (e.State == Nikolaev.WorkSpace.CasseState.CustomerDone)
            {
                PaintNumber = e.CasseNumber;
                PaintState = e.State;
                PaintQueue = e.QueueLenth;
                for (int i = 1; i < PaintQueue; i++)
                {
                    g.DrawEllipse(Pens.Red, Width / 2 - i * 15, Height / 10 + 25 * PaintNumber, 15, 15);
                }
                for (int i = PaintQueue; i < 50; i++)
                {
                    g.DrawEllipse(Pens.White, Width / 2 - i * 15, Height / 10 + 25 * PaintNumber, 15, 15);
                }
            }
        }
        //кнопка по нажатию которой отрисовываюся кассы и запускается магазин
        private void button1_Click(object sender, EventArgs e)
        {
            FontFamily fontFamily = new FontFamily("Arial");
            var fontTest = new Font(fontFamily, 26, FontStyle.Regular, GraphicsUnit.Pixel);
            int Width = pictureBox1.Width;
            int Height = pictureBox1.Height;
            shop.ReadyForWork += new Nikolaev.WorkSpace.ReadyForStartCasse(shop_ReadyForWork);
            this.DoubleBuffered = true;
            shop.StartWork(timer1);
            var g = Graphics.FromImage(pictureBox1.Image);
            g.DrawString("BLACK FRIDAY", fontTest, Brushes.Black, Width / 4, Height / 200);
            //отрисовка касс черным
            for (int i = 0; i < ScrollBarRight.Maximum; i++)
            {
                g.FillRectangle(Brushes.Black, Width / 2, Height / 10 + 25 * i, 20, 20);
                // g.FillRectangle(Brushes.Green, Width / 2, Height / 10 + 25 * i, 40, 50);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            shop.ReadyForWork -= new Nikolaev.WorkSpace.ReadyForStartCasse(shop_ReadyForWork);
            Isclosing = true;          
            shop.CancelWork();

        }
    }
}