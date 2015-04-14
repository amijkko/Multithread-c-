using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace WorkersManagers
{
   public  class Secretary
    {
        private int _amountOfTasks;
        private Manager[] _managers;
      public   bool IsExit;
        System.Collections.Concurrent.ConcurrentQueue<Report> cq = new ConcurrentQueue<Report>();
        System.Threading.Thread thread;

        public void StartWork()
        {
            thread = new System.Threading.Thread(WorkerThread);
            thread.Start();
        }

        private void WorkerThread()
        {
            Report report;
            while (!IsExit)
            {
                while (cq.TryDequeue(out report))
                {
                    foreach (var manager in report.Managers)
                    {
                        manager.GetReport(report);
                    }

                }
                System.Threading.Thread.Sleep(10);
            }
        }
        public void FinishWork()
        {
            IsExit = true;
            thread.Join();
        }

        public void GetReport(Report report)
        {
            cq.Enqueue(report);
        }

    }
    
}
