using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Data.Enums
{
     public static class TaskStatus
     {
        public enum status
        {
            //[Description("WAITING FOR SUPPORT = 0,")]   //when i want space in between my words
            WAITINGFORSUPPORT = 0,
            INPROGRESS = 1,
            ESCALATE  =2,
            PENDING=3,
            CANCELREQUEST=4,
            RESOLVED=5
        }
     }
}
