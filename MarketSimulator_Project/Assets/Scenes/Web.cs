using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class Web : MonoBehaviour
{
    public IEnumerator Login(string username, string password, Action<string, string, string> onSuccess)
    {
        WWWForm form = new WWWForm(); 
        form.AddField("loginUser", username); 
        form.AddField("loginPass", password); 

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MarketSimulator/Login.php", form))
        {
            yield return www.SendWebRequest(); 

            if (www.result == UnityWebRequest.Result.Success)
            {
                string result = www.downloadHandler.text; //getting reply
                Debug.Log("[Login] Response: " + result);

                //get user info from JSON
                string userID = GetJsonValue(result, "userID");
                string money = GetJsonValue(result, "money");
                string usernameResponse = GetJsonValue(result, "username");

                if (!string.IsNullOrEmpty(userID))
                {
                    onSuccess?.Invoke(usernameResponse, money, userID); 
                }
                else
                {
                    Debug.LogWarning("[Login] Failed: userID not found!");
                }
            }
            else
            {
                Debug.LogError("Login error: " + www.error); 
            }
        }
    }

    //buying item from server
    public IEnumerator BuyItem(string userID, string itemID, Action<string> onCoinsUpdated)
    {
        if (string.IsNullOrEmpty(userID))
        {
            Debug.LogError("userID is null or empty ");
            yield break; 
        }

        WWWForm form = new WWWForm(); 
        form.AddField("userID", userID); 
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MarketSimulator/BuyItem.php", form))
        {
            yield return www.SendWebRequest(); 

            if (www.result == UnityWebRequest.Result.Success)
            {
                string result = www.downloadHandler.text; 
                Debug.Log("[BuyItem] Server Response: " + result);

                string newCoins = GetJsonValue(result, "newCoins"); //get new coins

                if (!string.IsNullOrEmpty(newCoins) && int.TryParse(newCoins, out _))
                {
                    onCoinsUpdated?.Invoke(newCoins); //new money back
                }
                else
                {
                    Debug.LogWarning($"[BuyItem] Failed 'newCoins': '{newCoins}' server says: {result}");
                }
            }
            else
            {
                Debug.LogError("BuyItem Error: " + www.error); 
            }
        }
    }

    //help find a value in a JSON string
    private string GetJsonValue(string json, string key)
    {
        string pattern = $"\"{key}\":\"?(.*?)\"?(,|\\}})"; 
        var match = System.Text.RegularExpressions.Regex.Match(json, pattern);
        return match.Success ? match.Groups[1].Value : ""; //return found value
    }
}

   
    /*public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {
        string uri = "http://localhost/UnityBackendTutorial/" + "GetItemsIDs.php";
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/GetItemsIDs.php", form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Show results as text
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    //Debug.Log(webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    // Call callback function to pass results
                    callback(jsonArray);
                    break;
            }
        }
    }
*/


   /* public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        string uri = "http://localhost/UnityBackendTutorial/" + "GetItem.php";
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/GetItem.php", form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Show results as text
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    // Call callback function to pass results
                    callback(jsonArray);
                    break;
            }
        }
    }
    */

