using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uprajnenie4
{
    public class ChatRoom
    {
        public int members { get; set; }
        
        public Dictionary<Contact, int> chatMembers { get; set; }

        //counter of message, used as identificator in the dictionary
        public int messageDeliveredBetweenInstances { get; set; }
        
        //History for storing in Database
        public List<string> history { get; set; }

        //Dictionary to be stored all messages in the current session
        //Key - ID (int) Value - class Message
        private Dictionary<int, Message> chatMessages { get; set; }

        public ChatRoom()
        {
            chatMessages = new Dictionary<int, Message>();
            history = new List<string>();
            chatMembers = new Dictionary<Contact, int>(); // Fix: Initialize chatMembers
            history.Add($"Chat is opened at : {DateTime.Now}");
            messageDeliveredBetweenInstances = 0;
        }


        public void AddMemberToTheChat(Contact contact)
        {
            //check if contact is not already in the chatRoom
            if (!chatMembers.Keys.Contains(contact))
            {
                //Adding new contact to the chat, 
                chatMembers.Add(contact, 0);
            }
        }

        public void AddMessageToHistory(Message message, Contact contact)
        {
            //increment the totalMessage which are sent by the contact
            contact.totalMessageSent++;
            //using string interpolation
            history.Add($"Message: {message.message} | Created By: {contact.name} | DateTime: {DateTime.Now}");
            messageDeliveredBetweenInstances++;
        }

        public void EndChat()
        {
            history.Add($"Chat is stopped at: {DateTime.Now}");
        }

        //Deconstructor for 2nd task 
        //Да се напише метод, който деконструира данните за съобщението. Дайте различни примера за
        //деконструиране по вид и брой извлечени отделни полета.Направете например деконструктор,
        //който извлича поотделно датата и часа за съобщението.
        public string[] MessageDeconstructor(string message)
        {
            string[] strings = message.Split(" | ");
            List<string> separatedStrings = new List<string>();
            foreach(string str in strings)
            {
                //get index of the ':' symbol
                int index = str.IndexOf(':'); 

                //Ensuring that index exists
                if(index != -1)
                {
                    //2 = 1 (index + 1) + 1 (for the additional space after the character ':') 
                    string result = str.Substring(index + 2);
                    separatedStrings.Add(result);
                }
            }

            return separatedStrings.ToArray();
        }


        public (string, int, string) TuplesStatistic()
        {
            //Additional approach for faster receiving the user with the most sent
            //message, can be made with the usage of Stack, to be track in live time
            //who is the current user with the most send message in method AddMessageToHistory()

            // using the builtIn method Max() for getting the biggest number in 
            //sequence of numbers
            int max = chatMembers.Values.Max();
            //receiving key in the dictionary based on the value
            Contact key = chatMembers.FirstOrDefault(x => x.Value == max).Key;

            //variable for track for the shortest message
            int minLength = int.MaxValue;
            string shortestMessage = String.Empty;

            for(int x = 0; x < history.Count; x++)
            {
                string[] strings = MessageDeconstructor(history[x]);
                if(minLength < strings[0].Length)
                {
                    minLength = strings[0].Length;
                    shortestMessage = strings[0];
                }
            }


            return (key.name, key.totalMessageSent, shortestMessage);
        }
    }
}
