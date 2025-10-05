using UnityEngine;
using TMPro;

public class FundManager : MonoBehaviour
{
    public static FundManager Instance;

    public int currentFunds = 0;

    [Header("UI")]
    public TextMeshProUGUI fundsText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddFunds(int amount)
    {
        currentFunds += amount;
        UpdateUI();
        Debug.Log("Funds added: " + amount + ", Total: " + currentFunds);
    }

    private void UpdateUI()
    {
        if (fundsText != null)
        {
            fundsText.text = "Funds: " + currentFunds.ToString();
        }
    }
}