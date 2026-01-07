using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
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
        meshCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
    }

    public abstract ObjectData Loot();

    #region GetData
    public ObjectData GetData()
    {
        return data;
    }
    #endregion
}
