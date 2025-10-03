using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI seedsText;

    int money;
    int seeds;
    int price = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        money = 16;
        seeds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        moneyText.text = "$" + money;
        seedsText.text = "Seeds: " + seeds;
    }

    public void BuySeed()
    {
        if(money >= price)
        {
            money -= price;
            seeds++;
        }
    }

    public void LeaveShop()
    {

        //todo: add code here to set the seeds of the player to the value of the seeds int
        //do this by making the player's seeds (in whatever class those are stored) a public static variable


        //change scene
        SceneManager.LoadScene("Scene3-Plant");
    }
}
