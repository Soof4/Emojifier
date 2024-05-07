using TShockAPI;
using Newtonsoft.Json;

namespace Emojifier
{
    public class Configuration
    {
        public static string ConfigPath = Path.Combine(TShock.SavePath, "EmojifierConfig.json");
        public Dictionary<string, int> Emojis = new() { { "skull", 193 }, { "heart", 29 } };

        public static Configuration Reload()
        {
            Configuration? c = null;

            if (File.Exists(ConfigPath))
            {
                c = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(ConfigPath));
            }

            if (c == null)
            {
                c = new Configuration();
                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(c, Formatting.Indented));
            }

            return c;
        }
    }
}
