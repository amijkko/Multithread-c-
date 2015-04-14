using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Nikolaev.WorkSpace
{

    public partial class Roachs : Form
    {
        private int amount;




        public int Amount
        {
            get { return amount; }
            set
            {
                if (value != null && value != amount)
                {
                    amount = value;

                }
            }
        }

        public int Distance;
        public Brush SolidBlack = new SolidBrush(Color.Black);
        Color customColor = Color.FromArgb(150, Color.Gray);
        public bool DeadRoach;
        public int AmountOfRunners = 5;
        public int RunnersPassed;
        public int[] RoachPosition;
        delegate void SetTextCallback(string text);
        List<Logic.Paparoach> RoachList = new List<Logic.Paparoach>();
        public Roachs()
        {
            InitializeComponent();
            textBoxForNumbers.Text = "5";

            this.Show();
        }

        public int ReadNumber()
        {
            //проверка ввода 
            int result;
            if (!int.TryParse(textBoxForNumbers.Text, out result))
            {
                MessageBox.Show("Wrong Initializer");
                return -1;
            }

            return result;
        }
        private void StartClick(object sender, EventArgs e)
        {
            RaceTrack.Invalidate();
            AmountOfRunners = ReadNumber();
            if (AmountOfRunners < 1)
            {
                return;
            }
            RoachPosition = new int[AmountOfRunners];
            int trackWidth = this.RaceTrack.Width;
            int trackHeight = this.RaceTrack.Height;
            this.RunnersPassed = AmountOfRunners;
            for (int i = 0; i < AmountOfRunners; i++)
            {
                this.DoubleBuffered = true;
                Logic.Paparoach paparoach = new Logic.Paparoach(i, (int)(trackWidth * 0.8));
                RoachList.Add(paparoach);
                paparoach.ReadyForStart += new Logic.ReadyForStart(RoachReady);

                paparoach.StartRun((int)(trackWidth * 0.1));

            }
        }


        private void RoachReady(object sender, Logic.RoachEventArgs eventArgs)
        {
            int oldPosition;
            lock (RoachPosition)
            {
                oldPosition = RoachPosition[eventArgs.RoachNumber];
            }

            //проверка на готовность на старте
            if (eventArgs.EventType == 0)
            {
                Interlocked.Decrement(ref RunnersPassed);
                if (RunnersPassed == 0)
                {
                    Logic.Paparoach.IsReady.Set();

                }

                return;
            }//движение таракана
            else if (eventArgs.EventType == 1)
            {
                lock (RoachPosition)
                {

                    RoachPosition[eventArgs.RoachNumber] = eventArgs.Position;

                }


            }//финиш
            else if (eventArgs.EventType == 2)
            {


                eventArgs.RoachNumber += 1;
                SetText("runner number:" + eventArgs.RoachNumber + "\r  finished");

            }
            else if (eventArgs.EventType == 3)
            {

                Interlocked.Decrement(ref RunnersPassed);
                eventArgs.RoachNumber += 1;
                SetText("runner number:" + eventArgs.RoachNumber + "\r  has been killed");

            }

            RaceTrack.Invalidate(RoachDraw(eventArgs.RoachNumber, oldPosition));
            RaceTrack.Invalidate(RoachDraw(eventArgs.RoachNumber, eventArgs.Position));

        }
        //установка текса в текстбокс
        public void SetText(string text)
        {

            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                textBox1.AppendText(text + System.Environment.NewLine);

            }
        }
        //Основная функция рисования
        private void RaceTrack_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                int trackWidth = this.RaceTrack.Width;
                int trackHeight = this.RaceTrack.Height;

                SolidBrush shadowBrush = new SolidBrush(customColor);     
                //Линии Старта и Финиша
                e.Graphics.DrawLine(Pens.Gray, (int)(trackWidth * 0.1), 0, (int)(trackWidth * 0.1), trackHeight);
                e.Graphics.DrawLine(Pens.Gray, (int)(trackWidth * 0.9), 0, (int)(trackWidth * 0.9), trackHeight);
                //Надписи старт и стоп
                e.Graphics.DrawString("Start", System.Drawing.SystemFonts.DefaultFont, Brushes.Black, (float)(trackWidth * 0.1), (float)0 + 20);
                e.Graphics.DrawString("Finish", System.Drawing.SystemFonts.DefaultFont, Brushes.Black, (float)(trackWidth * 0.8), (float)0 + 20);
                Image roach = Image.FromFile(@"C:\Users\User\Downloads\paparoach.jpg");
                Image deadroach = Image.FromFile(@"C:\Users\User\Downloads\deadone.jpg");
                Bitmap roach1 = new Bitmap(roach, new Size(40, 30));
                Bitmap deadroach1 = new Bitmap(deadroach, new Size(50, 30));
                for (int i = 0; i < AmountOfRunners; i++)
                {      //Отрисовка линий,где у1 переменная определяющая их зависимость 
                    int y1 = (trackHeight / (AmountOfRunners + 1)) * (i + 1);
                    e.Graphics.DrawLine(Pens.Green, 0, y1, trackWidth, y1);

                    //Рисование тараканов
                    if (RoachPosition != null)
                    {
                        int position;
                        lock (RoachPosition)
                            position = RoachPosition[i];
                         Point roachPosition = new Point((int)(trackWidth * 0.1) + position, y1);
                        e.Graphics.DrawImage(roach1, roachPosition);

                        if (RoachList[i].Mustdie == true)
                       {
                         e.Graphics.DrawImage(deadroach1, roachPosition);
                         }
                    }
                 }
                return;    
                }
               
       

       
            catch (Exception ex)
            {
            }
        }

        public Rectangle RoachDraw(int number, int position)
        {

            int y1 = (RaceTrack.Height / (AmountOfRunners + 1)) * (number + 1);
            int x1 = (int)(RaceTrack.Width * 0.1) + position;
            Point roachPoint = new Point(x1, y1);
            return new Rectangle(x1, y1, 30, 40);

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            foreach (var i in RoachList)
            {
                i.SpeedChange();
            }
        }

        private void DeathClick(object sender, MouseEventArgs e)
        {
     //Получаем координаты курсора
         for (var i=0;i<RoachPosition.Length;i++)
            {
                var rect = RoachDraw(i, RoachPosition[i]);
                if (e.X > rect.X && e.X < rect.X + rect.Width && e.Y > rect.Y && e.Y < rect.Y + rect.Height)
                {
                    RoachList[i].Mustdie = true;
                    return;
                }
             
            } 
        }
    }
}
