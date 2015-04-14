using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Forms;


namespace Nikolaev.WorkSpace
{
    public class Shop
    {

        #region Properties
        public int CustomersPerMinute { get; set; }
        public int AmountOfCustomers { get; set; }
        public int AmountOfCasses { get; set; }
        #endregion Properties
        #region Members
        public bool NeedNewCasse;
        public int ReleaseCount;
        int semaphoreCount = 8;
        Nikolaev.WorkSpace.Customer customer = new Nikolaev.WorkSpace.Customer();
        public Semaphore semaphore;
        public event ReadyForStartCasse ReadyForWork;     
        List<Casse> Casses = new List<Casse>();
        List<Customer> Cassa = new List<Customer>();
        public List<Customer> Customers = new List<Customer>();
        #endregion Members


        public void StartWork(System.Windows.Forms.Timer customerTimer)
        {
            //запускаем симафор зависящий от изменения ползунка
            semaphore = new Semaphore(1, semaphoreCount);
            // запускаем одну постоянно работующую кассу
            Nikolaev.WorkSpace.Casse casse = new Nikolaev.WorkSpace.Casse(true);
            casse.ReadyForWork += new ReadyForStartCasse(CasseReady);
            Casses.Add(casse);
            casse.StartWork(semaphore);
            //запускаем кассы
            for (int i = 1; i < semaphoreCount; i++)
            {
                casse = new Nikolaev.WorkSpace.Casse(i);
                Casses.Add(casse);
                casse.ReadyForWork += new ReadyForStartCasse(CasseReady);
                casse.StartWork(semaphore);
                System.Diagnostics.Debug.WriteLine("thread started ={0}", i);
            }
            //устанавливаем таймер зависящий от левого ползунка
            customerTimer.Interval = 600 / (AmountOfCustomers + 1);
            customerTimer.Tick += new EventHandler(TimerEventProcessor);
            customerTimer.Start();

        }
        private void TimerEventProcessor(Object myObject,
                                          EventArgs myEventArgs)
        {
            //добавляем по таймеру покупателя
            Customer customerByTimer = new Customer();
            this.AddingCustomers(customerByTimer);               
        }
        
        private void CasseReady(object sender, Nikolaev.WorkSpace.CasseEventArgs eventArgs)
        {
            CasseReadyToWork(eventArgs);
        }

        public void CasseReadyToWork(CasseEventArgs args)
        {
            if (ReadyForWork != null)
            {
                ReadyForWork(this, args);
            }
        }

        public void AddingCustomers(Customer customer)
        {
            //ищем минимальное значение длины очереди среди касс,после чего присваиваем ее кассефри
            int minValue = int.MaxValue;
            Casse cassaFree = null;
            foreach (var v in Casses)
            {
                if (!v.IsWorking)
                    continue;
                if (v.cq.Count < minValue)
                {
                    minValue = v.cq.Count;
                    cassaFree = v;
                }
            }
            //добавлям покупателя в эту кассу
            cassaFree.Enqueue(customer);
            System.Diagnostics.Debug.WriteLine("Customer added To casseNumber ={0} case count = {1} is busy {2}", cassaFree.CasseNumber, cassaFree.cq.Count, cassaFree.IsBusy);
        }

        public int WorkingCasses()
        {
            int count = 0;
            //пробегаемся по всем кассам и ищем рабочие
            foreach (var v in Casses)
            {
                if (v.IsWorking)
                {
                    count++;
                }
            }
            return count;
        }
        public void CancelWork()
        { 
        foreach(var v in Casses)
        {
            if (v != null)
            {  

                v.CancelWork();
                
            }
        }
        }

        #region Unusable
        public void OpenNewCasse(Casse casse)
        {
            var dep = Casses.First().IsNotWorking;
        }
        #endregion Unusable
    }
}

