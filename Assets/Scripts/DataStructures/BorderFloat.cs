using System;

namespace SpaceShooter.DataStructures
{
    [Serializable]
    public class BorderFloat : Border<float>
    {
        public BorderFloat() : this(0.0f, 0.0f, 0.0f, 0.0f)
        {
        }

        public BorderFloat(float leftSide, float rightSide, float downSide, float upSide)
        {
            LeftSide = leftSide;
            RightSide = rightSide;
            UpSide = upSide;
            DownSide = downSide;
        }

        #region Expends for sides
        public override void ExpendLeftSide(float value)
        {
            if (LeftSide < 0)
                LeftSide -= value;
            else
                LeftSide += value;
        }

        public override void ExpendRightSide(float value)
        {
            if (RightSide < 0)
                RightSide -= value;
            else
                RightSide += value;
        }

        public override void ExpendUpSide(float value)
        {
            if (UpSide < 0)
                UpSide -= value;
            else
                UpSide += value;
        }

        public override void ExpendDownSide(float value)
        {
            if (DownSide < 0)
                DownSide -= value;
            else
                DownSide += value;
        }
        #endregion

        #region Narrow down for regions

        public override void NarrowLeftSide(float value)
        {
            if (LeftSide < 0)
                LeftSide += value;
            else
                LeftSide -= value;
        }

        public override void NarrowRightSide(float value)
        {
            if (RightSide < 0)
                RightSide += value;
            else
                RightSide -= value;
        }

        public override void NarrowUpSide(float value)
        {
            if (UpSide < 0)
                UpSide += value;
            else
                UpSide -= value;
        }

        public override void NarrowDownSide(float value)
        {
            if (DownSide < 0)
                DownSide += value;
            else
                DownSide -= value;
        }

        #endregion
        
    }
}