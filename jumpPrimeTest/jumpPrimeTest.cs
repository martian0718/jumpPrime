/*
 *  Author: Martin Wu
 *  
 *  Created: September 16th, 2020
 *
 *  Modified: September 17th, 2020
 *
 *  FileName: UnitTest1.cs
 *  
 *  Language: C# and Visual Studio
 */


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using P1;

namespace jumpPrimeTest
{
    [TestClass]
    public class jumpPrimeTest
    {
        [TestMethod]
        public void Test_Up()
        {
            //arrange
            Random random = new Random();
            int number = random.Next(1000,10000);
            jumpPrime obj = new jumpPrime(number);
            //act
            
            int expectedValue = number + 1;
            bool notPrime = false;
            while(true)
            {
                for (int i = 2; i < expectedValue / 2; i++)
                    if (expectedValue % i == 0) {
                        notPrime = true;
                        break;
                    }
                if (!notPrime)
                    break;
                expectedValue++;
                notPrime = false;
            }
            //assert
            Assert.AreEqual(expectedValue, obj.up());

        }

        [TestMethod]
        public void Test_Up_into_Inactive()
        {
            //arrange
            int number = 2488;
            jumpPrime obj = new jumpPrime(number);
            int limit = 26; //bounds should be 26 
            //act
            int expectedInt = 0;
            bool expectedState = false;
            for(int i = 0; i < limit; i++)
            {
                obj.up();
            }
            //assert
            Assert.AreEqual(expectedInt, obj.up());
            Assert.AreEqual(expectedState, obj.getActive());
        }

        [TestMethod]
        public void Test_Down()
        {
            //arrange
            Random random = new Random();
            int number = random.Next(1000, 10000);
            jumpPrime obj = new jumpPrime(number);
            //act

            int expectedValue = number-1;
            bool notPrime = false;
            while (true)
            {
                for (int i = 2; i < expectedValue / 2; i++)
                    if (expectedValue % i == 0)
                    {
                        notPrime = true;
                        break;
                    }
                if (!notPrime)
                    break;
                expectedValue--;
                notPrime = false;
            }
            //assert
            Assert.AreEqual(expectedValue, obj.down());
        }

        [TestMethod]
        public void Test_Down_into_Inactive()
        {
            //arrange
            int number = 1000;
            jumpPrime obj = new jumpPrime(number);
            //act
            int expectedInt = 0;
            bool expectedState = false;
            //assert
            Assert.AreEqual(expectedInt, obj.down());
            Assert.AreEqual(expectedState, obj.getActive());
        }

        [TestMethod]
        public void Test_Reset()
        {
            //arrange
            int number = 2488;
            jumpPrime obj = new jumpPrime(number);
            int limit = 14; //uppper prime is 2503 & max number of queries is 26
            //act
            bool expectedState = true;

            for(int i = 0; i < limit; i++)
            {
                obj.up();
            }
            obj.reset();    //num of queries should be zero
            for(int i = 0; i < limit; i++)
            {
                obj.up();
            }
            //even though we query 28 times over the bounds of 26, we reset so it should still be active
            //assert
            Assert.AreEqual(expectedState, obj.getActive());
        }

        [TestMethod]
        public void Test_Revive_shouldBeTrue()
        {
            //arrange
            int number = 2488;
            jumpPrime obj = new jumpPrime(number);
            int limitExceeded = 27;
            //act
            for(int i = 0; i < limitExceeded; i++)
            {
                obj.up();
            }
            bool expectedValue = true;
            obj.revive();
            //assert
            Assert.AreEqual(expectedValue, obj.getActive());
        }

        [TestMethod]
        public void Test_Revive_whenActive_shouldBeFalse()
        {
            //arrange
            int number = 2488;
            jumpPrime obj = new jumpPrime(number);
            //act
            obj.revive();
            bool expectedState = false;
            //assert
            Assert.AreEqual(expectedState, obj.getActive());
        }


    }
}
