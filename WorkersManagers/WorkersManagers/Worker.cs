using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace WorkersManagers
{
    [SerializableAttribute]



    public class Worker
    {
        private Secretary _secretary;
        private Manager[]  _managers;
        private int _amountOfTasks;
        private int _number;
      public  static int WorkCount;


        public Worker(Secretary secretary, Manager[] managers, int amountOfTasks, int number)
        {
            _number = number;
            _secretary = secretary;

            _amountOfTasks = amountOfTasks;
            _managers = managers;

            

        }
        public void StartWork(object sender,System.ComponentModel.DoWorkEventArgs eventArgs )
        {
            System.Threading.Interlocked.Increment(ref WorkCount);
            Random time = new Random(DateTime.Now.Millisecond); 
            for (int i = 0; i < _amountOfTasks; i++)
            {
                int delay = time.Next(100, 1000);
                DateTime startTime = DateTime.Now;
                System.Threading.Thread.Sleep(delay);
                DateTime finishTime = DateTime.Now;
              

                Report report = new Report() 
                {
                    StartTime=startTime,FinishTime=finishTime, TaskNumber=i+1,WorkerNumber=_number,Managers=_managers
                };
                _secretary.GetReport(report);
            }
         
        }
        public void CompleteWork(object sender, System.ComponentModel.RunWorkerCompletedEventArgs eventArgs)
        {
            System.Threading.Interlocked.Decrement(ref WorkCount);

        }
    }

}

