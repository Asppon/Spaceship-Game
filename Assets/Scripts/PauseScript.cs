using UnityEngine;
using System.Collections;
using System.Linq;

public class PauseScript : MonoBehaviour
{
    private bool isPaused = false;
    private bool cooldownActive = false;
    public float cooldownDuration = 0.5f; //adjust as needed
    void Update()
    {
        if (cooldownActive)
            return;
        if (Input.GetKeyDown(KeyCode.Escape)) //when escape is pressed
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
            StartCoroutine(PauseCooldown());
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; //sets the time scale to 0 to pause the game
        isPaused = true; //sets paused to true
        Debug.Log("Game Paused");
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; //sets the time scale back to 1 to resume the game
        isPaused = false;
        Debug.Log("Game Resumed");
    }

    IEnumerator PauseCooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldownDuration);
        cooldownActive = false;
    }
}


