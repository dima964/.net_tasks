using System.Numerics;
using Raylib_cs;

namespace Tanks;

public class Tank
{
    public Vector2 position;
    public Vector2 direction;

    public Vector2 tankSize;
    public Vector2 turretSize;

    public Color color;

    public float speed;
    public float maxSpeed;

    public Tank(Vector2 position, Color color)
    {
        this.position = position;

        // Tank starts pointing up
        this.direction = new Vector2(0, -1);

        this.tankSize = new Vector2(50, 50);
        this.turretSize = new Vector2(20, 35);

        this.color = color;

        speed = 0;
        maxSpeed = 250;
    }

    public void Update(KeyboardKey upKey, KeyboardKey downKey, KeyboardKey leftKey, KeyboardKey rightKey)
    {
        // Stop moving when no key is pressed
        speed = 0;

        if (Raylib.IsKeyDown(upKey))
        {
            direction = new Vector2(0, -1);
            speed = maxSpeed;
        }
        else if (Raylib.IsKeyDown(downKey))
        {
            direction = new Vector2(0, 1);
            speed = maxSpeed;
        }
        else if (Raylib.IsKeyDown(leftKey))
        {
            direction = new Vector2(-1, 0);
            speed = maxSpeed;
        }
        else if (Raylib.IsKeyDown(rightKey))
        {
            direction = new Vector2(1, 0);
            speed = maxSpeed;
        }

        // Move the tank
        position += direction * speed * Raylib.GetFrameTime();
    }

    public void Draw()
    {
        // Draw the tank body
        Vector2 tankTopLeft = position - tankSize / 2;

        Raylib.DrawRectangleV(
            tankTopLeft,
            tankSize,
            color
        );

        // Draw the turret in the direction the tank is facing
        Vector2 turretPosition =
            position - turretSize / 2 + direction * 25;

        Raylib.DrawRectangleV(
            turretPosition,
            turretSize,
            color
        );
    }

    public Rectangle GetRectangle()
    {
        // Used for collision checks
        return new Rectangle(
            position - tankSize / 2,
            tankSize
        );
    }
}