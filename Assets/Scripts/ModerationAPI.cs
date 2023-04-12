using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class ModerationAPI : MonoBehaviour
{
    public InputField inputField;
    public Text output;
    public string apiKey = "YOUR API KEY";
    public string model = "text-moderation-001";
    private string voilate = "Yes, the entered text voilates the content policy of OpenAI";
    private string notVoilate = "No, the entered text does not voilates the content policy of OpenAI";

    public void GetResponse()
    {
        StartCoroutine(MakeRequest());
    }

    IEnumerator MakeRequest()
    {
        // Create a JSON object with the necessary parameters
        var json = "{\"input\":\"" + inputField.text + "\"}";
        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        // Create a new UnityWebRequest
        var request = new UnityWebRequest("https://api.openai.com/v1/moderations", "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            var response = JsonUtility.FromJson<ModerationResponse>(request.downloadHandler.text);
            if(response.results[0].flagged)
            {
                output.text = voilate;
            }
            else
            {
                output.text = notVoilate;
            }
        }
    }

    [System.Serializable]
    private class ModerationResponse
    {
        public string id;
        public string model;
        public ModerationResult[] results;
    }

    [System.Serializable]
    private class ModerationResult
    {
        public ModerationCategories categories;
        public ModerationCategoryScores category_scores;
        public bool flagged;
    }

    [System.Serializable]
    private class ModerationCategories
    {
        public bool hate;
        public bool hate_threatening;
        public bool self_harm;
        public bool sexual;
        public bool sexual_minors;
        public bool violence;
        public bool violence_graphic;
    }

    [System.Serializable]
    private class ModerationCategoryScores
    {
        public float hate;
        public float hate_threatening;
        public float self_harm;
        public float sexual;
        public float sexual_minors;
        public float violence;
        public float violence_graphic;
    }
}
