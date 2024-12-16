using System.Net.WebSockets;
using System.Text;

namespace your_bike_user_backend.Handler
{
    public class NotificationWebSocketHandler
    {
        private static readonly List<WebSocket> Clients = new();

        public static async Task HandleConnection(WebSocket webSocket)
        {
            Clients.Add(webSocket);

            while (webSocket.State == WebSocketState.Open)
            {
                var buffer = new byte[1024 * 4];
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Clients.Remove(webSocket);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                }
            }
        }

        public static async Task BroadcastMessage(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);

            foreach (var client in Clients)
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
