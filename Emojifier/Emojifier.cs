using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using NuGet.Common;
using Terraria;
using Terraria.Localization;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace Emojifier
{
    [ApiVersion(2, 1)]
    public class Emojifier : TerrariaPlugin
    {
        #region Plugin Info

        public override string Name => "Emojifier";
        public override Version Version => new Version(1, 1, 0);
        public override string Author => "Soofa";
        public override string Description => "Emojis!";

        #endregion

        public Emojifier(Main game) : base(game) { }
        public static string configPath = Path.Combine(TShock.SavePath + "/EmojifierConfig.json");
        Config config = new Config();

        public override void Initialize()
        {
            ServerApi.Hooks.ServerChat.Register(this, OnServerChat);
            TShockAPI.Hooks.GeneralHooks.ReloadEvent += OnReload;

            if (File.Exists(configPath))
            {
                config = Config.Read();
            }
            else
            {
                config.Write();
            }
        }

        private void OnServerChat(ServerChatEventArgs args)
        {
            string text = args.Text;

            // Place the emojis
            if (text.Contains(':'))
            {
                foreach (var kvp in config.Emojis)
                {
                    text = text.Replace($":{kvp.Key}:", $"[i:{kvp.Value}]");
                }
            }

            // Forcefully change the Text property
            PropertyInfo? propertyInfo = args.GetType().GetProperty("Text");
            propertyInfo?.SetValue(args, text);
        }

        private void OnReload(ReloadEventArgs e)
        {
            if (File.Exists(configPath))
            {
                config = Config.Read();
            }
            else
            {
                config.Write();
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TShockAPI.Hooks.GeneralHooks.ReloadEvent -= OnReload;
            }
            base.Dispose(disposing);
        }
    }
}