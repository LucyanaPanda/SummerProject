using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]

public abstract class Item : MonoBehaviour, ILootableObject
{
    [SerializeField] protected ObjectData data;
    protected MeshRenderer meshRenderer;
    protected MeshFilter meshFilter;
    protected MeshCollider meshCollider;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        meshFilter.mesh = data.ModelObject;
        meshRenderer.materials = data.Materials.ToArray();
        print("hallo"); 
    }

    public abstract ObjectData Loot();

    #region GetData
    public int GetData(DataTypeObject type)
    {
        return type switch
        {
            DataTypeObject.PriceSell => data.PriceSell,
            DataTypeObject.PriceBuy => data.PriceBuy,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public string GetDataString(DataTypeObject type)
    {
        return type switch
        {
            DataTypeObject.Name => data.NameProduct,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public Sprite GetDataSprite(DataTypeObject type)
    {
        return type switch
        {
            DataTypeObject.Icon => data.Icon,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public Mesh GetDataMesh(DataTypeObject type)
    {
        return type switch
        {
            DataTypeObject.ModelObject => data.ModelObject,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public Material[] GetDataMaterials(DataTypeObject type)
    {
        return type switch
        {
            DataTypeObject.Materials => data.Materials.ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
    #endregion
}
