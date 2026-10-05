using System.Numerics;
using Raylib_cs;

namespace Tanks;

public class Bullet
{
    public Vector2 position;
    public Vector2 direction;

    public float speed;
    public float radius;

    public Bullet(Vector2 position, Vector2 direction)
    {
        this.position = position;
        this.direction = direction;

        speed = 500;
        radius = 8;
    }

    public void Update()
    {
        // Move the bullet in the direction the tank was facing
        position += direction * speed * Raylib.GetFrameTime();
    }

    public void Draw()
    {
        Raylib.DrawCircleV(
            position,
            radius,
            Color.White
        );
    }

    public Rectangle GetRectangle()
    {
        // Used for collision checks
        return new Rectangle(
            position.X - radius,
            position.Y - radius,
            radius * 2,
            radius * 2
        );
    }
}