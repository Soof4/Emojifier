using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace Emojifier {
    [ApiVersion(2, 1)]
    public class Emojifier : TerrariaPlugin {
        public override string Name => "Emojifier";
        public override Version Version => new Version(1, 0, 0);
        public override string Author => "Soofa";
        public override string Description => "Emojis!";
        public Emojifier(Main game) : base(game) {
        }

        public static string configPath = Path.Combine(TShock.SavePath + "/EmojifierConfig.json");
        Config config = new Config();
        public override void Initialize() {
            TShockAPI.Hooks.PlayerHooks.PlayerChat += OnPlayerChat;
            TShockAPI.Hooks.GeneralHooks.ReloadEvent += OnReload;

            if (File.Exists(configPath)) {
                config = Config.Read();
            }
            else {
                config.Write();
            }
        }

        private void OnPlayerChat(PlayerChatEventArgs args) {
            foreach (var kvp in config.Emojis) {
                args.RawText = args.RawText.Replace($":{kvp.Key}:", $"[i:{kvp.Value}]");
            }
        }

        private void OnReload(ReloadEventArgs e) {
            if (File.Exists(configPath)) {
                config = Config.Read();
            }
            else {
                config.Write();
            }
        }
        protected override void Dispose(bool disposing) {
            if (disposing) {
                TShockAPI.Hooks.PlayerHooks.PlayerChat -= OnPlayerChat;
                TShockAPI.Hooks.GeneralHooks.ReloadEvent -= OnReload;
            }
            base.Dispose(disposing);
        }
    }
}