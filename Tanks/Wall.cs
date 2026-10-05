using System.Numerics;
using Raylib_cs;

namespace Tanks;

public class Wall
{
    public Vector2 position;
    public Vector2 size;

    public Wall(Vector2 position, Vector2 size)
    {
        this.position = position;
        this.size = size;
    }

    public void Draw()
    {
        // Draw the wall
        Raylib.DrawRectangleV(
            position,
            size,
            Color.Gray
        );
    }

    public Rectangle GetRectangle()
    {
        // Used for collision checks
        return new Rectangle(position, size);
    }
}