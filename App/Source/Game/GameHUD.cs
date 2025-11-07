using SFML.Graphics;
using SFML.System;
using System.ComponentModel.Design;
using System.Linq;

namespace GameJam
{
  public class GameHUD : Actor
  {
        Font FontOsifont, FontHeartz;
        Text TXT_LightsOut, TXT_LightsOutTimer, TXT_CurrentHealth;

        public GameHUD()
        {
            Layer = ELayer.HUD;
            FontOsifont = new Font(Assets.Open("Lightbound.Data.Fonts.osifont.ttf"));
            FontHeartz = new Font(Assets.Open("Lightbound.Data.Fonts.heartz.ttf"));
            TXT_LightsOut = new Text($"Lights out in", FontOsifont);
            TXT_LightsOut.Position = new Vector2f((Engine.Get.Window.Size.X / 2f) - (TXT_LightsOut.GetLocalBounds().Width / 2f), 7.5f);
            TXT_LightsOutTimer = new Text($"{MyGame.Get.Lightbeam?.RemainingTime:0.0}s", FontOsifont);
            TXT_LightsOutTimer.Position = new Vector2f((Engine.Get.Window.Size.X / 2f) - (TXT_LightsOutTimer.GetLocalBounds().Width / 2f), TXT_LightsOut.GetLocalBounds().Height + 15f);
            string hearts = Enumerable.Range(1, MyGame.Get.Character.CurrentHealth)
                .Aggregate("", (acc, next) => acc += "D");
            TXT_CurrentHealth = new Text($"{hearts}", FontHeartz);
            TXT_CurrentHealth.FillColor = new Color(255, 125, 125, 150);
            TXT_CurrentHealth.CharacterSize = 25;
            TXT_CurrentHealth.Position = new Vector2f((Engine.Get.Window.Size.X / 2f) - (TXT_CurrentHealth.GetLocalBounds().Width / 2f), Engine.Get.Window.Size.Y - (TXT_CurrentHealth.GetLocalBounds().Height + 15f));
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            TXT_LightsOut.Draw(target, states);
            TXT_LightsOutTimer.Draw(target, states);
            TXT_CurrentHealth.Draw(target, states);
        }

        public override void Update(float dt)
        {
            if (MyGame.Get.CurrentState != MyGame.GameState.Game) return;
            base.Update(dt);

            if (MyGame.Get.Lightbeam?.TimeIncrease > 0f)
            {
                TXT_LightsOut.DisplayedString = $"Lights out in";
                TXT_LightsOutTimer.DisplayedString = $"{MyGame.Get.Lightbeam.RemainingTime:0.0}s + {MyGame.Get.Lightbeam.TimeIncrease:0.0}s";
            }
            else if (MyGame.Get.Lightbeam?.RemainingTime > 0f)
            {
                TXT_LightsOut.DisplayedString = $"Lights out in";
                TXT_LightsOutTimer.DisplayedString = $"{MyGame.Get.Lightbeam.RemainingTime:0.0}s";
            }
            else
            {
                TXT_LightsOut.DisplayedString = $"Lights out!";
                TXT_LightsOutTimer.DisplayedString = $"";
            }
            TXT_LightsOut.Position = new Vector2f((Engine.Get.Window.Size.X / 2f) - (TXT_LightsOut.GetLocalBounds().Width / 2f), 7.5f);
            TXT_LightsOutTimer.Position = new Vector2f((Engine.Get.Window.Size.X / 2f) - (TXT_LightsOutTimer.GetLocalBounds().Width / 2f), TXT_LightsOut.GetLocalBounds().Height + 15f);

            string hearts = Enumerable.Range(1, MyGame.Get.Character.CurrentHealth)
                .Aggregate("", (acc, next) => acc += "D");
            TXT_CurrentHealth.DisplayedString = $"{hearts}";
            TXT_CurrentHealth.Position = new Vector2f((Engine.Get.Window.Size.X / 2f) - (TXT_CurrentHealth.GetLocalBounds().Width / 2f), Engine.Get.Window.Size.Y - (TXT_CurrentHealth.GetLocalBounds().Height + 15f));
        }
  }
}

