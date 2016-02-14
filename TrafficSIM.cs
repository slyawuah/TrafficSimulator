//Discrete-Event Simulation
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace As2PartB {

    class MainClass {

        public static void Main() {

            int s, e, min, max; // s is interarrival time for the north to south lane, e for east to west lane, and min and max are the times the lights stay on for.
            //Ask for mean interarrival time for West to East Lane and make sure to use catch/try statement to not get error.
            Console.WriteLine("Please enter the mean interarrival time (s) for cars on the North to South lane.");
            while (true) {
                try {
                    s = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (System.FormatException Exception) {
                    System.Console.WriteLine("Please enter a positive number thats greater than zero. ");
                }
            }
            //Ask for mean interarrival time for East to West Lane and make sure to use catch/try statement to not get error.
            Console.WriteLine("Please enter the mean interarrival time (s) for cars on the East to West lane.");
            while (true) {
                try {
                    e = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (System.FormatException Exception) {
                    System.Console.WriteLine("Please enter a positive number thats greater than zero. ");
                }
            }
            //Ask for the minimum time to green, catch/try statement to not get error.
            Console.WriteLine("Please enter the minimum time (s) the light stays lit for green:");
            while (true) {
                try {
                    min = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (System.FormatException Exception) {
                    System.Console.WriteLine("Please enter a positive number thats greater than zero. ");
                }
            }
            //Ask for the maximum time to green, catch/try statement to not get error.
            Console.WriteLine("Please enter the maximum time (s) the light stays lit for green:");
            while (true) {
                try {
                    max = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (System.FormatException Exception) {
                    System.Console.WriteLine("Please enter a positive number thats greater than zero. ");
                }
            }

            //Create an instance of simulation and pass on parameters for the simulation for the simulation.
            Simulation test1 = new Simulation(s, e, min, max);
            //Ask the user to see simulation.
            Console.WriteLine("Would you like to see the simulation output (enable verbose)? Y/N");
            while (true) {
                try //Try to catch errors.
                {
                    char a = Convert.ToChar(Console.ReadLine()); //Get input to start game.
                    if (a == 'y' || a == 'Y') // If they say yes, start the game.
                    {
                        test1.CarProcedural(); //Run the Procedural function for the Car simulation.
                        break;
                    }
                    if (a == 'n' || a == 'N') // If they input no, then do not start game and proceed to exiting the program.
                    {
                        break; //Break out of the loop if No.
                    }

                }
                catch (System.FormatException Exception) {
                    System.Console.WriteLine("Please enter y/Y or n/N. ");
                }
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("The optimized light times for the average interarrival from north of {0}s and average interarrival from the east of {1}s are:", min, max);
            test1.OptimalTimes(); //Return the optimal times.
            Console.ReadLine(); //ReadLine to close program.
        }
    }

    //Simulation class
    class Simulation {
        private int northlightTime; //private variable for northlightTime.
        private int eastlightTime; //private variable for eastlightTime.
        private double rateofcar, rateofcar2; //rate of car and rate of car2, mean interarrival time for N to S, and N to W respectively.
        private double[] intertimeeast = new double[1000]; //private variable for intertimeeast.
        private double[] intertimenorth = new double[1000]; //private variable for intertimenorth.

        //Parameters for Simulation Class( pass rate car, pass rate car2 for other cars, and min and max for the lights.
        public Simulation(double ratecar, double ratecar2, int min, int max) {
            Rateofcar = ratecar;
            NorthlightTime = min;
            EastlightTime = max;
            intertimeeast = Generatetime(Rateofcar);
            intertimenorth = Generatetime(Rateofcar2);

        }
        //Procedural Method that runs the main simulation program.
        public void CarProcedural() {
            int start = 4; //Start the switch statement.
            int east = 1, north = 1; //Initialize the program.

            //Check if light changed and add stop light delay time.
            switch (start) {
                // A switch section can have more than one case label. 
                case 0:
                case 2:
                    Console.WriteLine("All Cars have left.");
                    break;
                // The following line causes a warning.
                //    Console.WriteLine("Unreachable code");
                // 7 - 4 in the following line evaluates to 3. 
                case 4:
                    if (east == 1) {
                        this.GenerateCarqueue(intertimeeast, RandomDirection()); //Put Cars into simulation and run it.
                        //this.GenerateCarqueue().
                    }
                    if (north == 1) {
                    }
                    break;
                case 7 - 1:
                    Console.WriteLine("Case 3");
                    break;
                // If the value of switchExpression is not 0, 1, 2, or 3, the 
                // default case is executed. 
                default:
                    Console.WriteLine("Default case (optional)");
                    break;
            }
        }
        //Return which lights are on or off.
        public void LightsOn() {
            Light l = new Light(); //Initialize class for light switching and detection.
            if (l.ChangeLights() == true) {
                Console.WriteLine("The lights have changed. GREEN on East.");
                //   Console.WriteLine("Time waited:" + EastlightTime);

            }
            else if (l.ChangeLights() == false) {
                Console.WriteLine("The lights have changed. GREEN on North.");
                //  Console.WriteLine("Time waited:" + NorthlightTime);


            }
        }
        //Return the rate of car north to south for the mean interarrival time.
        public double Rateofcar {
            get { return rateofcar; }
            set { rateofcar = value; }
        }
        //Return the rate of for east to west for the mean itnerarrival time.
        public double Rateofcar2 {
            get { return rateofcar2; }

            set { rateofcar2 = value; }
        }

        //Return the NorthLightTime and the value.
        public int NorthlightTime {
            get { return northlightTime; }
            set { northlightTime = value; }
        }
        //Return the EastLightTime and the value.
        public int EastlightTime {
            get { return eastlightTime; }
            set { eastlightTime = value; }
        }
        //Function for tracking the time of the simulation in hours::minutes::seconds.
        private string timer_Tick(double time) //Pass the time of the simulation.
        {
            TimeSpan t = TimeSpan.FromSeconds(time); //Create a TimeSpan class.
            return (string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds)); //Output the resulting time using the TimeSpan class.
        }
        //Functions to get random numbers, since in a closed loop most random values are rehased.
        private static readonly Random getrandom = new Random(); //create new instance of random.
        private static readonly object syncLock = new object(); //lock instance of object
        public static int GetRandomNumber(int min, int max) {
            lock (syncLock) { // synchronize
                return getrandom.Next(min, max);
            }
        }
        //Randomize car direction for arrival times.
        public string RandomDirection() {
            string direction;
            var rng = new Random();
            var temp = GetRandomNumber(0, 100);
            if (temp >= 50) {
                direction = "East";
            }
            else if (temp < 50) {
                direction = "North";
            }
            else {
                direction = "Invalid";
            }
            return direction;
        }
        //Return the optimal time and the average wait time for all cars.
        public void OptimalTimes() {
            Console.WriteLine("Time green for North {0}s.", NorthlightTime * (AveragearrivalTime()));
            Console.WriteLine("Time green for East {0}s.", EastlightTime * (AveragearrivalTime()));
            Console.WriteLine("Average wait time {0}s.", AveragearrivalTime());
        }

        //Calculate the deperature time for the cars.
        public double CarDepartureTimes() {
            double Timeaccumalator = 0;
            // Calculate the northlighttime departure time.
            for (int i = 0; i < NorthlightTime; i++) {
                Timeaccumalator += intertimeeast[i];
            }
            // Calculate the eastlighttime departure time.
            for (int i = 0; i < EastlightTime; i++) {
                Timeaccumalator += intertimenorth[i];
            }
            return Timeaccumalator / intertimeeast.Length;
        }
        //Generate Car Queques
        private Event[] GenerateCarqueue(double[] interarrivaltime, string direction) {
            PriorityQueue<Event> PQ = new PriorityQueue<Event>(1000); //Create a PriorityQueue for 1000 events.
            Event[] cararray = new Event[1000]; //Return an array for cararray.
            Light l = new Light(); //Create a new instance of Lights
            //StreamWriter writetext = new StreamWriter("writex.txt"); // Create an instance of SteamWriter to write to fiels for testing and graping purposes.
            double initialtime = 0;
            for (int i = 0; i < interarrivaltime.Length; i++) {
                LightsOn(); //Turn the lights on.
                RandomDirection(); //Return random direction for cars.
                if (RandomDirection() == "East") {
                    PQ.Add(new Event("Car " + Convert.ToString(GetRandomNumber(0, i)) + " at " + "East", initialtime + interarrivaltime[i]));
                    Console.WriteLine(timer_Tick(initialtime) + ":  " + PQ.Front().ToString(direction));
                    if (!PQ.Empty()) {
                        Console.WriteLine(timer_Tick(initialtime) + ":  " + "Car " + Convert.ToString(GetRandomNumber(GetRandomNumber(0, i), i)) + " has left East.");
                        PQ.Remove();
                        //Add car departuretime to total time.
                        timer_Tick(initialtime + CarDepartureTimes());
                    }
                }
                //For North direction.
                else if (RandomDirection() == "North") {
                    PQ.Add(new Event("Car " + Convert.ToString(GetRandomNumber(0, i)) + " at " + "North", initialtime + interarrivaltime[i]));
                    Console.WriteLine(timer_Tick(initialtime) + ":  " + PQ.Front().ToString(direction));
                    if (!PQ.Empty()) {
                        Console.WriteLine(timer_Tick(initialtime) + ":  " + "Car " + Convert.ToString(GetRandomNumber(GetRandomNumber(0, i), i)) + " has left North.");
                        PQ.Remove();
                        //Add car departuretime to total time.
                        timer_Tick(initialtime + CarDepartureTimes());
                    }
                }
                //  PQ.Add(new Event("Car " + Convert.ToString(i) + " at " + RandomDirection(), initialtime + interarrivaltime[i]));
                initialtime += interarrivaltime[i];
                if (!PQ.Empty()) {
                    // writetext.WriteLine(PQ.Front().ToString(direction));
                    if (l.ChangeLights() == true) {
                        initialtime += EastlightTime;
                        Console.WriteLine(timer_Tick(initialtime));
                    }
                    else if (l.ChangeLights() == false) {
                        initialtime += NorthlightTime;
                        Console.WriteLine(timer_Tick(initialtime));
                    }
                    // writetext.WriteLine(timer_Tick(initialtime));//record the time and save to a file for analysis and graphing.
                    // Console.WriteLine(timer_Tick(initialtime) + ":  " + PQ.Front().ToString(direction));
                    PQ.Remove();
                }
            }
            // writetext.Close();
            return cararray;
        }
        //Input parameters for the simulation, only one time is given and used for both.
        //This function generates the time and returns the InterArrivalTimes
        public double[] Generatetime(double AverageArrivalTime) {
            double[] InterArrivalTimes = new double[100];
            var rng = new Random();
            for (int i = 0; i < InterArrivalTimes.Length; i++) {
                InterArrivalTimes[i] = -Math.Log((rng.Next(1, 100)) / (double)100) * AverageArrivalTime;
            }
            return InterArrivalTimes;
        }
        //Return the AverageArrivalTime
        public double AveragearrivalTime() {
            double Timeaccumalator = 0;
            for (int i = 0; i < intertimeeast.Length; i++) {
                Timeaccumalator += intertimeeast[i];
            }
            return Timeaccumalator / intertimeeast.Length;
        }
    }
    //class for light
    class Light {
        private bool green;

        public delegate void LightChange();

        public event LightChange changeevent;

        public bool Green {
            get { return green; }
            set { green = value; }
        }

        //Initialize the boolean value for Green.
        public Light() {
            Green = true;
        }
        //Randomize function.
        private static readonly Random getrandom = new Random(); //create new instance of random.
        private static readonly object syncLock = new object(); //lock instance of object
        public static int GetRandomNumber(int min, int max) {
            lock (syncLock) { // synchronize
                return getrandom.Next(min, max);
            }
        }
        //Return the ChangeStatus()
        public void ChangeStatus() {
            if (Green == true) {
                Green = false;
                if (changeevent != null) {
                    changeevent();
                }

            }
            if (Green == false) {
                Green = true;
                if (changeevent != null) {
                    changeevent();

                }
            }
        }
        //Change the lights of the streets randomly, based on a random number generator.
        public bool ChangeLights() {

            double temp = GetRandomNumber(0, 100);
            if (temp >= 50) {
                Green = true;
            }
            else if (temp < 50) {
                Green = false;
            }

            return Green;

        }
    }

    public interface IContainer<Event> {
        void MakeEmpty();
        bool Empty();
        int Size();
    }

    public interface IPriorityQueue<Event> : IContainer<Event> where Event : IComparable {
        void Add(Event item);
        void Remove();
        Event Front();
    }
    // --------------------------------------------------------------------------------------------------------------
    public class PriorityQueue<Event> : IPriorityQueue<Event> where Event : IComparable {
        private int capacity;  // Maximum number of items in a priority queue
        private Event[] element;   // Array of items
        private int numItems;  // Number of items in a priority queue

        public PriorityQueue(int size) {
            capacity = size;
            element = new Event[size + 1];  // Indexing begins at 1
            numItems = 0;

        }
        // Percolate up from position i in a priority queue.
        private void PercolateUp(int i) {
            int child = i, parent;

            while (child > 1) {
                parent = child / 2;
                if (element[child].CompareTo(element[parent]) > 0)
                // If child has a higher priority than parent
                {
                    // Swap parent and child
                    Event item = element[child];
                    element[child] = element[parent];
                    element[parent] = item;
                    child = parent;  // Move up child index to parent index
                }
                else
                    // Item is in its proper position
                    return;
            }
        }
        public void Add(Event item) {
            if (numItems < capacity) {
                element[++numItems] = item;  // Place item at the next available position
                //PercolateUp(numItems);
            }
        }

        //Percolate down from position i in a priority queue.
        private void PercolateDown(int i) {
            int parent = i, child;
            while (2 * parent <= numItems)
            // while parent has at least one child
            {
                // Select the child with the highest priority
                child = 2 * parent;    // Left child index
                if (child < numItems)  // Right child also exists
                    if (element[child + 1].CompareTo(element[child]) > 0) {
                        // Right child has a higher priority than left child
                        child++;
                    }
                if (element[child].CompareTo(element[parent]) > 0)
                // If child has a higher priority than parent
                {
                    // Swap parent and child
                    Event item = element[child];
                    element[child] = element[parent];
                    element[parent] = item;
                    parent = child;  // Move down parent index to child index
                }
                else
                    // Item is in its proper place
                    return;
            }
        }
        public void Remove() {
            if (!Empty()) {
                // Remove item with highest priority (root) and
                // replace it with the last item
                element[1] = element[numItems--];
                // Percolate down the new root item
                //PercolateDown(1);
            }
        }

        public Event Front() {
            if (!Empty()) {
                return element[1];  // Return the root item (highest priority)
            }
            else
                return default(Event);
        }

        // Create a binary heap
        // Percolate down from the last parent to the root (first parent)

        private void BuildHeap() {
            int i;
            for (i = numItems / 2; i >= 1; i--) {
                //PercolateDown(i);
            }
        }

        // Sorts and returns the InputArray

        public void HeapSort(Event[] inputArray) {
            int i;

            capacity = numItems = inputArray.Length;

            // Copy input array to element (indexed from 1)
            for (i = capacity - 1; i >= 0; i--) {
                element[i + 1] = inputArray[i];
            }

            // Create a binary heap
            BuildHeap();

            // Remove the next item and place it into the input (output) array
            for (i = 0; i < capacity; i++) {
                inputArray[i] = Front();
                Remove();
            }
        }

        public void MakeEmpty() {
            numItems = 0;
        }
        //Return Empty() 
        public bool Empty() {
            return numItems == 0;
        }

        //Return Size()
        public int Size() {
            return numItems;
        }
    }
    // --------------------------------------------------------------------------------------------------------------
    public class Event : IComparable {
        private double time;
        private string eventtype;
        public double Time {
            get { return time; }
            set { time = value; }
            // class that implements IMyInterface.
        }
        public string EventType {
            get { return eventtype; }
            set { eventtype = value; }
        }
        public Event(string TypeofEvent, double EventTime) {
            Time = EventTime;
            EventType = TypeofEvent;
        }
        public int CompareTo(Object obj) {
            Event other = (Event)obj;   // Explicit cast
            return (int)(time - other.time);
        }
        //Return the ToString method with total duration of method.
        public string ToString(string direction) {
            return string.Format("{0}  Total Duration:{1}]", EventType, Time);
        }

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         