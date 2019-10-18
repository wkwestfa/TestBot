using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

class CommandHandler
{
    private DiscordSocketClient _client;
    private CommandService _service;

    public async Task InitializeAsync(DiscordSocketClient client)
    {
        _client = client;
        _service = new CommandService();

        await _service.AddModulesAsync(Assembly.GetEntryAssembly(), null);

        _client.MessageReceived += HandleCommandAsync;
    }

    private async Task HandleCommandAsync(SocketMessage socketMessage)
    {
        var msg = socketMessage as SocketUserMessage;

        if (msg == null)
            return;

        var context = new SocketCommandContext(_client, msg);
        int argPos = 0;

        if (msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
        {
            var result = await _service.ExecuteAsync(context, argPos, null);

            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            {
                Console.WriteLine(result.ErrorReason);
            }
        }
    }
}

