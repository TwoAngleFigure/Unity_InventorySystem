using UnityEngine;

[CreateAssetMenu(fileName = "New Currency", menuName = "SO/Currency")]
public class Currency : ScriptableObject
{
    public string currencyName;
    public string description;
    public int value;
    public int stack;

    public Sprite icon;
}