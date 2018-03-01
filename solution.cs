//Hashcode 2018

using System;
using System.IO;

class program{
	public int currentTime = 0; //current tick, initialised to 0
	public int availableCars; //number of cars not on rides
	public ride[] rides; //array of rides
	public car[] cars; //array of cars
	public int bonus; //number of bonus points for being early
	public int timeRemaining; //number of ticks left
	
	static void Main(){
		int currentTime = 0; //current tick, initialised to 0
		int availableCars = 0; //number of cars not on rides
		ride[] rides = new ride[1]; //array of rides
		car[] cars = new car[1]; //array of cars
		int bonus = 0; //number of bonus points for being early
		int timeRemaining = 0; //number of ticks left
		readIn(ref availableCars, ref rides, ref cars, ref bonus, ref timeRemaining);
	}
	
	static void readIn(ref int availableCars, ref ride[] rides, ref car[] cars, ref int bonus, ref int timeRemaining){ //read data in from the .in file
		string[] file = System.IO.File.ReadAllLines("a_example.in");
		string[] currentLine = file[0].Split(' ');
		availableCars = Int32.Parse(currentLine[2]);
		cars = new car[Int32.Parse(currentLine[2])];
		rides = new ride[Int32.Parse(currentLine[3])];
		bonus = Int32.Parse(currentLine[4]);
		timeRemaining = Int32.Parse(currentLine[5]);
		for(int i = 1; i < file.Length; i++){
			currentLine = file[i].Split(' ');
			rides[i-1] = new ride();
			rides[i-1].start = new int[2]{Int32.Parse(currentLine[0]), Int32.Parse(currentLine[1])};
			rides[i-1].end = new int[2]{Int32.Parse(currentLine[2]), Int32.Parse(currentLine[3])};
			rides[i-1].earliestStart = Int32.Parse(currentLine[4]);
			rides[i-1].latestFinish = Int32.Parse(currentLine[5]);
			//Console.WriteLine("Ride added. Start: {0}, End: {1}, Earliest Start: {2}, Latest Finish: {3}", rides[i-1].start, rides[i-1].end, rides[i-1].earliestStart, rides[i-1].latestFinish);
		}
	}
	
	static int Distance(int[]carPos,int[]ridePos){
		int dist = 0;
		
		int xdist = carPos[0] - ridePos[0];
		if(xdist<0){
			xdist *= -1;
		}
		int ydist = carPos[1] - ridePos[1];
		if(ydist<0){
			ydist *= -1;
		}
		dist = xdist + ydist;
		return dist;
	}
	
	public void advance(){
        //advance 1 tick
        while (timeRemaining > 0)
        {
            for (int i = 0; i < cars.Length; i++)
            {
                car Car = cars[i];
                if (Car.busy)
                {
                    if (!Car.currentPos[0].Equals(Car.destination[0]))
                    {
                        if (Car.currentPos[0] < Car.destination[0])
                        {
                            Car.currentPos[0]++;
                        }
                        else
                        {
                            Car.currentPos[0]--;
                        }
                    }
                    else if (!Car.currentPos[1].Equals(Car.destination[1]))
                    {
                        if (Car.currentPos[1] < Car.destination[1])
                        {
                            Car.currentPos[1]++;
                        }
                        else
                        {
                            Car.currentPos[1]--;
                        }
                    }
                }
                if (Car.currentPos[0].Equals(Car.destination[0]) && Car.currentPos[1].Equals(Car.destination[1]))
                {
                    Car.busy = false;
                }
            }
            currentTime++;
            timeRemaining--;
        }
	}
}

class ride{
	public int[] start; //start pos
	public int[] end; //end pos
	
	public int earliestStart; //earliest start time
	public int latestFinish; //latest finish time
	
	public bool complete = false;
}

class car{
    public int[] currentPos = new int[2] {0,0};
    public int[] destination = new int[2];
    public bool busy = false;
}
