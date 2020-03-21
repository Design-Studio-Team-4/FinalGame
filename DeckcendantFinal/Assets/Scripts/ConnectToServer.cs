using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class ConnectToServer : MonoBehaviour
{
    int score = 1;
    string name = "Chris";

    public TMPro.TextMeshProUGUI Leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(connect());
        StartCoroutine(RunLeaderboard(name, score));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator connect(){
        using(UnityWebRequest www = UnityWebRequest.Get("http://localhost/Deckcendant/connect.php")){
            yield return www.Send();
            if (www.isNetworkError|| www.isHttpError){
                Debug.Log(www.error);

            }else{
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
}

