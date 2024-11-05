using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // method to start playing the game
    public void Play()
    {
        // load the ship entrance main scene
        SceneManager.LoadScene("ShipEntranceMain");
    }
    // method to navigate to the options menu
    public void Options()
    {
        // load the options scene
        SceneManager.LoadScene("Options");
    }
    // method to exit the game
    public void Exit()
    {
        // quit the application
        Application.Quit();
    }
    // method to go back to the main menu
    public void Back()
    {
        // load the main menu scene
        SceneManager.LoadScene("Main Menu");
    }
    // method to view the leaderboard
    public void Leaderboard()
    {
        // load the leaderboard scene
        SceneManager.LoadScene("Leaderboard");
    }
    // method to view the controls
    public void Controls()
    {
        // load the controls scene
        SceneManager.LoadScene("Controls");
    }
}
