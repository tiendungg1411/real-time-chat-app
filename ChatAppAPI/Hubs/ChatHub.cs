﻿using ChatAppAPI.DataService;
using ChatAppAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppAPI.Hubs
{
    public class ChatHub : Hub
    {

        private readonly ShareDb _shared;
        public ChatHub(ShareDb shared)
        {
             _shared = shared;
        }
        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage", "admin", $"{conn.UserName} has joined");
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
            _shared.connection[Context.ConnectionId] = conn;
            await Clients.Group(conn.ChatRoom)
                .SendAsync("JoinSpecificChatRoom", "admin", $"{conn.UserName} has joined {conn.ChatRoom}");
        }

        public async Task SendMessage(string msg)
        {
            if(_shared.connection.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                await Clients.Group(conn.ChatRoom).SendAsync("ReceiveSpecificMessage", conn.UserName, msg);
            }
        }
    }
}
