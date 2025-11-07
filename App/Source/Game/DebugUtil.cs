using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameJam
{
    public class DebugUtil
    {
        public static void DrawFloatReact(FloatRect collider)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad0))
            {
                Vertex[] lines = {
                    new Vertex(new Vector2f(collider.Left, collider.Top), Color.Red),
                    new Vertex(new Vector2f(collider.Left + collider.Width, collider.Top), Color.Red),
                    new Vertex(new Vector2f(collider.Left + collider.Width, collider.Top + collider.Height), Color.Red),
                    new Vertex(new Vector2f(collider.Left, collider.Top + collider.Height), Color.Red),
                    new Vertex(new Vector2f(collider.Left, collider.Top), Color.Red),
                    new Vertex(new Vector2f(collider.Left + collider.Width, collider.Top + collider.Height), Color.Red),
                    new Vertex(new Vector2f(collider.Left, collider.Top + collider.Height), Color.Red),
                    new Vertex(new Vector2f(collider.Left + collider.Width, collider.Top), Color.Red),
                };
                Engine.Get.Window.Draw(lines, PrimitiveType.LineStrip);
                CircleShape center = new CircleShape(5f);
                center.Origin = new Vector2f(center.GetLocalBounds().Width, center.GetLocalBounds().Height) / 2.0f;
                center.FillColor = Color.Red;
                center.Position = new Vector2f(collider.Left + (collider.Width / 2f), collider.Top + (collider.Height / 2f));
                Engine.Get.Window.Draw(center);
            }
        }

        public static void DrawShape(Shape shape)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad0))
            {
                Engine.Get.Window.Draw(shape);
            }
        }
    }
}
