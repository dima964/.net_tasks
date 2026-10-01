using System.Numerics;
using Raylib_cs;

namespace Pong;

internal class Program
{
    static void Main(string[] args)
    {
        Program pong = new Program();
        pong.RunGame();
    }

    void RunGame()
    {
        // Screen
        int screenWidth = 1000;
        int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "Pong");
        Raylib.SetTargetFPS(60);

        // Paddle settings
        float playerSpeed = 400.0f;
        float playerToWall = 60.0f;
        Vector2 playerSize = new Vector2(40, 200);

        // Player positions
        Vector2 player1 = new Vector2(
            playerToWall,
            screenHeight / 2 - playerSize.Y / 2
        );

        Vector2 player2 = new Vector2(
            screenWidth - playerSize.X - playerToWall,
            screenHeight / 2 - playerSize.Y / 2
        );

        // Scores
        int player1Score = 0;
        int player2Score = 0;

        // Ball
        Vector2 ballPosition = Raylib.GetScreenCenter();
        Vector2 ballDirection = Vector2.Normalize(new Vector2(1, 0.5f));
        float ballSpeed = 160.0f;
        float ballRadius = 15.0f;

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();

            // Player 1 movement - W / S
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                player1.Y -= playerSpeed * deltaTime;
            }

            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                player1.Y += playerSpeed * deltaTime;
            }

            // Player 2 movement - Arrow Up / Arrow Down
            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                player2.Y -= playerSpeed * deltaTime;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                player2.Y += playerSpeed * deltaTime;
            }

            // Keep player 1 inside the screen
            if (player1.Y < 0)
            {
                player1.Y = 0;
            }

            if (player1.Y > screenHeight - playerSize.Y)
            {
                player1.Y = screenHeight - playerSize.Y;
            }

            // Keep player 2 inside the screen
            if (player2.Y < 0)
            {
                player2.Y = 0;
            }

            if (player2.Y > screenHeight - playerSize.Y)
            {
                player2.Y = screenHeight - playerSize.Y;
            }

            // Move the ball
            ballPosition += ballDirection * ballSpeed * deltaTime;

            // Ball rectangle/paddle collision
            Rectangle player1Rectangle = new Rectangle(
                player1,
                playerSize
            );

            Rectangle player2Rectangle = new Rectangle(
                player2,
                playerSize
            );

            // Ball hits player 1
            if (Raylib.CheckCollisionCircleRec(
                ballPosition,
                ballRadius,
                player1Rectangle))
            {
                ballDirection.X *= -1;
                ballPosition.X = player1.X + playerSize.X + ballRadius;
            }

            // Ball hits player 2
            if (Raylib.CheckCollisionCircleRec(
                ballPosition,
                ballRadius,
                player2Rectangle))
            {
                ballDirection.X *= -1;
                ballPosition.X = player2.X - ballRadius;
            }

            // Ball hits top wall
            if (ballPosition.Y - ballRadius <= 0)
            {
                ballDirection.Y *= -1;
                ballPosition.Y = ballRadius;
            }

            // Ball hits bottom wall
            if (ballPosition.Y + ballRadius >= screenHeight)
            {
                ballDirection.Y *= -1;
                ballPosition.Y = screenHeight - ballRadius;
            }

            // Ball goes past the left side
            if (ballPosition.X + ballRadius < 0)
            {
                player2Score++;

                ballPosition = Raylib.GetScreenCenter();
                ballDirection = Vector2.Normalize(new Vector2(1, 0.5f));
            }

            // Ball goes past the right side
            if (ballPosition.X - ballRadius > screenWidth)
            {
                player1Score++;

                ballPosition = Raylib.GetScreenCenter();
                ballDirection = Vector2.Normalize(new Vector2(-1, 0.5f));
            }

            // Draw everything
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);

            // Players
            Raylib.DrawRectangleV(player1, playerSize, Color.White);
            Raylib.DrawRectangleV(player2, playerSize, Color.White);

            // Ball
            Raylib.DrawCircleV(ballPosition, ballRadius, Color.White);

            // Scores
            Raylib.DrawText(
                player1Score.ToString(),
                screenWidth / 2 - 100,
                50,
                50,
                Color.White
            );

            Raylib.DrawText(
                player2Score.ToString(),
                screenWidth / 2 + 70,
                50,
                50,
                Color.White
            );

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}