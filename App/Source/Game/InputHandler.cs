using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameJam
{
    public class InputHandler : Actor
    {
        public InputHandler()
        {
            Layer = ELayer.Front;
            Engine.Get.Window.MouseButtonPressed += OnClick;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public void OnClick(object sender, MouseButtonEventArgs args)
        {
            //Hitting Moths
            if (args.Button == Mouse.Button.Left && Engine.Get.Scene.GetAll<Moth>().Count > 0) {
                List<Moth> moths = Engine.Get.Scene.GetAll<Moth>()
                    .Where(m => (m.Position - Engine.Get.MousePos).Size() <= m.CircleCollider.Radius)
                    .ToList();

                if (moths.Count > 0) moths.First().Hit();
            }
        }
    }
}
