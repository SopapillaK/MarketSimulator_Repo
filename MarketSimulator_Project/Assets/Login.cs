using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public GameObject MarketPanelUI;
    public TextMeshProUGUI UsernameText;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI ShoppingListText;
    public List<GameObject> allPossibleItems;
    private List<string> shoppingList = new List<string>();

    public List<string> GetShoppingList()
{
    return shoppingList;
}

    void Start()
    {
        LoginButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text, (username, coins) => {
                DisplayUserData(username, coins);
                MarketPanelUI.SetActive(true);
                this.gameObject.SetActive(false);
            }));
        });
    }

    void DisplayUserData(string username, string coins)
    {
        UsernameText.text = username;
        CoinText.text = coins + " coins";
        GenerateShoppingList();
    }

    void GenerateShoppingList()
    {
        shoppingList.Clear();
        ShoppingListText.text = "Shopping List:\n";

        int listSize = Random.Range(2, 5);
        List<GameObject> tempPool = new List<GameObject>(allPossibleItems); //copying all items 

        for (int i = 0; i < listSize && tempPool.Count > 0; i++)
        {
            int index = Random.Range(0, tempPool.Count);
            GameObject item = tempPool[index];
            string itemName = item.name;

            shoppingList.Add(itemName); //adding to list 
            ShoppingListText.text += "- " + itemName + "\n"; //ui
            tempPool.RemoveAt(index); //removing item 
        }
    }

    //checking if item is on shopping list 

    public bool IsItemInShoppingList(string itemName)
    {
        return shoppingList.Contains(itemName);
    }

    public void UpdateCoins(string newCoinAmount)
    {
        CoinText.text = newCoinAmount + " coins";
    }
}
