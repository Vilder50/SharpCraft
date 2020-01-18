using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void TestVector()
        {
            Vector vector = new Vector(1,2,3);
            Assert.AreEqual(1, vector.X, "X wasn't set by constructor");
            Assert.AreEqual(2, vector.Y, "Y wasn't set by constructor");
            Assert.AreEqual(3, vector.Z, "Z wasn't set by constructor");

            Assert.AreEqual("1.1 2.2 3.3", new Vector(1.1,2.2,3.3).GetVectorString(), "GetVectorString doesn't return correct string");
        }

        [TestMethod]
        public void TestVectorOperators()
        {
            Assert.AreEqual("7 9 13", (new Vector(1, 2, 3) + new Vector(6, 7, 10)).GetVectorString(), "Vectors aren't added together correctly");
            Assert.AreEqual("-5 -5 -7", (new Vector(1, 2, 3) - new Vector(6, 7, 10)).GetVectorString(), "Vectors aren't subtracted from each other correctly");
            Assert.AreEqual("6 14 30", (new Vector(1, 2, 3) * new Vector(6, 7, 10)).GetVectorString(), "Vectors aren't mulitplied together correctly");
            Assert.AreEqual("2 6 22", (new Vector(12, 42, 220) / new Vector(6, 7, 10)).GetVectorString(), "Vectors aren't divided from each other correctly");

            Assert.AreEqual("6 12 18", (new Vector(1, 2, 3) * 6).GetVectorString(), "Vectors aren't multiplied correctly with number");
            Assert.AreEqual("1 2 3", (new Vector(6, 12, 18) / 6).GetVectorString(), "Vectors aren't divided correctly with number");
        }

        [TestMethod]
        public void TestNumberToDirection()
        {
            //test no ignore
            List<Vector> vectors = new List<Vector>();
            for (int i = 0; i < 6; i++)
            {
                Vector newVector = Vector.NumberToDirection(i);
                foreach (Vector vector in vectors)
                {
                    if ((vector.X == newVector.X && vector.X != 0) || (vector.Y == newVector.Y && vector.Y != 0) || (vector.Z == newVector.Z && vector.Z != 0))
                    {
                        Assert.Fail("NumberToDirection doesn't return 6 different vectors");
                    }
                }
                vectors.Add(newVector);
            }

            //test ignore
            for (int i = 0; i < 4; i++)
            {
                Vector newVector = Vector.NumberToDirection(i, ID.Axis.y);
                foreach (Vector vector in vectors)
                {
                    if (newVector.Y != 0)
                    {
                        Assert.Fail("NumberToDirection didn't ignore the given direction");
                    }
                }
                vectors.Add(newVector);
            }

            //test exceptions
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Vector.NumberToDirection(-1), "NumberToDirection less than 0 didn't throw exception");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Vector.NumberToDirection(6), "NumberToDirection higher than 5 didn't throw exception");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Vector.NumberToDirection(4, ID.Axis.z), "Ignore NumberToDirection higher than 3 didn't throw exception");
        }

        [TestMethod]
        public void TestConvertToObject()
        {
            Assert.AreEqual("{a:0.1d,b:2d,c:0.3d}", new Vector(0.1, 2, 0.3).GetAsDataObject(new object[] { "a", "b", "c" }).GetDataString(), "VectorDouble.GetAsDataObject didn't return correct object");
            Assert.AreEqual("{\"a\":0.1,\"b\":0.2,\"c\":0.3}", new Vector(0.1, 0.2, 0.3).GetAsDataObject(new object[] { "a", "b", "c", true }).GetDataString(), "VectorDouble.GetAsDataObject didn't return correct json object");
            Assert.AreEqual("{a:1,b:2,c:3}", new IntVector(1,2,3).GetAsDataObject(new object[] { "a", "b", "c" }).GetDataString(), "VectorInt.GetAsDataObject didn't return correct object");
            Assert.AreEqual("{\"a\":1,\"b\":2,\"c\":3}", new IntVector(1, 2, 3).GetAsDataObject(new object[] { "a", "b", "c", true }).GetDataString(), "VectorInt.GetAsDataObject didn't return correct json object");
        }

        [TestMethod]
        public void TestConvertToArray()
        {
            Assert.AreEqual("[1.1d,2.2d,3.3d]", new Vector(1.1,2.2,3.3).GetAsArray(null,null).GetDataString(), "VectorDouble.GetAsDataArray didn't return correct object");
            Assert.AreEqual("[I;1,2,3]", new IntVector(1,2,3).GetAsArray(null, null).GetDataString(), "VectorInt.GetAsDataArray didn't return correct object");
        }

        [TestMethod]
        public void TestCoords()
        {
            Coords normalCoords = new Coords(1, 2, 3, false, false, false);
            Assert.AreEqual("1 2 3", normalCoords.GetVectorString(), "NormalCoords vector string wasn't correct");
            normalCoords = new Coords(false, 1, 2, 3);
            Assert.AreEqual("1 2 3", normalCoords.GetVectorString(), "NormalCoords vector string wasn't correct");

            Coords relativeCoords = new Coords(1, 2, 3);
            Assert.AreEqual("~1 ~2 ~3", relativeCoords.GetVectorString(), "RelativeCoords vector string wasn't correct");
            relativeCoords = new Coords(true, 1, 2, 3);
            Assert.AreEqual("~1 ~2 ~3", relativeCoords.GetVectorString(), "RelativeCoords vector string wasn't correct");
            relativeCoords = new Coords();
            Assert.AreEqual("~ ~ ~", relativeCoords.GetVectorString(), "RelativeCoords vector string wasn't correct");

            Coords mixedCoords = new Coords(0.9, 0, -0.9,true, false, true);
            Assert.AreEqual("~.9 0 ~-.9", mixedCoords.GetVectorString(), "GetVectorString doesn't minimize coords correctly");

            Assert.IsTrue(new Coords(0, 0, 0, true, false, true).SameRelativeCoords(new Coords(1, 2, 3, true, false, true)), "SameRelativeCoords should return true since both coords are relative in same places");
            Assert.IsFalse(new Coords(0, 0, 0, true, false, true).SameRelativeCoords(new Coords(1, 2, 3, true, true, true)), "SameRelativeCoords should return false since both coords are not relative in same places");
        }

        [TestMethod]
        public void TestLocalCoords()
        {
            LocalCoords coords = new LocalCoords(1.1, 2.2, 3.3);
            Assert.AreEqual("^1.1 ^2.2 ^3.3", coords.GetVectorString(), "Coords vector string wasn't correct");

            coords = new LocalCoords(0.1, 0, -0.1);
            Assert.AreEqual("^.1 ^ ^-.1", coords.GetVectorString(), "GetVectorString doesn't minimize coords correctly");
        }
    }
}
