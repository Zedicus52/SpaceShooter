using UnityEngine;

namespace SpaceShooter.DataStructures
{
    public static class ScreenBounds
    {
        public static float LeftSide
        {
            get
            {
                if (_leftSide == 0)
                {
                    InitializeBounds();
                    return _leftSide;
                }

                return _leftSide;
            }
        }
        public static float RightSide
        {
            get
            {
                if (_rightSide == 0)
                {
                    InitializeBounds();
                    return _rightSide;
                }

                return _rightSide;
            }
        }
        public static float UpSide
        {
            get
            {
                if (_upSide == 0)
                {
                    InitializeBounds();
                    return _upSide;
                }

                return _upSide;
            }
        }
        public static float DownSide
        {
            get
            {
                if (_downSide == 0)
                {
                    InitializeBounds();
                    return _downSide;
                }

                return _downSide;
            }
        }

        private static float _leftSide;
        private static float _rightSide;
        private static float _upSide;
        private static float _downSide;

        private static void InitializeBounds()
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            _leftSide = -vec.x;
            _rightSide = vec.x;
            _upSide = vec.y;
            _downSide = -vec.y;
        }
    }
}