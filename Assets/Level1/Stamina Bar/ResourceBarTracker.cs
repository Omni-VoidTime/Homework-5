using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[SelectionBase]
public class ResourceBarTracker : MonoBehaviour
{
    [Header("Core Settings")]
    [SerializeField] private Image bar;
    [SerializeField] private int resourceCurrent = 100;
    [SerializeField] private int resourceMax = 100;
    [SerializeField] private int resourceAbsoluteMax = 1000;
    [SerializeField] private bool overkillPossible;
    private float timer = 0;


    private void Start()
    {
        UpdateBarAndResourceText();
    }

    private void Update()
    {
        if (timer < 5)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            ChangeResourceByAmount(5);
            timer = 0;
        }
    }
    private void UpdateBarAndResourceText()
    {
        if (resourceMax <= 0)
        {
            bar.fillAmount = 0;
            return;
        }

        float fillAmount = (float)resourceCurrent / resourceMax;

        bar.fillAmount = fillAmount;
    }

    public bool ChangeResourceByAmount(int amount)
    {
        if (!overkillPossible && resourceCurrent + amount < 0)
            return false;
        resourceCurrent += amount;
        resourceCurrent = Mathf.Clamp(resourceCurrent, 0, resourceMax);

        bar.fillAmount = (float)resourceCurrent / resourceMax;

        return true;
    }

    public int CurrentResource()
    {
        return resourceCurrent;
    }
}

