using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public Web web;
    private Login loginScript;

    private string userID => loginScript != null ? loginScript.currentUserID : "0";

//holy dictionary 
    private Dictionary<string, int> itemPrices = new Dictionary<string, int>()
    {
        { "1", 2 }, { "2", 2 }, { "3", 2 }, { "4", 3 }, { "5", 3 },
        { "6", 1 }, { "7", 2 }, { "8", 2 }, { "9", 1 }, { "10", 2 }
    };

    void Start()
    {
        if (Main.Instance != null)
        {
            loginScript = Main.Instance.loginScript;
            web = Main.Instance.Web; 
        }
        else
        {
            Debug.LogWarning("Login script where are you?");
        }
    }
    
    //checking if item is in the customer's shopping list
    private bool CanBuy(string itemName)
    {
        if (loginScript == null)
        {
            Debug.LogWarning("NO LOGIN REFERENCE");
            return false;
        }

        return loginScript.IsItemInShoppingList(itemName);
    }

    private void TryBuy(string itemName, string itemID)
    {
        if (CanBuy(itemName))
        {
            //allowed to buy send request to server
            StartCoroutine(web.BuyItem(userID, itemID, (newCoins) =>
            {
                if (!string.IsNullOrEmpty(newCoins) && int.TryParse(newCoins, out _))
                {
                    loginScript.UpdateCoins(newCoins);
                    loginScript.DecrementItemQuantity(itemName);

                    if (itemPrices.TryGetValue(itemID, out int price))
                    {
                        loginScript.AddToCashierEarnings(price);
                    }
                }
                else
                {
                    Debug.LogWarning("NOT ENOUGH: " + newCoins);
                }
            }));
        }
        else
        {
            loginScript.ShowAngryCustomer(); 
        }
    }

    //buttonTime!
    public void OnBuyApplePressed() => TryBuy("Apple", "1");
    public void OnBuyPearPressed() => TryBuy("Pear", "2");
    public void OnBuyBananaPressed() => TryBuy("Banana", "3");
    public void OnBuyKiwiPressed() => TryBuy("Kiwi", "4");
    public void OnBuyStrawberryPressed() => TryBuy("Strawberry", "5");
    public void OnBuyPepperPressed() => TryBuy("Pepper", "6");
    public void OnBuyBroccoliPressed() => TryBuy("Broccoli", "7");
    public void OnBuyCeleryPressed() => TryBuy("Celery", "8");
    public void OnBuyLettucePressed() => TryBuy("Lettuce", "9");
    public void OnBuyCarrotPressed() => TryBuy("Carrot", "10");
}