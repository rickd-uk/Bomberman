
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] Players;

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach (GameObject Player in Players)
        {
            if (Player.activeSelf)
            {
                aliveCount++;
            }
        }

        if (aliveCount <=1)
        {
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
