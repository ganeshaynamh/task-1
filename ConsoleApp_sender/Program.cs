using RabbitMQ.Client;
using Serilog;
using System;
using System.Text;

namespace ConsoleApp_sender
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Sending messsage!");
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                //using (StreamWriter writetext = new StreamWriter("Write.txt"))
                {
                    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null
                        );


                    Console.Write("Enter the message to sent : ");
                    string message = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    Log.Logger = new LoggerConfiguration().WriteTo.File("Console.text").CreateLogger();

                    Log.Information(message);
                    //writetext.WriteLine(message);
                    Console.WriteLine("write in text file");
                }
                Console.WriteLine(" Press [enter] to exit.");
               
                Console.ReadLine();
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
