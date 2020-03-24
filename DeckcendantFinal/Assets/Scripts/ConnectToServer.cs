using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ConnectToServer : MonoBehaviour
{
    public TMPro.TMP_InputField Inputfield;
    public GameObject placeholdertext;
    public GameObject LeaderboardCanvas;
    public GameObject SubmissionCanvas;
    int score = 1;
    string name = " ";
    bool submitted = false;
    bool ranConnect = false;

    public TMPro.TextMeshProUGUI Leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        SubmissionCanvas.SetActive(true);
        LeaderboardCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (submitted == true)
        {
            if (ranConnect == false)
            {
               // StartCoroutine(connect());
                StartCoroutine(RunLeaderboard(name, score));
                SubmissionCanvas.SetActive(false);
                LeaderboardCanvas.SetActive(true);
                ranConnect = true;
            }
  
        }
    }
    IEnumerator connect() {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/Deckcendant/connect.php")) {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);

            } else {
                Debug.Log(www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
    }
    IEnumerator RunLeaderboard(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Deckcendant/board.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

            }
            else
            {
                Leaderboard.text = www.downloadHandler.text;
                byte[] results = www.downloadHandler.data;
            }


        }
    }
    public void NextPage()
    {
        Leaderboard.pageToDisplay++;
    }

    public void PreviousPage()
    {
        if (Leaderboard.pageToDisplay > 0)
        {
            Leaderboard.pageToDisplay--;
        }

    }
    public void SubmitName()
    {
        if (string.IsNullOrEmpty(Inputfield.text) != true)
        {
            name = Inputfield.text;
            submitted = true;

        }
        else
        {
            placeholdertext.GetComponent<TMPro.TextMeshProUGUI>().text = "Invalid";
        }
   
    }
} 


