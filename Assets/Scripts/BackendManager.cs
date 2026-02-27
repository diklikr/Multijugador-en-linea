using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BackendManager : MonoBehaviour
{
  private string apiURL = "http://localhost:3000/score";

    public void SendScore(string playerName, int score)
    {
        StartCoroutine(PostScore(playerName, score));
    }

    IEnumerator PostScore(string playerName, int score){
        ScoreData data = new ScoreData { player = playerName, score = score };

        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(apiURL + "/scores", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return new WaitForSeconds(0.1f);
        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
        Debug.Log("Respuesta del servidor" + request.downloadHandler.text);
        }
    }

}
