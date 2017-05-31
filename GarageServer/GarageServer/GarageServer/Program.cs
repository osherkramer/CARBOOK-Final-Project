using System;

namespace GarageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            bool running = true;
            int command;
            server.beginWork();
            

            do
            {
                Console.WriteLine("************** Garage Server Console **************");
                Console.WriteLine("**                   Menu:                       **");
                Console.WriteLine("** 1. Create new history items for car.          **");
                Console.WriteLine("** 2. Create new history items for several cars. **");
                Console.WriteLine("** 3. Exit                                       **");     
                Console.WriteLine("***************************************************");

                while(!Int32.TryParse(Console.ReadLine(), out command) || command < 1 || command > 3)
                {
                    Console.WriteLine("Error command number, please try again");  
                }

                switch(command)
                {
                    case 1:
                        createHistoryItemForCar(server);
                        break;
                    case 2:
                        Console.WriteLine("****************************");
                        Console.WriteLine("** 1. For specifics cars  **");
                        Console.WriteLine("** 2. For random cars     **");
                        Console.WriteLine("****************************");

                        int command2 = 0;
                        while (!Int32.TryParse(Console.ReadLine(), out command2) || command2 < 1 || command2 > 2)
                        {
                            Console.WriteLine("Error command number, please try again");
                        }

                        switch (command2)
                        {
                            case 1:
                                int numberCars = 0;
                                Console.WriteLine("Please enter number of cars that you want to enter: ");
                                while (!Int32.TryParse(Console.ReadLine(), out numberCars) || numberCars < 1)
                                {
                                    Console.WriteLine("Invalid number, please try again");
                                }

                                for(int i = 0; i < numberCars; i++)
                                {
                                    createHistoryItemForCar(server);
                                }
                                break;

                            case 2:
                                createHistoryItemForRandomCars(server);
                                break;
                        }
                        break;
                    case 3:
                        running = false;
                        break;
                }

            } while (running);
            server.CloseServer();
        }

        static void createHistoryItemForRandomCars(Server server)
        {
            int numberCars;
            int hiNumber;

            Console.Write("Please enter number of cars to create: ");
            while (!Int32.TryParse(Console.ReadLine(), out numberCars) || numberCars < 1)
            {
                Console.WriteLine("Number of history items can't be less then 1. Please try again: ");
            }

            Console.Write("Please enter number of maximum history items to create for each car: ");
            while (!Int32.TryParse(Console.ReadLine(), out hiNumber) || hiNumber < 1)
            {
                Console.WriteLine("Number of history items can't be less then 1. Please try again: ");
            }

            server.CreateHistoryItemRandom(numberCars, hiNumber);
        }

        static void createHistoryItemForCar(Server server)
        {
            String carNumber;
            int hiNumber;
            Console.Write("Please enter the car number: ");
            carNumber = Console.ReadLine();
            while (carNumber.Length != 7 || !Int32.TryParse(carNumber, out hiNumber))
            {
                Console.Write("Wrong number, please try again: ");
                carNumber = Console.ReadLine();
            }

            Console.Write("Please enter number of history items to create for car " + carNumber + ": ");
            while (!Int32.TryParse(Console.ReadLine(), out hiNumber) || hiNumber < 1)
            {
                Console.WriteLine("Number of history items can't be less then 1. Please try again: ");
            }

            bool success = server.CreateHistoryItem(carNumber, hiNumber);

            if (success)
            {
                Console.WriteLine("Success to create " + hiNumber + " history items for car " + carNumber);
            }
            else
            {
                Console.WriteLine("Unsuccess to create " + hiNumber + " history items for car " + carNumber);
            }
        }
    }
}
