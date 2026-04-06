using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencySlotUI : MonoBehaviour
{
    [SerializeField] private string _currencyId;
    public Image icon;
    public TMP_Text currencyStackText;

    [Header("Getter")]
    public string currencyId { get => _currencyId; }

    public void Awake()
    {
        if(icon == null) icon = GetComponentInChildren<Image>();
        if(currencyStackText == null) currencyStackText = GetComponentInChildren<TMP_Text>();
    }

    public void UpdateUI(int currentStack)
    {
        currencyStackText.text = currentStack.ToString();
    }
}
