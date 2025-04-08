using System.Collections;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public Web web;
    public string userID = "1"; //dynamically set

    public void OnBuyApplePressed()
    {
        StartCoroutine(web.BuyItem(userID, "1")); // Apples
    }

    public void OnBuyPearPressed()
    {
        StartCoroutine(web.BuyItem(userID, "2")); // Pears
    }

    public void OnBuyBananaPressed()
    {
        StartCoroutine(web.BuyItem(userID, "3")); // Bananas
    }

    public void OnBuyKiwiPressed()
    {
        StartCoroutine(web.BuyItem(userID, "4")); // Kiwis
    }

    public void OnBuyStrawberryPressed()
    {
        StartCoroutine(web.BuyItem(userID, "5")); // Strawberries
    }
}
