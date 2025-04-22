using System.Collections;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public Web web;
    private Login loginScript; 
    public string userID = "1";

    void Start()
    {
        if (Main.Instance != null && Main.Instance.loginScript != null)
        {
            loginScript = Main.Instance.loginScript;
        }
        else
        {
            Debug.LogWarning("Login script not found");
        }
    }

    private bool CanBuy(string itemName)
    {
        if (loginScript == null)
        {
            Debug.LogWarning("No login refernce!");
            return false;
        }

        string cleanedName = itemName.Trim().ToLower();
        foreach (string item in loginScript.GetShoppingList())
        {
            if (item.Trim().ToLower() == cleanedName)
                return true;
        }

        Debug.Log(itemName + " is not on the shopping list!");
        return false;
    }

    //fruits
    public void OnBuyApplePressed()       => TryBuy("Apple", "1");
    public void OnBuyPearPressed()        => TryBuy("Pear", "2");
    public void OnBuyBananaPressed()      => TryBuy("Banana", "3");
    public void OnBuyKiwiPressed()        => TryBuy("Kiwi", "4");
    public void OnBuyStrawberryPressed()  => TryBuy("Strawberry", "5");

    //veggies
    public void OnBuyPepperPressed()      => TryBuy("Pepper", "6");
    public void OnBuyBroccoliPressed()    => TryBuy("Broccoli", "7");
    public void OnBuyCeleryPressed()      => TryBuy("Celery", "8");
    public void OnBuyLettucePressed()     => TryBuy("Lettuce", "9");
    public void OnBuyCarrotPressed()      => TryBuy("Carrot", "10");

    private void TryBuy(string itemName, string itemID)
    {
        if (CanBuy(itemName))
        {
            StartCoroutine(web.BuyItem(userID, itemID, (newCoins) => {
                loginScript.UpdateCoins(newCoins);
            }));
        }
    }
}
