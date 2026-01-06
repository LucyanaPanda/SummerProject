using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public abstract class Item : MonoBehaviour, ILootableObject
{
    [SerializeField] protected ObjectData data;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    protected virtual void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer.material.mainTexture = data.Icon.texture;
        meshFilter.mesh = data.ModelObject;
    }

    protected abstract int SellProduct();

    protected abstract bool StockIt(/*Inventory inventory*/);

    public abstract void Loot();
    public abstract void Drop();
}
