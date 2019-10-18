using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;


class Program
{
    private delegate bool EventHandler();

    private DiscordSocketClient _client;
    private CommandHandler _handler;

    static void Main(string[] args)
    => new Program().StartAsync().GetAwaiter().GetResult();

    public async Task StartAsync()
    {
        await Configure();

        await Task.Delay(-1);
    }

    private async Task OnClientReady()
    {
        await InviteManager.BootUp(_client);

        Console.WriteLine("Ready");

        _client.Log -= Log;
    }

    private async Task OnUserJoined(SocketGuildUser user)
    {
        await InviteManager.NewUserJoined(user);
    }

    private async Task OnUserUpdated(SocketUser userBefore, SocketUser userAfter)
    {
        await Bot.UpdateUserName(userBefore, userAfter);
    }

    private async Task Configure()
    {
        await ConfigureDiscord();
    }

    private async Task ConfigureDiscord()
    {
        if (Config.bot.token == "" || Config.bot.token == null)
            return;

        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Verbose
        });
        
        _client.Log += Log;
        _client.Ready += OnClientReady;
        _client.UserJoined += OnUserJoined;
        _client.UserUpdated += OnUserUpdated;
        await _client.StartAsync();
        await _client.LoginAsync(TokenType.Bot, Config.bot.token);

        _handler = new CommandHandler();
        await _handler.InitializeAsync(_client);
    }

    private async Task Log(LogMessage logMessage)
    {
        Console.WriteLine(logMessage.Message);
    }
}
