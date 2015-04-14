using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkersManagers
{
    class Factory
    {
        Random randomForWorkers = new Random();
        Random randomForTasks = new Random();
        Random randomForManagers = new Random();
        Random amountOfSubManagers = new Random();
        private int _amountOfWorkers;
        private int _amountOfTasks;
        private int _amountOfManagers;
        public Factory()
        {
            _amountOfWorkers = randomForWorkers.Next(1, 15);
            _amountOfTasks = randomForTasks.Next(1, 10);
            _amountOfManagers = randomForManagers.Next(1, 10);
            
        }
        public int AmountOfWorkers
        {
            
            get { return _amountOfWorkers; }
            
        }
        public int AmountOfTasks
        {

            get { return randomForTasks.Next(1, 10); }
            
            
        }
        public int AmountOfManagers
        {

            get { return _amountOfManagers; }
            
            
        }
        public void StartFactory()
        {   
            Secretary secretary = new Secretary();
            secretary.StartWork();
            Manager[] managers = new Manager[AmountOfManagers];
            for (int i =0 ;i<managers.Length;i++)
            {
                managers[i]= new Manager(i+1);
            }

            for(int i=0;i<AmountOfWorkers;i++)
            {
                Manager[] personalManagers= SubManagers(managers);


                Worker worker = new Worker(secretary, personalManagers, AmountOfTasks, i + 1);
                
                System.ComponentModel.BackgroundWorker background1 = new System.ComponentModel.BackgroundWorker();
                background1.DoWork += worker.StartWork;
                background1.RunWorkerCompleted +=worker.CompleteWork ; 
                background1.RunWorkerAsync(worker);
                  
           
            
            }
            System.Threading.Thread.Sleep(100);
            while (Worker.WorkCount > 0)
                System.Threading.Thread.Sleep(100);
            secretary.FinishWork();

        }
        private Manager[] SubManagers(Manager[] managers)
        {
           List <Manager>  submanagers= new List<Manager>();
           int subLength = amountOfSubManagers.Next(1, managers.Length);
           foreach (var manager in managers)
               submanagers.Add(manager);
            while (submanagers.Count > subLength)
                submanagers.RemoveAt(amountOfSubManagers.Next(0,submanagers.Count-1));
            return submanagers.ToArray();
        }

    }


}
