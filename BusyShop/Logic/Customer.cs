using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nikolaev.WorkSpace
{
    #region EventMembers
    public delegate void ReadyForStart(object sender, RoachEventArgs eventArgs);
    public class RoachEventArgs : EventArgs
    {
        public int CustomerNumber;
        public int Position;
        public CustomerState State;
        public int AmountOfCustomers;

        public RoachEventArgs(int customerNumber, CustomerState state)
        {
            this.State = state;
            this.CustomerNumber = customerNumber;
        }
    }
    public enum CustomerState
    {
        InQueue,
        Finished,
    }
    #endregion EventMembers
    public class Customer
    {
        #region Members
        public event ReadyForStart ReadyForShopping;
        public bool ShoppingDone;
        private int _number;
        Random Buying = new Random();
        #endregion Members
        public Customer()
        {

        }
        //не используется
        public void DoStaff()
        {
            //int BuyingGoods = Buying.Next(100,1000);
            while (!ShoppingDone)
            {
                RaiseReadyToStart(new RoachEventArgs(_number, CustomerState.InQueue));
            }
            RaiseReadyToStart(new RoachEventArgs(_number, CustomerState.Finished));
        }
        public void RaiseReadyToStart(RoachEventArgs args)
        {
            if (ReadyForShopping != null)
            {
                ReadyForShopping(this, args);
            }
        }
    }
}
