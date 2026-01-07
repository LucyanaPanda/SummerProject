using UnityEngine;

public class Product : Item
{
    float initialMass;

    protected override void Awake()
    {
        base.Awake();
    }

    public override ObjectData Loot()
    {
        meshCollider.enabled = false;
        meshRenderer.enabled = false;

        initialMass = rb.mass;
        rb.mass = 0f;

        return data;
    }
}
