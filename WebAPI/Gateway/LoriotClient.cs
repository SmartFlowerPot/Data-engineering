using System;
using WebAPI.Gateway.Service;
using WebSocketSharp;

namespace WebAPI.Gateway
{
    public class LoriotClient
    {
        private WebSocket _socket;
        private string Url = "wss://iotnet.cibicom.dk/app?token=vnoUBwAAABFpb3RuZXQuY2liaWNvbS5ka54Zx4fqYp5yzAQtnGzDDUw=";
        private ILoriotService _loriotService;
        
        
        // as we need to handle situation that we don't know exactly when they will happen,
        // so, to receive a msg from the server etc, we use event handlers: onClose, onMessage, onOpen...
        // those are event handlers that we can subscribe to.
        public LoriotClient()
        {
            _socket = new WebSocket(Url);
            _socket.OnOpen += OnOpen;
            _socket.OnMessage += OnMessage;
            _socket.OnError += OnError;
            _socket.OnClose += OnClose;

            _socket.Connect();
        }

        public void SendDownlinkMessage(String jsonTelegram)
        {
            _socket.Send(jsonTelegram);
        }

        private void OnOpen(object? sender, EventArgs e)
        {
            Console.WriteLine($"GATEWAY CONTROLLER => CONNECTION ESTABLISHED...");
        }

        
        // 2 params that the method receives comes from the onMessage event handler
        // then attach the method to the event
        // so, whenever there is a new message/incoming from the loriot, C# knows that it has to execute this function.
        // sender = who is sending the msg
        // MEA = all the arguments contained in that msg / e=event
        private static void OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
        }
        
        
        private void OnError(object? sender, ErrorEventArgs e)
        {
            Console.WriteLine("GATEWAY CONTROLLER => ERROR OCCURED: "+e.Message);
        }
        

        private void OnClose(object? sender, CloseEventArgs e)
        {
            Console.WriteLine("GATEWAY CONTROLLER => Connection closed: "+e.Code);
        }
    }
}