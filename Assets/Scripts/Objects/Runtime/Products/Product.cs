using Lucyana.Objects.Items;

namespace Lucyana.Objects.Products
{
    public class Product : Item
    {
        public override ObjectData Loot()
        {
            meshCollider.enabled = false;
            meshRenderer.enabled = false;

            rb.mass = 0f;

            return data;
        }
    }
}
