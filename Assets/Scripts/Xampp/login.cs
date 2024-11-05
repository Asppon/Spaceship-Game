using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class login : MonoBehaviour
{
    public InputField usernameField; //username and password fields
    public InputField passwordField;

    public void LogIn()
    {
        StartCoroutine(LoginUser());
    }
    IEnumerator LoginUser()
    {
        string loginURL = "http://localhost/Roguelike/login.php"; //calls the php script
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text); //adds username and password to php script
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error); //if a success is not returned then an error is presented
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log(responseText); 
                //else then debug login successful
                if (responseText.Trim() == "Login successful")
                {
                    Debug.Log("Login successful!");
                    SceneManager.LoadScene("Main Menu");
                } //transition to the next scene upon successful login
                else
                {
                    Debug.Log("Invalid username or password. Please try again.");
                }
            }
        }
    }
}



