using Newtonsoft.Json;

namespace Emojifier {
    public class Config {
        public Dictionary<string, int> Emojis = new() { { "skull", 193 }, {"heart", 29} };
        
        public void Write() {
            File.WriteAllText(Emojifier.configPath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        public static Config Read() {
            if (!File.Exists(Emojifier.configPath)) {
                return new Config();
            }
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(Emojifier.configPath));
        }
    }
    
}
