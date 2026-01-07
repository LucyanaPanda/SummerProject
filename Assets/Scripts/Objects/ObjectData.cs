using UnityEngine;

[CreateAssetMenu(fileName = "Objects", menuName = "Objects/Create New Object")]
public class ObjectData : ScriptableObject
{
    public uint ID;
    public string NameProduct;
    public int PriceSell;
    public int PriceBuy;
    public Sprite Icon;
}