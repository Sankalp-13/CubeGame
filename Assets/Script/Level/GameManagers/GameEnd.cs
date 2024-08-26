using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    public Transform player;
    public float fallThreshold = -10f;

    private bool won = false;
    private bool lost = false;

    void Update()
    {
        if (player.position.y < fallThreshold && !lost)
        {
            GameOver();
        }

        if (ScoreManager.instance.score >= 50 && !won)
        {
            YouWon();
        }
    }


    void YouWon()
    {
        GameOverManager manager = FindObjectOfType<GameOverManager>();
        manager.TriggerYouWon();
    }
    void GameOver()
    {
        GameOverManager manager = FindObjectOfType<GameOverManager>();

        manager.TriggerGameOver();
    }
}
