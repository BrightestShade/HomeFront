using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public int currencyCount;
    public TextMeshProUGUI currencyText;

    void Update()
    {
        currencyText.text = "Currency Count: " + currencyCount.ToString();
    }
}
