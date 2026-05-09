using Lucyana.Objects.Items;

namespace Lucyana.Objects.Products
{
    public class Product : Item
    {
        public float initialMass;

        public override ObjectData Loot()
        {
            meshCollider.enabled = false;
            meshRenderer.enabled = false;

            initialMass = rb.mass;
            rb.mass = 0f;

            return data;
        }
    }
}
