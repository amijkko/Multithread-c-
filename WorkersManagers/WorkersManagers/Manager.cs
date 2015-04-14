using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkersManagers
{
  public   class Manager
    {
        Random time = new Random(DateTime.Now.Millisecond);
        public int Number { get; set; }

        public Manager(int number)
        {
            Number = number;
             string fileName = "test_"+Number+".txt";
             using (System.IO.FileStream fs = System.IO.File.Create(fileName, 1024)) ;
           
        }
        public void GetReport(Report report)
        {
            int latency = time.Next(10, 10000);
            System.Threading.Thread.Sleep(latency);
            string fileName = "test_"+Number+".txt";
             using (System.IO.FileStream fs = System.IO.File.OpenWrite(fileName)) 
             {
                 string line= "ManagerNubmer: "+Number +", WorkerNumber: " +report.WorkerNumber  +", Starting Time: " + report.StartTime +"FinishTime: " + report.FinishTime;
                 System.IO.TextWriter writer = new System.IO.StreamWriter(fs);
                 writer.WriteLine(line);
             }
          
        }
    }
}

