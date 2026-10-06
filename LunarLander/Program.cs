using System.Numerics;
using Raylib_cs;

namespace LunarLander;

internal class Program
{
    static void Main(string[] args)
    {
        Program lander = new Program();
        lander.GameLoop();
    }

    void GameLoop()
    {
        int screenWidth = 800;
        int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "Lunar Lander");
        Raylib.SetTargetFPS(60);

        Vector2 startPosition = new Vector2(
            screenWidth / 2,
            150
        );

        Ship ship = new Ship(startPosition);

        // Landing platform at the bottom
        Rectangle landingPlatform = new Rectangle(
            300,
            520,
            200,
            30
        );

        bool gameOver = false;
        bool landed = false;

        float safeLandingSpeed = 80.0f;

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();

            if (!gameOver)
            {
                ship.Update(deltaTime);

                // Check if the ship hits the landing platform
                if (Raylib.CheckCollisionRecs(
                    ship.GetRectangle(),
                    landingPlatform))
                {
                    gameOver = true;

                    // Low enough speed means a successful landing
                    if (MathF.Abs(ship.velocity.Y) <= safeLandingSpeed)
                    {
                        landed = true;
                    }
                }

                // Ship goes off the screen
                if (ship.position.Y > screenHeight + 100)
                {
                    gameOver = true;
                    landed = false;
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            ship.Draw();

            // Draw the landing platform
            Raylib.DrawRectangleRec(
                landingPlatform,
                Color.Green
            );

            // Show fuel
            Raylib.DrawText(
                "Fuel: " + (int)ship.fuel,
                20,
                20,
                30,
                Color.White
            );

            // Show speed
            Raylib.DrawText(
                "Speed: " + (int)MathF.Abs(ship.velocity.Y),
                20,
                60,
                25,
                Color.White
            );

            if (gameOver)
            {
                if (landed)
                {
                    Raylib.DrawText(
                        "SUCCESSFUL LANDING!",
                        220,
                        250,
                        40,
                        Color.Green
                    );
                }
                else
                {
                    Raylib.DrawText(
                        "CRASH!",
                        320,
                        250,
                        50,
                        Color.Red
                    );
                }
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}