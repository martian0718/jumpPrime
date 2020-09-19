/*
 *  Author: Martin Wu
 *  
 *  Created: September 10th, 2020
 *
 *  Modified: September 17th, 2020
 *
 *  FileName: P1.cs (Driver)
 *  
 *  Language: C# in Visual Studio
 */

/*              An Overview of this Program     
 *  
 *  I've used functional decomposition to decompose each function to have one specific job. 
 *      Initialize numbers will initialize the random numbers
 *      makeObjects will create the jumpPrime objects and run the program
 *      simulateObject will have an RNG for
 * 
 *  We create an array of 5 random numbers. The first 3 will be any number from 0 to 10,000. 
 *  Then we will have 2 random numbers that could be literally anything. Then we make 5 different jumpPrime objects and construct them
 *  with the five randomly generated integers. With those objects we will use a for loop to go through a simulation with each of them.
 *  
 *  Where we will query up() a random number of times (RNG from 10-30). After those queries we'll attempt to revive the jumpPrime object. If it is active when attempted to 
 *  revive, it will just become deactivated and return 0 whenever it is being queried again for the rest of the program. 
 *  But if it was actually inactive and then revived, it should go back to functioning properly.
 *  
 *  Generate another random number from 1-20 to indicate how many times to query down() now. If we go inactive from overquerying or from going below the minimum number (1000), then 
 *  we will revive() the jumpPrime object again. If we were ALREADY inactive because of the prior improper call to revive() then it will attemp to revive again but do nothing.
 *  Otherwise, after finishing querying down, we'll reset the object and generate another random number (1-10) to query down() once more.
 *  At the very end, return whether the jumpPrime object is active.
 * 
 *  All input will be provided by an RNG from a certain range or just any number... 
 *  Outputs will be all the up() and down() queries and also whether the object got revived() or reset()
 * 
 *  Assumptions:
 *  Created an RNG that can return any number up till 100,000 for 3 different jumpPrime objects and any random number for 2 different jumpPrime objects. 
 *  RNG for number of queries going up and down as well which will be between 1 and 20. For going up, it will be between (10,30) and going back down the first time
 *  will be between (1,20). Then the last number of queries will be between (1,10). We should have a couple of instances where active is true and active is false
 * 
 */

using System;

namespace P1
{
    class P1
    {
        static void Main(string[] args)
        {

            initializeNumbers();
        }

        public static void initializeNumbers()
        {
            Random random = new Random();

            int number_objects = 5;
            int num_less_than_10000 = 3;
            int[] num = new int[number_objects];
            int upper_limit = 100000;

            for(int i = 0; i < num_less_than_10000; i++)
            {
                if (i < num_less_than_10000)
                    num[i] = random.Next(upper_limit);
                else
                    num[i] = random.Next();
            }

            makeObjects(num, number_objects);


        }

        private static void makeObjects(int[] num, int number_objects)
        {
            jumpPrime[] db = new jumpPrime[number_objects];

            for (int i = 0; i < number_objects; i++)
            {
                Console.WriteLine("========Test Object " + (i + 1) + ": =========");
                db[i] = new jumpPrime(num[i]);
                simulateObjects(ref db[i]);
                Console.WriteLine(); //for layout
                Console.WriteLine();
            }
        }

        private static void simulateObjects(ref jumpPrime obj)
        {
            Random random = new Random();

            const int upper_limit_up = 30;
            const int lower_limit_up = 10;
            const int upper_limit_down = 20;
            
            int queries = random.Next(lower_limit_up, upper_limit_up);
            query_up_and_revive(ref obj, queries);

            Console.WriteLine(); //for layout
            
            queries = random.Next(1,upper_limit_down);
            iterate_down_with_revive(ref obj, queries);

            Console.WriteLine(); //for layout
            Console.WriteLine("Object is reset...");
            obj.reset();
            Console.WriteLine(); //for layout

            queries = random.Next(1, lower_limit_up);
            iterate_down(ref obj, queries);

            Console.WriteLine(); //for layout
            Console.WriteLine("Active is " + obj.getActive());

        }

        private static void query_up_and_revive(ref jumpPrime obj, int queries)
        {
            iterate_up(ref obj, queries);
            Console.WriteLine("Revive Object...");
            obj.revive();
        }

        //may output many zeroes if number of queries exceeds limit
        private static void iterate_up(ref jumpPrime obj, int num)
        {
            Console.WriteLine("We are querying the up call: ");
            for (int i = 0; i < num; i++)
            {
                
                Console.Write(i + ". " + obj.up() + "  ");
               
            }
            
        }

        //may out many zeros if object becomes inactive
        private static void iterate_down(ref jumpPrime obj, int num)
        {
            Console.WriteLine("We are querying the down call: ");
            for (int i = 0; i < num; i++)
            {
                
                Console.Write(i + ". " + obj.down() + "  ");
                
            }
            Console.WriteLine();
        }

        //once it becomes inactive, it should revive
        private static void iterate_down_with_revive(ref jumpPrime obj, int num)
        {
            Console.WriteLine("We are querying the down call: ");
            for (int i = 0; i < num; i++)
            {
                if (obj.getActive())
                    Console.Write(i + ". " + obj.down() + "  ");
                else
                {
                    Console.Write("We exceeded the querying limit... Must Revive  ");
                    obj.revive();
                    break;
                }
            }
            Console.WriteLine();
        }




    }
}
