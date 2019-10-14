using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class EmbedHelper
{
    public static EmbedBuilder CreateEmbedMessage(string title, string description, Color color)
    {
        var embed = new EmbedBuilder();

        embed.WithTitle(title);
        embed.WithDescription(description);
        embed.WithColor(color);

        return embed;
    }

    public static EmbedBuilder CreateEmbedMessage(string title, IEnumerable<string> lines, Color color)
    {
        var embed = new EmbedBuilder();

        embed.WithTitle(title);

        foreach (var item in lines)
        {
            embed.Description += "[" + item + "](https://www.wikipedia.org/)" + Environment.NewLine;
        }

        embed.WithColor(color);

        return embed;
    }

}
