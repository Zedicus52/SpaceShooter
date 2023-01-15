namespace SpaceShooter.Core
{
    [System.Serializable]
    public struct Pair<T>
    {
        public T Item { get; private set; }
        public float Weight { get; private set; }

        public Pair(T item, float weight)
        {
            Item = item;
            Weight = weight;
        }
    }
}