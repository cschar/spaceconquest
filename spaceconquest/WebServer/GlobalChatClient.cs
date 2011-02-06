using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace spaceconquest
{

    public struct ChatLog{
        public String playerName;
        public String message;
        public DateTime time;
    }

   public class GlobalChatClient
    {
        private List<ChatLog> chatLogs;
        private String httpSource;

        //Constructor
        public GlobalChatClient(String serverAddress ){
            chatLogs = new List<ChatLog>();
            httpSource = serverAddress;
        }


        public List<ChatLog> GetLogs()
        {
            UpdateLog();
            return this.chatLogs;
        }
        
        private void UpdateLog(){


            List<String> chatLogs = new List<string>();


            try
            {
                //HttpWebRequest chatReq =
                 //   (HttpWebRequest)WebRequest.Create(httpSource);
                HttpWebRequest chatReq =
                   (HttpWebRequest)WebRequest.Create(httpSource);
                HttpWebResponse chatResp = (HttpWebResponse)chatReq.GetResponse();

                Stream resStream = chatResp.GetResponseStream();


                //used on each read operation
                byte[] buf = new byte[10000];

                // to build entire output
                StringBuilder sb = new StringBuilder();

                string tempString = null;
                int count = 0;



                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buf, 0, buf.Length);

                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        tempString = Encoding.ASCII.GetString(buf, 0, count);

                        // continue building the string
                        sb.Append(tempString);
                    }
                }
                while (count > 0); // any more data to read?


                //Parse the stringBuffer into lists
                count = 0;
                StringBuilder sb2 = new StringBuilder();
                for (int i = 0; i < sb.Length; i++)
                {
                    if (sb[i] == '\n')
                    {
                        chatLogs.Add(sb2.ToString());
                        sb2.Clear();
                    }
                    else
                    {
                        sb2.Append(sb[i]);
                    }
                }
              
            }
            catch (WebException e)
            {
                throw e;
                //Console.WriteLine("WebException in GlobalChatClient-->Update");
            }

        foreach (String s in chatLogs)
        {
            Console.WriteLine(s);
        }
        
        
        }//end update method


       /// <summary>
       ///  Sends an HTTP GET Request to the web server whose root is @ httpSource
       ///  returns true if successful
       ///  otherwise false if error 
       /// </summary>
       /// <param name="name"></param>
       /// <param name="message"></param>
       /// <returns></returns>
        public Boolean SendMessage(string name, string message)
        {
            try{
                HttpWebRequest sendChatReq = (HttpWebRequest) WebRequest.Create(httpSource + "Submit?name="+ name +"&message=" + message);
                sendChatReq.GetResponse();
                return true;
            }catch( WebException e){
                Console.WriteLine("Error sending message in GlobalChatClient");
                return false;
            }
        }
   
   
   }//end globalChat client
}//end namespace
