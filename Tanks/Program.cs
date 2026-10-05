using System.Numerics;
using Raylib_cs;

namespace Tanks;

internal class Program
{
    static void Main(string[] args)
    {
        Program game = new Program();
        game.GameLoop();
    }

    void GameLoop()
    {
        int screenWidth = 1000;
        int screenHeight = 700;

        Raylib.InitWindow(screenWidth, screenHeight, "Tanks");
        Raylib.SetTargetFPS(60);

        // Starting positions for both tanks
        Vector2 player1Start = new Vector2(150, screenHeight / 2);
        Vector2 player2Start = new Vector2(screenWidth - 150, screenHeight / 2);

        Tank player1 = new Tank(player1Start, Color.Green);
        Tank player2 = new Tank(player2Start, Color.Red);

        int player1Score = 0;
        int player2Score = 0;

        Bullet? player1Bullet = null;
        Bullet? player2Bullet = null;

        // Wall in the middle of the map
        Wall wall = new Wall(
            new Vector2(screenWidth / 2 - 25, 150),
            new Vector2(50, 400)
        );

        double player1LastShootTime = 0;
        double player2LastShootTime = 0;

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();

            // Move the tanks
            Vector2 player1OldPosition = player1.position;
            Vector2 player2OldPosition = player2.position;

            player1.Update(
                KeyboardKey.W,
                KeyboardKey.S,
                KeyboardKey.A,
                KeyboardKey.D
            );

            player2.Update(
                KeyboardKey.Up,
                KeyboardKey.Down,
                KeyboardKey.Left,
                KeyboardKey.Right
            );

            // Stop the tank if it hits the wall
            if (Raylib.CheckCollisionRecs(
                player1.GetRectangle(),
                wall.GetRectangle()))
            {
                player1.position = player1OldPosition;
            }

            if (Raylib.CheckCollisionRecs(
                player2.GetRectangle(),
                wall.GetRectangle()))
            {
                player2.position = player2OldPosition;
            }

            // Keep tanks inside the screen
            KeepTankInsideScreen(player1, screenWidth, screenHeight);
            KeepTankInsideScreen(player2, screenWidth, screenHeight);

            // Tanks can't move through each other
            if (Raylib.CheckCollisionRecs(
                player1.GetRectangle(),
                player2.GetRectangle()))
            {
                player1.position = player1OldPosition;
                player2.position = player2OldPosition;
            }

            // Shoot with Space and Enter
            double currentTime = Raylib.GetTime();

            if (Raylib.IsKeyPressed(KeyboardKey.Space)
                && player1Bullet == null
                && currentTime - player1LastShootTime >= 1)
            {
                player1Bullet = new Bullet(
                    player1.position,
                    player1.direction
                );

                player1LastShootTime = currentTime;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Enter)
                && player2Bullet == null
                && currentTime - player2LastShootTime >= 1)
            {
                player2Bullet = new Bullet(
                    player2.position,
                    player2.direction
                );

                player2LastShootTime = currentTime;
            }

            // Update player 1 bullet
            if (player1Bullet != null)
            {
                player1Bullet.Update();

                if (Raylib.CheckCollisionRecs(
                    player1Bullet.GetRectangle(),
                    wall.GetRectangle()))
                {
                    player1Bullet = null;
                }
                else if (Raylib.CheckCollisionRecs(
                    player1Bullet.GetRectangle(),
                    player2.GetRectangle()))
                {
                    player1Score++;

                    player1.position = player1Start;
                    player2.position = player2Start;

                    player1Bullet = null;
                    player2Bullet = null;
                }
                else if (
                    player1Bullet.position.X < 0 ||
                    player1Bullet.position.X > screenWidth ||
                    player1Bullet.position.Y < 0 ||
                    player1Bullet.position.Y > screenHeight)
                {
                    player1Bullet = null;
                }
            }

            // Update player 2 bullet
            if (player2Bullet != null)
            {
                player2Bullet.Update();

                if (Raylib.CheckCollisionRecs(
                    player2Bullet.GetRectangle(),
                    wall.GetRectangle()))
                {
                    player2Bullet = null;
                }
                else if (Raylib.CheckCollisionRecs(
                    player2Bullet.GetRectangle(),
                    player1.GetRectangle()))
                {
                    player2Score++;

                    player1.position = player1Start;
                    player2.position = player2Start;

                    player1Bullet = null;
                    player2Bullet = null;
                }
                else if (
                    player2Bullet.position.X < 0 ||
                    player2Bullet.position.X > screenWidth ||
                    player2Bullet.position.Y < 0 ||
                    player2Bullet.position.Y > screenHeight)
                {
                    player2Bullet = null;
                }
            }

            // Draw everything
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            wall.Draw();

            player1.Draw();
            player2.Draw();

            if (player1Bullet != null)
            {
                player1Bullet.Draw();
            }

            if (player2Bullet != null)
            {
                player2Bullet.Draw();
            }

            Raylib.DrawText(
                "Player 1: " + player1Score,
                20,
                20,
                30,
                Color.White
            );

            Raylib.DrawText(
                "Player 2: " + player2Score,
                screenWidth - 200,
                20,
                30,
                Color.White
            );

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    void KeepTankInsideScreen(Tank tank, int screenWidth, int screenHeight)
    {
        float halfWidth = tank.tankSize.X / 2;
        float halfHeight = tank.tankSize.Y / 2;

        if (tank.position.X - halfWidth < 0)
            tank.position.X = halfWidth;

        if (tank.position.X + halfWidth > screenWidth)
            tank.position.X = screenWidth - halfWidth;

        if (tank.position.Y - halfHeight < 0)
            tank.position.Y = halfHeight;

        if (tank.position.Y + halfHeight > screenHeight)
            tank.position.Y = screenHeight - halfHeight;
    }
}