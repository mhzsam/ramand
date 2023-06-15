// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data.Common;
using System.Text;
using System.Threading.Channels;

  IConnection _connection;
  IModel _channel;

    CreateConnection();
   
    Console.ReadKey();

  void CreateConnection()
{
    var factory = new ConnectionFactory
    {
        Uri = new Uri("amqp://guest:guest@localhost:5672/")
    };

    _connection = factory.CreateConnection();
    _channel = _connection.CreateModel();    
    _channel.QueueDeclare("test-consumed", true, false, false, null);
    _channel.ExchangeDeclare("dlx_exchange", ExchangeType.Direct);
    _channel.QueueBind("test", "dlx_exchange", "test-consumed", null);

    var consumer = new EventingBasicConsumer(_channel);
    consumer.Received += (sender, args) =>
    {
        var message = Encoding.UTF8.GetString(args.Body.ToArray());
        Console.WriteLine($" message: {message}");
      
            _channel.BasicAck(args.DeliveryTag, false);   
      
       
            _channel.BasicPublish("", "test-consumed", null, args.Body); 
        
    };

    
    _channel.BasicConsume("test", false, consumer);
}

 static bool ProcessMessage(string message)
{
    Console.WriteLine($"Processing message: {message}");    

    return true; 
}

 void CloseConnection()
{
    _channel.Close();
    _connection.Close();
}
