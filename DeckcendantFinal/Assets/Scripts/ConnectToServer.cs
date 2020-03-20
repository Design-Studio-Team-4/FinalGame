using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class ConnectToServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(connect());
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
   
}
