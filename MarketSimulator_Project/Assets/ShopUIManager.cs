using System.Collections;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public Web web;
    public string userID = "1"; //dynamically set after login

    //fruits
    public void OnBuyApplePressed()
    {
        StartCoroutine(web.BuyItem(userID, "1")); //apples
    }

    public void OnBuyPearPressed()
    {
        StartCoroutine(web.BuyItem(userID, "2")); //pears
    }

    public void OnBuyBananaPressed()
    {
        StartCoroutine(web.BuyItem(userID, "3")); //bananas
    }

    public void OnBuyKiwiPressed()
    {
        StartCoroutine(web.BuyItem(userID, "4")); //kiwis
    }

    public void OnBuyStrawberryPressed()
    {
        StartCoroutine(web.BuyItem(userID, "5")); //strawberries
    }

    //vegetables
    public void OnBuyPepperPressed()
    {
        StartCoroutine(web.BuyItem(userID, "6")); //peppers
    }

    public void OnBuyBroccoliPressed()
    {
        StartCoroutine(web.BuyItem(userID, "7")); //broccoli
    }

    public void OnBuyCeleryPressed()
    {
        StartCoroutine(web.BuyItem(userID, "8")); //celery
    }

    public void OnBuyLettucePressed()
    {
        StartCoroutine(web.BuyItem(userID, "9")); //lettuce
    }

    public void OnBuyCarrotPressed()
    {
        StartCoroutine(web.BuyItem(userID, "10")); //carrots
    }
}
