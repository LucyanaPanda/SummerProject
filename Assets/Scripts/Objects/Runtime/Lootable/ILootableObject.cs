namespace Lucyana.Objects
{
    public interface ILootableObject<T> where T : class
    {
        T Loot();
    }
}
