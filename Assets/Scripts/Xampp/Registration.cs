using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField usernameField; // input field for username
    public InputField passwordField; // input field for password

    // method to register user
    public void Register()
    {
        // start the registration coroutine
        StartCoroutine(RegisterUser());
    }
    IEnumerator RegisterUser()    // coroutine to register user
    {
        // url for registration
        string registerURL = "http://localhost/Roguelike/register.php";

        // create form data
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);

        // send request to register user
        using (UnityWebRequest www = UnityWebRequest.Post(registerURL, form))
        {
            yield return www.SendWebRequest();

            // check if request was successful
            if (www.result != UnityWebRequest.Result.Success)
            {
                // log error if request fails
                Debug.Log(www.error);
            }
            else
            {
                // log response if request succeeds
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
