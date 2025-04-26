using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    //UI references
    public GameObject MarketPanelUI;
    public TextMeshProUGUI UsernameText;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI ShoppingListText;
    public TextMeshProUGUI CashierText;

    //items
    public List<GameObject> allPossibleItems;
    public string currentUserID;

    //character images
    public Image customerImage;
    public Sprite annabelleSprite;
    public Sprite samSprite;
    public Sprite sophiaSprite;
    public Sprite rickSprite;
    public Sprite allisonSprite;

    //shopping list and money
    private Dictionary<string, int> shoppingList = new Dictionary<string, int>();
    private int cashierEarnings = 0;
    private int totalEarnings = 0;

    //user login info
    private string[] usernames = { "annabelle", "sam", "sophia", "rick", "allison" };
    private string[] passwords = { "ruckle", "123456", "12", "morty", "e" };
    private int currentCustomerIndex = 0;

    public GameObject EndOfDayImage;
    public TextMeshProUGUI EndOfDayText;

    [Header("Angry UI")]
    public GameObject AngryPanel;
    public Image AngryCharacterImage;
    public Image AngryDialogueImage;

    [Header("Angry Sprites")]
    public Sprite annabelleAngrySprite;
    public Sprite samAngrySprite;
    public Sprite sophiaAngrySprite;
    public Sprite rickAngrySprite;
    public Sprite allisonAngrySprite;
    public Sprite angryDialogueSprite;

    [Header("Thank You")]
    public GameObject ThankYouImage;

    //first customer
    IEnumerator Start()
    {
        yield return LoadCustomer(currentCustomerIndex);
    }

    //load customer info
    private IEnumerator LoadCustomer(int index)
    {
        if (index >= usernames.Length)
        {
            Debug.Log("All customers served!");
            ShoppingListText.text = "All orders complete!";
            MarketPanelUI.SetActive(false);

            //end of day panel
            EndOfDayImage.SetActive(true);
            EndOfDayText.text = "Total Day's Earnings: $" + totalEarnings;
            yield break;
        }

        while (Main.Instance == null || Main.Instance.Web == null)
            yield return null;

        string username = usernames[index];
        string password = passwords[index];

        //try login
        yield return Main.Instance.Web.Login(username, password, (user, coins, userID) =>
        {
            currentUserID = userID;
            DisplayUserData(user, coins);
            MarketPanelUI.SetActive(true);
        });
    }

    void DisplayUserData(string username, string coins)
    {
        UsernameText.text = username;
        CoinText.text = coins + " coins";
        CashierText.text = "Total Earned: $" + totalEarnings;

        GenerateShoppingList(); //random shopping list

        //update customer sprite
        switch (username.ToLower())
        {
            case "annabelle": customerImage.sprite = annabelleSprite; break;
            case "sam": customerImage.sprite = samSprite; break;
            case "sophia": customerImage.sprite = sophiaSprite; break;
            case "rick": customerImage.sprite = rickSprite; break;
            case "allison": customerImage.sprite = allisonSprite; break;
            default: Debug.LogWarning("NO SPRITE " + username); break;
        }
    }

    //shopping list time
    void GenerateShoppingList()
    {
        shoppingList.Clear();
        ShoppingListText.text = "SHOPPING LIST:\n";

        int listSize = Random.Range(2, 5);
        List<GameObject> tempPool = new List<GameObject>(allPossibleItems);

        for (int i = 0; i < listSize && tempPool.Count > 0; i++)
        {
            int index = Random.Range(0, tempPool.Count);
            GameObject item = tempPool[index];
            string itemName = item.name;

            int quantity = Random.Range(1, 5);
            shoppingList[itemName] = quantity;

            ShoppingListText.text += $"- {quantity} {itemName}(s)\n";
            tempPool.RemoveAt(index);
        }
    }

    //check if item is on shopping list
    public bool IsItemInShoppingList(string itemName)
    {
        return shoppingList.ContainsKey(itemName) && shoppingList[itemName] > 0;
    }

    //decrement shopping list when buying 
    public void DecrementItemQuantity(string itemName)
    {
        if (shoppingList.ContainsKey(itemName))
        {
            shoppingList[itemName]--;

            if (shoppingList[itemName] <= 0)
                shoppingList.Remove(itemName);

            UpdateShoppingListUI();

            if (shoppingList.Count == 0)
            {
                Debug.Log("Order Complete!");

                if (ThankYouImage != null)
                {
                    ThankYouImage.SetActive(true);
                    StartCoroutine(ProceedToNextCustomerAfterDelay());
                }
            }
        }
    }

    //update shopping list UI
    public void UpdateShoppingListUI()
    {
        ShoppingListText.text = "Shopping List:\n";
        foreach (var item in shoppingList)
        {
            ShoppingListText.text += $"- {item.Value} {item.Key}(s)\n";
        }
    }

    //update coins after buying
    public void UpdateCoins(string newCoinAmount)
    {
        Debug.Log("Updated Money: " + newCoinAmount);
        if (CoinText == null)
        {
            Debug.LogError("NULL");
            return;
        }
        CoinText.text = newCoinAmount + " coins";
    }

    //update cashier earnings
    public void AddToCashierEarnings(int amount)
    {
        cashierEarnings += amount;
        totalEarnings += amount;
        CashierText.text = "$" + cashierEarnings + " earned";
    }

    //angry customer panel
    public void ShowAngryCustomer()
    {
        if (AngryPanel == null) return;

        AngryPanel.SetActive(true);

        //angry face
        switch (UsernameText.text.ToLower())
        {
            case "annabelle": AngryCharacterImage.sprite = annabelleAngrySprite; break;
            case "sam": AngryCharacterImage.sprite = samAngrySprite; break;
            case "sophia": AngryCharacterImage.sprite = sophiaAngrySprite; break;
            case "rick": AngryCharacterImage.sprite = rickAngrySprite; break;
            case "allison": AngryCharacterImage.sprite = allisonAngrySprite; break;
            
        }

        AngryDialogueImage.sprite = angryDialogueSprite;
        StartCoroutine(HideAngryPanelAfterDelay());
    }

    //bye angry panel 
    private IEnumerator HideAngryPanelAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        AngryPanel.SetActive(false);

        //rest character 
        switch (UsernameText.text.ToLower())
        {
            case "annabelle": customerImage.sprite = annabelleSprite; break;
            case "sam": customerImage.sprite = samSprite; break;
            case "sophia": customerImage.sprite = sophiaSprite; break;
            case "rick": customerImage.sprite = rickSprite; break;
            case "allison": customerImage.sprite = allisonSprite; break;
        }
    }

    //moving to next customer thank you
    private IEnumerator ProceedToNextCustomerAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (ThankYouImage != null)
            ThankYouImage.SetActive(false);

        currentCustomerIndex++;
        StartCoroutine(LoadCustomer(currentCustomerIndex));
    }
}
