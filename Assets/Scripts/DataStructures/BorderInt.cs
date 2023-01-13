namespace SpaceShooter.DataStructures
{
    public class BorderInt : Border<int>
    {
        public BorderInt() : this(0, 0, 0, 0)
        {
        }

        public BorderInt(int leftSide, int rightSide, int downSide, int upSide)
        {
            LeftSide = leftSide;
            RightSide = rightSide;
            UpSide = upSide;
            DownSide = downSide;
        }
        #region Expends for sides
        public override void ExpendLeftSide(int value)
        {
            if (LeftSide < 0)
                LeftSide -= value;
            else
                LeftSide += value;
        }

        public override void ExpendRightSide(int value)
        {
            if (RightSide < 0)
                RightSide -= value;
            else
                RightSide += value;
        }

        public override void ExpendUpSide(int value)
        {
            if (UpSide < 0)
                UpSide -= value;
            else
                UpSide += value;
        }

        public override void ExpendDownSide(int value)
        {
            if (DownSide < 0)
                DownSide -= value;
            else
                DownSide += value;
        }
        #endregion

        #region Narrow down for regions

        public override void NarrowLeftSide(int value)
        {
            if (LeftSide < 0)
                LeftSide += value;
            else
                LeftSide -= value;
        }

        public override void NarrowRightSide(int value)
        {
            if (RightSide < 0)
                RightSide += value;
            else
                RightSide -= value;
        }

        public override void NarrowUpSide(int value)
        {
            if (UpSide < 0)
                UpSide += value;
            else
                UpSide -= value;
        }

        public override void NarrowDownSide(int value)
        {
            if (DownSide < 0)
                DownSide += value;
            else
                DownSide -= value;
        }

        #endregion

    }
}