using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObjectScript", menuName = "Scriptable Objects/CardScriptableObjectScript")]
public class CardScriptableObjectScript : ScriptableObject
{
    public string name;
    public int cost;
    public int atk;
    public int def;
}
