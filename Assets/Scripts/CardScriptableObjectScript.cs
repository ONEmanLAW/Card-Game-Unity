using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObjectScript", menuName = "Scriptable Objects/CardScriptableObjectScript")]
public class CardScriptableObjectScript : ScriptableObject
{
    public string cardName;
    public int cost;
    public int atk;
    public int def;
}
