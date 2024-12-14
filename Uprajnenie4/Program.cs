using System;
using System.Collections.Generic;
using System.Linq;

namespace Uprajnenie4
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatRoom chatRoom = new ChatRoom();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nChat Room Menu:");
                Console.WriteLine("1. Add Member");
                Console.WriteLine("2. Send Message");
                Console.WriteLine("3. Show Chat History");
                Console.WriteLine("4. Show Chat Statistics");
                Console.WriteLine("5. End Chat");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddMember(chatRoom);
                        break;

                    case "2":
                        SendMessage(chatRoom);
                        break;

                    case "3":
                        ShowChatHistory(chatRoom);
                        break;

                    case "4":
                        ShowStatistics(chatRoom);
                        break;

                    case "5":
                        chatRoom.EndChat();
                        Console.WriteLine("Chat ended.");
                        break;

                    case "6":
                        exit = true;
                        Console.WriteLine("Exiting the application.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddMember(ChatRoom chatRoom)
        {
            Console.Write("Enter the name of the member: ");
            string name = Console.ReadLine();
            Contact newContact = new Contact(name);
            chatRoom.AddMemberToTheChat(newContact);
            Console.WriteLine($"Member '{newContact.name}' added to the chat.");
        }

        static void SendMessage(ChatRoom chatRoom)
        {
            if (chatRoom.chatMembers == null || chatRoom.chatMembers.Count == 0)
            {
                Console.WriteLine("No members in the chat. Add members first.");
                return;
            }

            Console.Write("Enter the sender's name: ");
            string senderName = Console.ReadLine();
            Contact sender = chatRoom.chatMembers.Keys.FirstOrDefault(c => c.name == senderName);

            if (sender == null)
            {
                Console.WriteLine("Member not found. Make sure the member is added to the chat.");
                return;
            }

            Console.Write("Enter the message: ");
            string messageText = Console.ReadLine();

            Message message = new Message
            {
                sender = sender,
                message = messageText,
                timeSent = DateTime.Now
            };

            chatRoom.AddMessageToHistory(message, sender);
            Console.WriteLine("Message sent successfully.");
        }

        static void ShowChatHistory(ChatRoom chatRoom)
        {
            Console.WriteLine("Chat History:");
            foreach (var entry in chatRoom.MessageDeconstructor(string.Join("\n", chatRoom.history)))
            {
                Console.WriteLine(entry);
            }
        }

        static void ShowStatistics(ChatRoom chatRoom)
        {
            var (mostActiveUser, totalMessages, shortestMessage) = chatRoom.TuplesStatistic();

            Console.WriteLine("Chat Statistics:");
            Console.WriteLine($"Most active user: {mostActiveUser} with {totalMessages} messages sent.");
            Console.WriteLine($"Shortest message: {shortestMessage}");
        }
    }
}
