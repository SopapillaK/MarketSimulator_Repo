using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions;

public class Web : MonoBehaviour
{
    void Start()
    {
        //tbc
    }

    IEnumerator GetDate()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/MarketSimulator/GetDate.php"))
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

    IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/MarketSimulator/GetUsers.php"))
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

    public IEnumerator Login(string username, string password, System.Action<string, string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MarketSimulator/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Login Error: " + www.error);
            }
            else
            {
                string result = www.downloadHandler.text;
                if (result.Contains("Wrong Credentials"))
                {
                    Debug.Log("Wrong Credentials");
                }
                else
                {
                    Debug.Log("Login Success! " + result);
                    string user = GetJsonValue(result, "username");
                    string money = GetJsonValue(result, "money");
                    callback?.Invoke(user, money);
                }
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MarketSimulator/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

   public IEnumerator BuyItem(string userID, string itemID, System.Action<string> onCoinsUpdated = null)
{
    WWWForm form = new WWWForm();
    form.AddField("userID", userID);
    form.AddField("itemID", itemID);

    using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MarketSimulator/BuyItem.php", form))
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Buy Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            //new coin amount from server
            string newCoins = GetJsonValue(www.downloadHandler.text, "newCoins");

            //update the ui
            onCoinsUpdated?.Invoke(newCoins);
        }
    }
}

    private string GetJsonValue(string json, string key)
    {
        string pattern = $"\"{key}\":\"?(.*?)\"?(,|\\}})";
        var match = Regex.Match(json, pattern);
        return match.Success ? match.Groups[1].Value : "";
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

