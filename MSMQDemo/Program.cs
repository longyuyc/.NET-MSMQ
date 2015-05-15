using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//引入命名空间
using System.Messaging;

namespace MSMQDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            MSMQ queue = new MSMQ();
            queue.CreateQueue(@".\private$\myQueue");
            queue.SendMessage();
            queue.GetAllMessage();
            //queue.ReceiveMessage();
            //queue.ClealMessage();
        }
    }
}
