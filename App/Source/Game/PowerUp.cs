using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameJam
{
    public class PowerUp : AnimatedActor
    {
        public float RemainingTime;

        public const float AbsorbRate = 5f;
        public PowerUp()
        {
            Layer = ELayer.Items;
            AnimatedSprite = new AnimatedSprite(new Texture(Assets.Open("Lightbound.Data.Textures.PowerUp.Potion.png")), 2, 1);
            AnimatedSprite.Loop = false;
            AnimatedSprite.FrameTime = 0.25f;
            Center();

            Position = new Vector2f(
                Engine.Get.random.NextFloat(GetLocalBounds().Width, Engine.Get.Window.Size.X - GetLocalBounds().Width),
                Engine.Get.random.NextFloat(GetLocalBounds().Height, Engine.Get.Window.Size.Y - GetLocalBounds().Height)
            );

            RemainingTime = Engine.Get.random.Next(4, 6);

            OnDestroy += OnPotionDestroy;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }

        public override void Update(float dt)
        {
            if (MyGame.Get.CurrentState != MyGame.GameState.Game) return;
            base.Update(dt);
            if (Mouse.IsButtonPressed(Mouse.Button.Left) && GetGlobalBounds().Contains(Engine.Get.MousePos.X, Engine.Get.MousePos.Y)) {
                AnimatedSprite.Loop = true;
                RemainingTime -= (dt * AbsorbRate);
                MyGame.Get.Lightbeam.TimeIncrease += (dt * AbsorbRate);
                if (RemainingTime <= 0f) {
                    Destroy();
                }
            } else
            {
                AnimatedSprite.Loop = false;
            }
        }

        private void OnPotionDestroy(Actor actor) {
            MyGame.Get.SoundManager.PlaySound("PotionVFX", 30.0f);
        }

        public void RemoveOnPotionDestroy()
        {
            OnDestroy -= OnPotionDestroy;
        }
    }
}
