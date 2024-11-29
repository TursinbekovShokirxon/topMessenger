using Domain;
using Domain.Models;
using Infrastructure.data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Mesenger.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string userName, string message, string time);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly MessengerContext _db;
        public ChatHub(MessengerContext db)
        {
            _db = db;
        }
        public async Task JoinChat(UserConnection connection)
        {
            try
            {
                var time = DateTime.Now.ToString("HH:mm:ss");
                // Добавляем пользователя в группу
                await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

                // Уведомляем участников группы о новом пользователе
                await Clients.Group(connection.ChatRoom).
                    ReceiveMessage("Admin", $"{connection.UserName} присоединился к чату.", time);

                var messages =await _db.Messages.Where(m => m.ChatRoom == connection.ChatRoom).ToListAsync();
                foreach (var message in messages)
                {
                    await Clients.Caller.ReceiveMessage(message.UserName, message.Message, message.TimeStamp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task SendMessage(string chatRoom, string userName, string message)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            var newMessage = new ChatMessage()
            {
                Message = message,
                UserName = userName,
                ChatRoom = chatRoom,
                TimeStamp = time
            };
            _db.Messages.Add(newMessage);
            await _db.SaveChangesAsync();
            // Отправляем сообщение всем в группе
            await Clients.Group(chatRoom).ReceiveMessage(userName, message, time);
        }
        #region MyRegion
        /*private string SaveAvatar(string base64Avatar)
        {
            if (string.IsNullOrEmpty(base64Avatar))
                return @"C:\path\to\default-avatar.jpg";

            var fileName = Guid.NewGuid().ToString() + ".jpg"; // Use a unique name for the avatar file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatars", fileName);
            byte[] bytes = Convert.FromBase64String(base64Avatar);
            File.WriteAllBytes(filePath, bytes);

            return $"/avatars/{fileName}"; // Return relative path to the image
        }*/
        #endregion
    }
}
