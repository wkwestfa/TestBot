using Newtonsoft.Json;
using System.IO;

public struct BotConfig
{
    public string token;
    public string cmdPrefix;
    public string channelName;
    public string username;
    public string twitchToken;
    public string twitchRefreshToken;
    public string clientId;
}

class Config
{
    private const string configFolder = "Reso";
    private const string configFile = "config.json";

    public static BotConfig bot;

    static Config()
    {
        string configLocation = $"{configFolder}/{configFile}";

        if (!Directory.Exists(configFolder))
        {
            Directory.CreateDirectory(configFolder);
        }

        if (!File.Exists(configLocation))
        {
            bot = new BotConfig();

            string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
            File.WriteAllText(configLocation, json);
        }
        else
        {
            string json = File.ReadAllText(configLocation);
            bot = JsonConvert.DeserializeObject<BotConfig>(json); //TODO: Look into this for user - Very easy way to grab class data that looks a lot less cluttered.
        }
    }
}