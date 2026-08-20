using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace MasterPauseButton
{
    public class ModEntry : Mod
    {
        private ModConfig Config;

        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady || e.Button != this.Config.PauseKey)
            {
                return;
            }

            if (!Game1.IsMasterGame)
            {
                Game1.addHUDMessage(new HUDMessage(Game1.content.LoadString("Strings\\UI:Chat_HostOnlyCommand"), 0));
                return;
            }

            Game1.netWorldState.Value.IsPaused = !Game1.netWorldState.Value.IsPaused;
            Game1.addHUDMessage(new HUDMessage(Game1.netWorldState.Value.IsPaused ? "Paused" : "Resumed", 0));
        }
    }
}