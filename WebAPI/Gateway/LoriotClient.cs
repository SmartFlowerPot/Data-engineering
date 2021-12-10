#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Gateway.Model;
using WebAPI.Gateway.Service;
using WebAPI.Models;
using WebSocketSharp;

namespace WebAPI.Gateway
{
    public sealed class LoriotClient
    {
        private static readonly Lazy<LoriotClient> lazy =
            new(() => new LoriotClient());
        
        private WebSocket _socket;

        private string Url =
            "wss://iotnet.cibicom.dk/app?token=vnoUBwAAABFpb3RuZXQuY2liaWNvbS5ka54Zx4fqYp5yzAQtnGzDDUw=";

        private ILoriotService _loriotService;
        
        public static LoriotClient Instance => lazy.Value;

        // as we need to handle situation that we don't know exactly when they will happen,
        // so, to receive a msg from the server etc, we use event handlers: onClose, onMessage, onOpen...
        // those are event handlers that we can subscribe to.
        private LoriotClient()
        {
            _loriotService = new LoriotService();
            _socket = new WebSocket(Url);
            _socket.OnOpen += OnOpen;
            _socket.OnMessage += OnMessage;
            _socket.OnError += OnError;
            _socket.OnClose += OnClose;
            _socket.Connect();
        }
        
        public void SendDownLinkMessage(WindowController.WindowControl jsonTelegram)
        {
            string data = jsonTelegram.OpenedClosed
                ? "01"
                : "00";

            var message = new DownLinkMessage()
            {
                cmd = "tx",
                EUI = jsonTelegram.EUI,
                port = 1,
                confirmed = true,
                data = data
            };
            string json = JsonSerializer.Serialize(message);
            Console.WriteLine(json);
            _socket.Send(json);
        }

        public void GetCacheReadings()
        {
            var message = new Message
            {
                cmd = "cq"
            };
            var json = JsonSerializer.Serialize(message);
            _socket.Send(json);
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
        private void OnMessage(object? sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            var message = JsonSerializer.Deserialize<IoTMessage>(e.Data);
            Console.WriteLine($"GATEWAY CONTROLLER => {message}");
            _loriotService.HandleMessage(message);
        }

        private void OnError(object? sender, ErrorEventArgs e)
        {
            Console.WriteLine("GATEWAY CONTROLLER => ERROR OCCURED: " + e.Message);
        }

        private void OnClose(object? sender, CloseEventArgs e)
        {
            Console.WriteLine("GATEWAY CONTROLLER => Connection closed: " + e.Code);
        }
    }
}