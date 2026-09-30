using System.Numerics;
using Raylib_cs;

namespace Screensaver;

internal class Program
{
    static void Main(string[] args)
    {
        // Open an 800x800 window
        Raylib.InitWindow(800, 800, "Screensaver");

        // Limit the frame rate to 30 FPS
        Raylib.SetTargetFPS(30);

        // Triangle points
        Vector2 A = new Vector2(Raylib.GetScreenWidth() / 2, 40);
        Vector2 B = new Vector2(40, Raylib.GetScreenHeight() / 2);
        Vector2 C = new Vector2(Raylib.GetScreenWidth() - 40, Raylib.GetScreenHeight() * 3 / 4);

        // Speed
        float speed = 100.0f;

        // Direction vectors
        Vector2 dirA = new Vector2(1, 1);
        Vector2 dirB = new Vector2(1, -1);
        Vector2 dirC = new Vector2(-1, 1);

        while (!Raylib.WindowShouldClose())
        {
            // Move the points
            A += dirA * speed * Raylib.GetFrameTime();
            B += dirB * speed * Raylib.GetFrameTime();
            C += dirC * speed * Raylib.GetFrameTime();

            // Check A against screen edges
            if (A.X < 0 || A.X > Raylib.GetScreenWidth())
            {
                dirA.X = dirA.X * -1.0f;
            }

            if (A.Y < 0 || A.Y > Raylib.GetScreenHeight())
            {
                dirA.Y = dirA.Y * -1.0f;
            }

            // Check B against screen edges
            if (B.X < 0 || B.X > Raylib.GetScreenWidth())
            {
                dirB.X = dirB.X * -1.0f;
            }

            if (B.Y < 0 || B.Y > Raylib.GetScreenHeight())
            {
                dirB.Y = dirB.Y * -1.0f;
            }

            // Check C against screen edges
            if (C.X < 0 || C.X > Raylib.GetScreenWidth())
            {
                dirC.X = dirC.X * -1.0f;
            }

            if (C.Y < 0 || C.Y > Raylib.GetScreenHeight())
            {
                dirC.Y = dirC.Y * -1.0f;
            }

            // Draw
            Raylib.BeginDrawing();

            // Black background
            Raylib.ClearBackground(Color.Black);

            // Draw triangle
            Raylib.DrawLineV(A, B, Color.Green);
            Raylib.DrawLineV(B, C, Color.Yellow);
            Raylib.DrawLineV(C, A, Color.SkyBlue);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}