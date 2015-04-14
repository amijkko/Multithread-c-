using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;




namespace Logic
{

    public delegate void ReadyForStart(object sender, RoachEventArgs eventArgs);
    public class RoachEventArgs : EventArgs
    {
        
       //Параметры таракана
        public int RoachNumber;
        public int Position;
        public int EventType;
        public int AmountOfRunners;
        public RoachEventArgs(int roachNumber, int position, int eventType)
        {
            
            this.Position = position;
            this.EventType = eventType;
            this.RoachNumber = roachNumber;
        }
    }
    

    public class Paparoach
    {
        private string sourceProperty;
        public event EventHandler UnitChanged;
        [System.ComponentModel.Bindable(true)]
        public string SourceProperty
        {
            get { return sourceProperty; }
            set
            {
                if (value != sourceProperty)
                {
                    sourceProperty = value;
                    EventHandler handler = UnitChanged;
                    if (handler != null) handler(this, EventArgs.Empty);
                }
            }
        }
        public int Amount { get; set; }
     static   Random randomSpeed = new Random(DateTime.Now.Millisecond);
        private int _number;
        private int _distance;
        private int _startPosition;
        
        public bool StartReached;  
        public bool FinishReached;
        public bool Mustdie=true;
        public int Speed;
        public static ManualResetEvent IsReady = new ManualResetEvent(false);
        public event ReadyForStart ReadyForStart;
        Random time = new Random(DateTime.Now.Millisecond);

        

        public Paparoach(int number, int distance)
        {
            _number = number;
            _distance = distance;
            Random randomSpeed= new Random(DateTime.Now.Millisecond);
            Speed = randomSpeed.Next(10, 30);
        }
        public void StartRun(int initialPosition)
        {     
            _startPosition = initialPosition;
            System.Threading.Thread thread = new Thread(Race);
            thread.Start();
            

        }
        public void Race()
        {
            double position = -_startPosition;
            Mustdie = false;
                //проверка достигнут ли старт
            while (!StartReached && !Mustdie)
                {
                   
                        RaiseReadyToStart(new RoachEventArgs(_number, (int)position, 1));
                        Thread.Sleep(100);
                        position += Speed * 0.1;
                        StartReached = position >= 0;
                   
                  
                }
            if (Mustdie)
            {
                RaiseReadyToStart(new RoachEventArgs(_number, (int)position, 3));
                return;
            }
                //на старте обнуляем позицию
                position = 0.0;
                //ожидание завершения тредов
                RaiseReadyToStart(new RoachEventArgs(_number, (int)position, 0));
                IsReady.WaitOne();

                while (!FinishReached && !Mustdie)
                {
                    
                        //Движение  
                        RaiseReadyToStart(new RoachEventArgs(_number, (int)position, 1));
                        Thread.Sleep(100);
                        position += Speed * 0.1;
                        FinishReached = position >= _distance;
                    
                 
                }
                if (Mustdie)
                {
                    RaiseReadyToStart(new RoachEventArgs(_number, (int)position, 3));
                    return;
                }
                    RaiseReadyToStart(new RoachEventArgs(_number, _distance, 2));

               
                
           
        }

        public void SpeedChange()
        {     
            Speed = randomSpeed.Next(10, 35);
        }

        public void RaiseReadyToStart(RoachEventArgs args)
        {
            if (ReadyForStart != null)
            {
                ReadyForStart(this, args);
            }

        }
        private static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine(name + " starts and calls IsReady.WaitOne()");

            IsReady.WaitOne();

            Console.WriteLine(name + " ends.");
        }
       
        }
    }



