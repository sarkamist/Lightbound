using Candle;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace GameJam
{
    public class Darkness : StaticActor
    {
        public List<RadialLight> Lights;
        public LightingArea Fog;
        public Darkness()
        {
            Layer = ELayer.Darkness;
            Lights = new List<RadialLight>();
            Fog = new LightingArea(LightingArea.Mode.Fog, new Vector2f(0f, 0f), new Vector2u(Engine.Get.Window.Size.X, Engine.Get.Window.Size.Y));
            Fog.AreaColor = Color.Black;
		}

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
			Fog.Clear();
			if (Lights.Count > 0)
            {
                foreach (RadialLight light in Lights)
                {
                    Fog.Draw(light);
                }
            }
            Fog.Draw(target, states);
            Fog.Display();
        }
	}
}

