using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ConnectToServer : MonoBehaviour
{
    public Score ScoreClass;
    string PlayerName;
    int PlayerScore;
    public TMPro.TextMeshProUGUI leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerScore = ScoreClass.getScore();
        PlayerScore = 1;
        PlayerName = "Chris";
       // StartCoroutine(connect());
        //StartCoroutine(WebTestStart());
        StartCoroutine(AddPlaythrough(PlayerName,PlayerScore));
    }
    
    // Update is called once per frame
    void Update()
    {

        
    }
    IEnumerator connect(){
        using(UnityWebRequest www = UnityWebRequest.Get("http://localhost/Deckcendant/board.php")){
            yield return www.SendWebRequest();
            if (www.isNetworkError|| www.isHttpError){
                Debug.Log(www.error);

            }else{
               Debug.Log(www.downloadHandler.text);
               byte[] results = www.downloadHandler.data;
            }
        }
    }
    IEnumerator WebTestStart()
    {
        WWW request = new WWW("http://localhost/Deckcendant/WebTest.php");
        yield return request;
        string[] webResults = request.text.Split('\t');
        foreach (string s in webResults)
        {
            Debug.Log(s);
        }
    }
    IEnumerator AddPlaythrough(string name, int score)
    {
        
        WWWForm form = new WWWForm();
        string scorestring = score.ToString();
        form.AddField("name", name);
        form.AddField("score", score);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Deckcendant/board.php", form))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
            leaderboard.text = www.downloadHandler.text;
        }
        
    }
}
