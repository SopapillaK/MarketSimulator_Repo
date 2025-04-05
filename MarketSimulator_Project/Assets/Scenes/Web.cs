using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;




public class Web : MonoBehaviour
{
    void Start()
    {

        //StartCoroutine(GetDate("http://localhost/UnityBackendTutorial/GetDate.php"));
        StartCoroutine(GetUsers());
        StartCoroutine(Login("annabelle", "ruckle"));
        StartCoroutine(RegisterUser("testuser", "123456"));

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
				//Show results as text
				Debug.Log(www.downloadHandler.text);

				// Or retrieve results as binary data
				byte[] results = www.downloadHandler.data;
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
                //Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MarketSimulator/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Main.Instance.UserInfo.SetCredentials(username, password);
               // Main.Instance.UserInfo.SetID(www.downloadHandler.text);
            }
            //If we logged in correctly
                if (www.downloadHandler.text.Contains("Wrong Credentials") || www.downloadHandler.text.Contains("Username does not exist"))
                {
                    Debug.Log("Try Again");
                }
            else
            {
                Debug.Log("login success!");
               // Main.Instance.UserProfile.SetActive(true);
               // Main.Instance.Login.gameObject.SetActive(false);
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

}