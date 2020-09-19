/*
 *  Author: Martin Wu
 *  
 *  Created: September 10th, 2020
 *
 *  Modified: September 17th, 2020
 *
 *  FileName: jumpPrime.cs
 *  
 *  Language: C# using Visual Studio
 */

using System;

namespace P1
{
    /*  Interface Invariant
     * 
     *  Some constraints that we have for the jumpPrime object is that the number it holds must be
     *  at least 4 digits long and yield the prime numbers closes to that number. There are also some expected use of public functions such as 
     *  up(), down(), reset(), revive(), and getActive(). The jumpPrime object will be active unless the number of queries exceeds its limit,
     *  the number we are querying is less than four digits long, or if one tries to revive an already active object which in that case will 
     *  permanately deactivate it (not allowing it to become active again). 
     *  
     *  Client must provide a digit that is at least of 4 digits length to the jumpPrime object to provide it, its initial number or state. If
     *  an invalid number is given, the object will be given the number 5000 as its default inital state.
     *  
     *  up() will return the next prime number above our current number, but if it is in an inactive state it will just return 0
     *  down() will return the next prime number right below our current number, but if the jumpPrime object is in an inactive state it will return 0
     *  reset() can be used to reset the jumpPrime object but only if it is currently active, it if is inactive nothing will happen
     *  revive() can only be used to bring back an inactive object back to an active state. If revive() is called when the object is already
     *      active, then the jumpPrime object will become permanately inactive or in other terms deactivated. 
     *  getActive() will return the current state of the object 
     *
     */
    
    public class jumpPrime
    {
        private int number;
        private int upperPrime;
        private int lowerPrime;
        private int numOfQueries;
        private int maxQueries;
        private bool active;
        private bool deactivated;

        private const int min_number = 1000;
        private const int default_number = 5000;
        
        public jumpPrime(int number)
        {
            active = true;
            this.number = (number >= min_number) ? number : default_number;
            upperPrime = nextUpperPrime(this.number);
            lowerPrime = nextLowerPrime(this.number);
            numOfQueries = 0;
            maxQueries = upperPrime - lowerPrime;
            deactivated = false;
        }
        //Pre-Condition: Encapsulated number must be 4 digits long & must be Active in order to return next prime number
        //               Number of queries have to be less than bound of queries
        //Post-Condition: Number will increment, number of queries will increment
        //                & if active return upperPrime otherwise return 0
        //
        //                State could become inactive if number is below 1000 or if number of queries exceeds limit
        public int up()
        {
            if(jumpUp())
            {
                return upperPrime = nextUpperPrime(number);
            }

            return 0;
        }
        //Pre-Condition: Encapuslated number must be 4 digits long & must be Active in order to return next low prime number
        //               Number of queries have to be less than bound of queries
        //Post-Condition: Number will decrement, number of queries will increment,
        //                & if active return lowerPrime otherwise return 0
        //
        //                State could become inactive if you try to decrement when number is at 1000 or if number of query exceeds limit
        public int down()
        {
            if(jumpDown())
            {
                return lowerPrime = nextLowerPrime(number);
            }

            return 0;
        }
        //Pre-Condition: State should be Active 
        //Post-Condition: numOfQueries will be reset to 0
        //                maxQueries will be reset based off the updated upperPrime & updated lowerPrime
        //
        //                Nothing happens if you try to reset() when state is inactive
        public void reset()
        {
            if(active)
            {
                upperPrime = nextUpperPrime(number);
                lowerPrime = nextLowerPrime(number);
                numOfQueries = 0;
                maxQueries = upperPrime - lowerPrime;
            }
        }
        //Pre-Condition: Must be inactive and cannot be deactivated and is a mutator function which controls state 
        //Post-Condition: It will become active while also having numOfQueries reset to 0 and maxQueries being reset based off the updated upperPrime & updated lowerPrime
        //                If Pre-Condition wasn't followed, object becomes Permanetly Deactivated
        public void revive() {

            if (deactivated)
                return;

            active = !active;

            if (active)
                reset();
            else
                deactivated = true;
        }

        public bool getActive()
        {
            return active;
        }
        
