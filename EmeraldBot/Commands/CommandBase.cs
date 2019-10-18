using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


public class CommandBase : ModuleBase<SocketCommandContext>
{
    public EmbedBuilder CreateEmbedMessage(string title, string description, Color color)
    {
        var embed = new EmbedBuilder();

        embed.WithTitle(title);
        embed.WithDescription(description);
        embed.WithColor(color);

        return embed;
    }

    public EmbedBuilder CreateEmbedMessage(string title, string description)
    {
        var embed = new EmbedBuilder();

        embed.WithTitle(title);
        embed.WithDescription(description);

        return embed;
    }

    public EmbedBuilder CreateEmbedMessage(string title, IEnumerable<string> lines, Color color)
    {
        var embed = new EmbedBuilder();

        embed.WithTitle(title);

        foreach (var item in lines)
        {
            embed.Description += $"{item}" + Environment.NewLine;
        }

        embed.WithColor(color);

        return embed;
    }

    public async Task SendMessage(string message)
    {
        await Context.Channel.SendMessageAsync(message);
    }

    public async Task SendMessage(string title, IEnumerable<string> lines, Color color)
    {
        var embedMessage = CreateEmbedMessage($"**{title}**", lines, color);

        await Context.Channel.SendMessageAsync("", false, embedMessage.Build());
    }

    public async Task SendMessage(string title, string description, string footer, Color color)
    {
        var embedMessage = CreateEmbedMessage($"**{title}**", description, color);
        embedMessage.WithFooter(footer);

        await Context.Channel.SendMessageAsync("", false, embedMessage.Build());
    }

    public async Task SendMessage(string title, string description, Color color)
    {
        var embedMessage = CreateEmbedMessage($"**{title}**", description, color);

        await Context.Channel.SendMessageAsync("", false, embedMessage.Build());
    }

    public async Task SendMessage(string title, string description)
    {
        var embedMessage = CreateEmbedMessage($"**{title}**", description);

        await Context.Channel.SendMessageAsync("", false, embedMessage.Build());

    }

    public async Task SendMessage(EmbedBuilder embedMessage)
    {
        await Context.Channel.SendMessageAsync("", false, embedMessage.Build());
    }

    public async Task Wait(int seconds)
    {
        int milliseconds = seconds * 1000;

        await Task.Delay(milliseconds);
    }
}
