using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkersManagers
{
   public  class Report
    {
        public int WorkerNumber { get; set; }
        public int TaskNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public Manager[] Managers { get; set; }
        public Secretary Secretary { get; set; }
    }
}
