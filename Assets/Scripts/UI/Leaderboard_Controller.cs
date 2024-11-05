using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboard_Controller : MonoBehaviour
{
    public Text[] nameTexts; //array of Text components for displaying names
    public Text[] scoreTexts; //array of Text components for displaying scores

    public void GetScores()
    {
        StartCoroutine(RetrieveScoresFromDatabase());
    }

    IEnumerator RetrieveScoresFromDatabase()
    {
        string retrieveURL = "http://localhost/Roguelike/retrieve_scores.php";
        //connections to database
        using (WWW www = new WWW(retrieveURL))
        {
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("Error retrieving scores: " + www.error);
                yield break;
            }
            //split the received data by new line characters
            string[] lines = www.text.Split('\n');
            //display scores in the text boxes
            for (int i = 0; i < Mathf.Min(lines.Length, nameTexts.Length, scoreTexts.Length); i++)
            {
                string[] data = lines[i].Split(',');
                if (data.Length >= 2)
                {
                    string name = data[0].Trim(); //name
                    string score = data[1].Trim(); //score

                    //assign name and score to corresponding text components
                    if (i < nameTexts.Length && nameTexts[i] != null)
                        nameTexts[i].text = name;
                    if (i < scoreTexts.Length && scoreTexts[i] != null)
                        scoreTexts[i].text = score;
                }
            }
        }
    }
}

