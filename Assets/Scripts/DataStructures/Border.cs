namespace SpaceShooter.DataStructures
{
    public abstract class Border<T>
    {
        public T LeftSide { get; protected set; }
        public T RightSide { get; protected set; }
        public T UpSide { get; protected set; }
        public T DownSide { get; protected set; }

        public abstract void ExpendLeftSide(T value);
        public abstract void ExpendRightSide(T value);
        public abstract void ExpendUpSide(T value);
        public abstract void ExpendDownSide(T value);
        
        public abstract void NarrowLeftSide(T value);
        public abstract void NarrowRightSide(T value);
        public abstract void NarrowUpSide(T value);
        public abstract void NarrowDownSide(T value);
    }
}