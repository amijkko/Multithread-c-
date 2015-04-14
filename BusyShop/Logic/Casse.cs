using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace Nikolaev.WorkSpace
{
    #region EventMembers
    public delegate void ReadyForStartCasse(object sender, CasseEventArgs eventArgs);
    public class CasseEventArgs : EventArgs
    {
        public int CasseNumber;
        public CasseState State;
        public int QueueLenth;

        public CasseEventArgs(int casseNumber, CasseState state, int queueLenth)
        {
            this.QueueLenth = queueLenth;
            this.State = state;
            this.CasseNumber = casseNumber;
        }
    }

    public enum CasseState
    {
        Working,
        IsBusy,
        CustomerDone,
        DoneWorking,
        Adding,
    }
    #endregion EventMembers
    public class Casse
    {
        #region Properties
        public bool IsWorking { get; private set; }
        public int CasseNumber { get; private set; }
        public int QueueLenth { get; private set; }
        public int Capacity { get { return GetQueueLenth(); } }
        public bool IsBusy { get; private set; }
        #endregion Properties
        #region Members
        public event ReadyForStartCasse ReadyForWork;
        private ManualResetEvent event_1 = new ManualResetEvent(false);
        public bool ShoppingDone;
        public bool ShouldWork;
        public System.Collections.Concurrent.ConcurrentQueue<Nikolaev.WorkSpace.Customer> cq = new ConcurrentQueue<Nikolaev.WorkSpace.Customer>();
        Thread thread = null;
        public int Amount;
        private bool callbackInvoked;
        Random RandomLatency = new Random();
        public bool IsNotWorking;
        public bool IsClosed;
        #endregion Members
        public Casse(int number)
        {
            CasseNumber = number;
        }
        public Casse(bool isWorking)
        {

            ShouldWork = isWorking;
        }
        public void StartWork(Semaphore semaphore)
        {
            thread = new Thread(ThreadProc);
            thread.Start(semaphore);
          
        }
        //определяем длину очереди
        public int GetQueueLenth()
        {
            return cq.Count;
        }
        #region ThreadProc
        private void ThreadProc(object o)
        {
            Semaphore semaphore = o as Semaphore;

            WaitHandle[] waitHandles = new WaitHandle[] { semaphore, event_1 };
            while (true)
            {
                System.Threading.WaitHandle.WaitAny(waitHandles);
                if(event_1.WaitOne(0))
                {
                  
                return;
                }
                DateTime time = DateTime.Now;
                Customer customer;
                IsWorking = true;
                IsNotWorking = false;
                CasseReadyToWork(new CasseEventArgs(CasseNumber, CasseState.Working, QueueLenth));
                //касса работает пока длина очереди больше нуля,или она определена как работающая
                while (cq.Count > 0 || ShouldWork || (DateTime.Now - time).TotalSeconds < 10)
                {
                    if (cq.Count == 0)
                    {
                        if (IsClosed)
                            break;
                        if (event_1.WaitOne(100))
                        {
                            return;
                        }
                        continue;
                    }
                    var currentBusy = cq.Count > 5;
                    if (IsBusy != currentBusy)
                    {
                        if (currentBusy)
                        {
                            CasseReadyToWork(new CasseEventArgs(CasseNumber, CasseState.IsBusy, QueueLenth));
                            QueueLenth = cq.Count;
                        }
                        else
                        {
                            CasseReadyToWork(new CasseEventArgs(CasseNumber, CasseState.Working, QueueLenth));
                            QueueLenth = cq.Count;
                        }
                        IsBusy = currentBusy;
                    }
                    cq.TryDequeue(out customer);
                    if (event_1.WaitOne(RandomLatency.Next(500,1000)))
                    {
                        return;
                    }
                    CasseReadyToWork(new CasseEventArgs(CasseNumber, CasseState.CustomerDone, QueueLenth));
                    QueueLenth = cq.Count;
                }
                IsWorking = false;
                CasseReadyToWork(new CasseEventArgs(CasseNumber, CasseState.DoneWorking, QueueLenth));
                QueueLenth = cq.Count;
            }
        }
        #endregion ThreadProc
        //добавление в очередь кастомера
        public void Enqueue(Customer customer)
        {
            cq.Enqueue(customer);
            CasseReadyToWork(new CasseEventArgs(CasseNumber, CasseState.Adding, QueueLenth));
        }
        public void CasseReadyToWork(CasseEventArgs args)
        {
            if (ReadyForWork != null)
            {
                ReadyForWork(this, args);
            }

        }
        public void CancelWork()
        {
            
            event_1.Set();
            callbackInvoked=true;
            thread.Join();
            
        }
    }
}

