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
    bool ranBoard = false;
    string sort = "false";
    public TMPro.TextMeshProUGUI Leaderboard;
    bool connected = false;
    bool added = false;
    // Start is called before the first frame update
    void Start()
    {
        ranBoard = false;
        added = false;
        SubmissionCanvas.SetActive(true);
        LeaderboardCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
       
        if (submitted == true)
        {
            if (connected == false)
            {

                SubmissionCanvas.SetActive(false);
                LeaderboardCanvas.SetActive(true);
                StartCoroutine(connect());
                StartCoroutine(AddPlaythrough(name, CoinandScore.coinsAndScoreInstance.score));
                
                
            }
            else
            {
                if(ranBoard == false)
                {
                StartCoroutine(RunLeaderboard(sort));
                }
                
                //StartCoroutine(GetLeaderboard());
            }


        }
        else
        {

           
            
        }

    }
    IEnumerator GetLeaderboard()
    {

        using (UnityWebRequest www = UnityWebRequest.Get("http://ugrad.bitdegree.ca/~christopherwclar/deck/text.txt"))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

            }
            else
            {
                
                byte[] results = www.downloadHandler.data;
            }
        }
        yield return null;
    }
    IEnumerator connect() {
        using (UnityWebRequest www = UnityWebRequest.Get("http://ugrad.bitdegree.ca/~christopherwclar/deck/connect.php")) {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);

            } else {
                connected = true;
                Debug.Log(www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
      
        yield return null;
    }
    IEnumerator disconnect()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://ugrad.bitdegree.ca/~christopherwclar/deck/disconnect.php"))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

            }
            else
            {
                connected = false;
                Debug.Log(www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
        yield return null;
    }
    IEnumerator AddPlaythrough(string name, int score)
    {
        if(added == false)
        { added = true;
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);
        using (UnityWebRequest www = UnityWebRequest.Post("http://ugrad.bitdegree.ca/~christopherwclar/deck/addplaythrough.php", form))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }

        }
        }
       
        
        yield return null;
    } 
    IEnumerator RunLeaderboard(string sort)
    {   ranBoard = true;
        WWWForm form = new WWWForm();
        form.AddField("sort", sort);
        using (UnityWebRequest www = UnityWebRequest.Post("http://ugrad.bitdegree.ca/~christopherwclar/deck/getleaderboard.php", form))
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
        yield return null;
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
    public void Menu()
    {
        StartCoroutine(disconnect());
        Debug.Log("Hit Menu Button");
    }
    public void sortbutton()
    {
        if (sort == "false")
        {
            sort = "true";
        }
        else
        {
            sort = "false";
        }
        StartCoroutine(RunLeaderboard(sort));
    }
    private void OnApplicationQuit()
    {
        StartCoroutine(disconnect());
    }
} 


