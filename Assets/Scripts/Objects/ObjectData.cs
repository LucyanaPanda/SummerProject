using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objects", menuName = "Objects/Create New Object")]
public class ObjectData : ScriptableObject
{
    public string NameProduct;
    public int PriceSell;
    public int PriceBuy;
    public Sprite Icon;
    public Mesh ModelObject;
    public List<Material> Materials;
}

public enum DataTypeObject
{
    Name,
    PriceSell,
    PriceBuy,
    Icon,
    ModelObject,
    Materials
}