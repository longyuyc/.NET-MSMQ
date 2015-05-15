using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MSMQDemo
{
    public class MSMQ
    {
        private string Path;
        public void CreateQueue(string queuePath)
        {
            try
            {
                if(MessageQueue.Exists(queuePath))
                {
                    Console.WriteLine(queuePath+"已经存在");
                }
                else
                {
                    MessageQueue.Create(queuePath);
                }
                Path = queuePath;
            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //发送消息
        public void SendMessage()
        {
            //连接本地队列
            try
            {
                MessageQueue myQueue = new MessageQueue(Path);
                Message myMessage = new Message();
                myMessage.Body = "消息内容 fuck you";
                myMessage.Formatter = new XmlMessageFormatter(new Type[]{typeof(string)});
                //发送消息
                myQueue.Send(myMessage);
                Console.WriteLine("消息发送成功");
                Console.ReadLine();
            }catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ReceiveMessage()
        {
            MessageQueue myQueue = new MessageQueue();
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            try
            {
                //从队列中接收消息
                Message myMessage = myQueue.Receive();
                string context = myMessage.Body.ToString();
                Console.WriteLine("消息内容:"+context);
                Console.ReadLine();
            }catch(MessageQueueException e){
                Console.WriteLine(e.Message);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //清空制定队列的消息
        public void ClearMessage()
        {
            MessageQueue myQueue = new MessageQueue(Path);
            myQueue.Purge();
            Console.WriteLine("已清空了{0}上的所有消息",Path);
        }
        //连接队列冰获取队列的全部消息
        public void GetAllMessage()
        {
            MessageQueue myQueue = new MessageQueue(Path);
            Message[] allMessage = myQueue.GetAllMessages();
            XmlMessageFormatter formatter = new XmlMessageFormatter(new Type[]{typeof(string)});
            for (int i = 0; i < allMessage.Length; i++)
            {
                allMessage[i].Formatter = formatter;
                Console.WriteLine("第{0}机密消息为:{1}", i + 1, allMessage[i].Body.ToString());
            }
            Console.ReadLine();
        }
    }
}
