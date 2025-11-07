using SFML.Graphics;
using SFML.System;

namespace GameJam
{
    public class GameBackground : StaticActor
    {
        public GameBackground()
        {
            Layer = ELayer.Background;
            Texture t = new Texture(
                Assets.Open("Lightbound.Data.Textures.Backgrounds.GameBackground.png"),
                new IntRect(0, 0, (int) Engine.Get.Window.Size.X, (int) Engine.Get.Window.Size.Y)
            );
            t.Repeated = true;
            Sprite = new Sprite(t);
            Center();
            Position = new Vector2f(GetLocalBounds().Width / 2f, GetLocalBounds().Height / 2f);
        }
        public override void Update(float dt)
        {
        }
    }
}

