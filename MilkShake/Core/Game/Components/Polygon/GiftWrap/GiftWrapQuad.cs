using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Tools.Utils;

namespace MilkShakeFramework.Core.Game.Components.Polygon.GiftWrap
{
    public class GiftWrapQuad
    {
        private float _leftRotationOne;
        private float _leftRotationTwo;
        private float _rightRotationOne;
        private float _rightRotationTwo;

        private Vector2 _topLeft;
        private Vector2 _topRight;
        private Vector2 _bottomLeft;
        private Vector2 _bottomRight;

        private Vector2 _pointA;
        private Vector2 _pointB;
        private Vector2 _pointC;
        private Vector2 _pointD;

        public int _collision;

        public GiftWrapQuad(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight, float leftRotationOne, float leftRotationTwo, float rightRotationOne, float rightRotationTwo,
                            Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointD)
        {
            _topLeft = topLeft;
            _topRight = topRight;
            _bottomLeft = bottomLeft;
            _bottomRight = bottomRight;

            _leftRotationOne = leftRotationOne;
            _leftRotationTwo = leftRotationTwo;
            _rightRotationOne = rightRotationOne;
            _rightRotationTwo = rightRotationTwo;

            _pointA = pointA;
            _pointB = pointB;
            _pointC = pointC;
            _pointD = pointD;

            _collision = 0;
        }

        public Vector2 TopLeft { get { return _topLeft; } }
        public Vector2 TopRight { get { return _topRight; } }
        public Vector2 BottomLeft { get { return _bottomLeft; } }
        public Vector2 BottomRight { get { return _bottomRight; } }

        public float LeftRotationOne { get { return _leftRotationOne; } }
        public float LeftRotationTwo { get { return _leftRotationTwo; } }
        public float RightRotationOne { get { return _rightRotationOne; } }
        public float RightRotationTwo { get { return _rightRotationTwo; } }

        public Vector2 PointA { get { return _pointA; } }
        public Vector2 PointB { get { return _pointB; } }
        public Vector2 PointC { get { return _pointC; } }
        public Vector2 PointD { get { return _pointD; } }

        public float Rotation { get { return MathHelper.ToDegrees(MathUtils.AngleBetweenTwoVectors(PointB, PointC)) + 180; } }
    }
}