        //Pre-Condition: Must be Active 
        //Post-Condition: Number must be over 1000 && numOfQueries < maxQueries otherwise it will be inactive, query and number will both be incremented
        private bool jumpUp()
        {

            if (active)
            {
                if (numOfQueries == maxQueries || number < min_number)
                {
                    active = false; 
                } else
                {
                    numOfQueries++;
                    number++;
                }
            }

            return active;
        }
        //Pre-Condition: Must be Active 
        //Post-Condition: Number must be over 1000 && numOfQueries < maxQueries otherwise it will be inactive, query is incremented, and number is decremented
        private bool jumpDown()
        {
            if(active)
            {
                if (numOfQueries == maxQueries || number == min_number)
                    active = false;  
                else
                {
                    numOfQueries++;
                    number--;
                }
            }

            return active;
            
        }

        private bool isPrime(int num)
        {
            for (int i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }


        private int nextUpperPrime(int num)
        {
            if (isPrime(num))
                num++;

            while (!isPrime(num))
                num++;
            return num;
        }

        private int nextLowerPrime(int num)
        {
            if(isPrime(num))
                num--;

            while (!isPrime(num))
                num--;
            return num;
        }
    }
}



/*
 *  Implementation Invariant 
 *     
 *  "min_number" will represent the minimum number the jumpPrime object number can be which is 1000, otherwise it will turn to an inactive state
 *  "default_number" will represent the initial number the jumpPrime object will be at if given an invalid number to start out with (5000)
 *  
 *  I've also utilized 5 different private utility methods
 *      int isPrime(int) is used to check the primality of a certain number, returns true if prime otherwise it is false
 *      int nextUpperPrime(int) is used to look for the next upper prime number which utilizes the isPrime() function
 *      int nextLowerPrime(int) is used to look for the next lower prime number which also utilizes the isPrime() function
 *      bool jumpUp() is another private function which is utilized in the public up() function. It is used to increment the query count as well
 *          as the current number if pre-conditions are approved. This function controls the state of active and will return the object's state
 *          after the call for the query. If pre-conditions are passed, then it will return true and allow the up() 
 *          function to correctly return the upper prime number.
 *      bool jumpDown() is the same as jumpUp() but for the down() function.
 *  
 *  
 *  For up() & down():
 *      We first check if the pre-conditions (numOfQueries is still less than maxQueries & number is 4 digits long) are passed in the private utility methods (jumpUp() or jumpDown())
 *          If they are, then we'll increment or decrement our hidden number, increment the query count, and return the proper number. 
 *          Otherwise, active will be set to false and then return 0.
 *  For reset():
 *      if active, update upperPrime and lowerPrime. From those numbers, update maxQueries and reset numOfQueries to 0. 
 *      else do nothing
 *  For revive():
 *      If deactivated, just return and do nothing
 *      Otherwise switch active's state and if active is true, call reset(); otherwise set deactived to be true
 *      
 *  Constructor's Initial State: 
 *      number will be set to the client's number unless it is not of 4 digits length at the minimum. Then it will just be set to the 
 *          default_number which is currently at 5000
 *      active will be set to true, deactivated will be set to false
 *      upperPrime & lowerPrime will be initialized based off the current value of "number"
 *      numOfQueries will be set to 0, while maxQueries will be set to the difference between upperPrime & lowerPrime.
 *          
 *  "number" will either increment or decrement based off of up() or down() and numOfQueries will always increment when called from up() or down()
 *  
 *  
 *  The "active" state depends on the "number" variable and the "numOfQueries" variable. "Active" will transition to false if "number"
 *  goes below or equal to the "min_number" or if "numOfQueries" goes above "maxQueries". 
 *  
 *  "deactivated" is dependent on when the function revive() is called at an incorrect time. If revive() is called when the object is active,
 *  then "deactivated" becomes true and the object can never become active again.
 *  
 *  when an object is inactive & !deactivated, it won't be able to call any functions properly except for revive() and getActive(). 
 *      up() & down() will only return 0 
 *      reset() will do nothing
 *  
 *  If "deactivated" is set to true:
 *      up() & down() will only return 0
 *      reset() will do nothing
 *      revive() will do nothing
 *      getActive() will always return false
 *  
 *  As seen above, "maxQueries" is based off of "upperPrime" and "lowerPrime". But as those values changes, "maxQueries" will remain the same.
 *  The only time "maxQueries" will change is when reset() or revive() is called in which "upperPrime" and "lowerPrime" is also updated. 
 *  
 *  When calling revive() and is done properly, number should stay the same it was as before but upperPrime and lowerPrime may end up getting updated.
 *      "active" will be switched to true and numqueries set to zero. 
 * 
 */


