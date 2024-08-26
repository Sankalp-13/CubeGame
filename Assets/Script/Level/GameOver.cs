using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    public Transform player;
    public float fallThreshold = -10f;

    void Update()
    {
        if (player.position.y < fallThreshold)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Implement game over logic here, such as stopping the game, showing a game over screen, etc.
        Debug.Log("Game Over!");
    }
}
