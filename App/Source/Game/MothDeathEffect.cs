using SFML.System;
using SFML.Graphics;

namespace GameJam
{
    public class MothDeathEffect : AnimatedActor
    {
        private float LifeTime;

        public MothDeathEffect()
        {
            AnimatedSprite = new AnimatedSprite(new Texture(Assets.Open("Lightbound.Data.Textures.Moth.DeathEffect.png")), 4, 1);
            AnimatedSprite.Loop = false;
            AnimatedSprite.FrameTime = 0.2f;
            LifeTime = AnimatedSprite.FrameTime * 2f;
            Center();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Position += new Vector2f(0.0f, 30.0f * dt);
            LifeTime -= dt;
            if (LifeTime < 0.0f)
            {
                Destroy();
            }
        }
    }
}

