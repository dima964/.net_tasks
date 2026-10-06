using System.Numerics;
using Raylib_cs;

namespace LunarLander;

public class Ship
{
    public Vector2 position;
    public Vector2 velocity;

    public float enginePower;
    public float fuel;
    public float fuelConsumption;

    public bool engineOn;

    public Vector2 shipSize;

    public Ship(Vector2 position)
    {
        this.position = position;
        velocity = Vector2.Zero;

        enginePower = 300.0f;
        fuel = 100.0f;
        fuelConsumption = 20.0f;

        engineOn = false;

        shipSize = new Vector2(40, 60);
    }

    public void Update(float deltaTime)
    {
        // Gravity pulls the ship down
        velocity.Y += 100.0f * deltaTime;

        engineOn = false;

        // Use the engine while Space is held
        if (Raylib.IsKeyDown(KeyboardKey.Space) && fuel > 0)
        {
            engineOn = true;

            velocity.Y -= enginePower * deltaTime;
            fuel -= fuelConsumption * deltaTime;

            if (fuel < 0)
                fuel = 0;
        }

        position += velocity * deltaTime;
    }

    public void Draw()
    {
        // Draw the ship
        Vector2 shipTopLeft = position - shipSize / 2;

        Raylib.DrawRectangleV(
            shipTopLeft,
            shipSize,
            Color.White
        );

        // Draw the engine flame
        if (engineOn)
        {
            Raylib.DrawCircle(
                (int)position.X,
                (int)(position.Y + shipSize.Y / 2 + 10),
                10,
                Color.Orange
            );
        }
    }

    public Rectangle GetRectangle()
    {
        return new Rectangle(
            position - shipSize / 2,
            shipSize
        );
    }
}