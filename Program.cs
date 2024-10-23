// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Xml;
using Microsoft.VisualBasic;

Random random = new Random();
DateTime now = DateTime.Now;

List<Plant> plants = new List<Plant>()
{
    new Plant
    {
        Species = "Ficus",
        LightNeeds = 3, // 1=Low, 2=Medium, 3=High
        AskingPrice = 25.50m,
        City = "Los Angeles",
        ZIP = "90001",
        Sold = false,
        AvailableUntil = new DateTime(2024, 09, 10)  
    },
    new Plant
    {
        Species = "Snake Plant",
        LightNeeds = 1,
        AskingPrice = 15.00m,
        City = "New York",
        ZIP = "10001",
        Sold = true,
        AvailableUntil = new DateTime(2024, 11, 15)  
    },
    new Plant
    {
        Species = "Spider Plant",
        LightNeeds = 2,
        AskingPrice = 18.75m,
        City = "Austin",
        ZIP = "73301",
        Sold = false,
        AvailableUntil = new DateTime(2025, 1, 5)  
    },
    new Plant
    {
        Species = "Aloe Vera",
        LightNeeds = 3,
        AskingPrice = 12.30m,
        City = "Phoenix",
        ZIP = "85001",
        Sold = true,
        AvailableUntil = new DateTime(2024, 10, 31)  
    },
    new Plant
    {
        Species = "Peace Lily",
        LightNeeds = 4,
        AskingPrice = 20.00m,
        City = "Chicago",
        ZIP = "60601",
        Sold = false,
        AvailableUntil = new DateTime(2025, 3, 1) 
    }
};

var nonSoldPlants = plants.Where(plant =>
{
    bool isAvailable = !plant.Sold;
    TimeSpan expTimeSpan = plant.AvailableUntil - now;
    bool notExp = expTimeSpan > TimeSpan.Zero;
    return isAvailable && notExp;
} ).ToList();

void PlantList()
{
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {PlantDetails(plants[i])} in {plants[i].City} {(plants[i].Sold? "was sold" : "is avaiable")} for {plants[i].AskingPrice} dollars");
    };
};
void postPlant()
{
    Console.WriteLine("what is the species?");
    string newSpecies = Console.ReadLine();

    int newLightNeeds = 0;
    while (newLightNeeds < 1 || newLightNeeds > 5)
    {
        Console.WriteLine("what is the light needs 1-5?");
        try
        {
            newLightNeeds = int.Parse(Console.ReadLine().Trim());

        }
        catch(Exception ex)
        {
            Console.WriteLine("please pick a number between 1-5");
        }
    }
    
    decimal newAskingPrice = 0;
    bool validPrice = false;
    while (!validPrice)
    {
        Console.WriteLine("what is the asking Price?");
        string input = Console.ReadLine();
        try
        {

         newAskingPrice = decimal.Parse(input.Trim());
         if (newAskingPrice > 0)
         {
            validPrice = true;
         }
         else
         {
            Console.WriteLine("price must be a positive number");
         }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid price");
        }
        
    }

    Console.WriteLine("what is City?");
    string newCity = Console.ReadLine();
    string newZip = String.Empty;
    bool validZIP = false;
    while(!validZIP)
    {   
        Console.WriteLine("what is the ZIP?");
         newZip = Console.ReadLine().Trim();
        try
        {
            string pattern = @"^\d{5}$";
            string copy = newZip;
            validZIP = Regex.IsMatch(copy, pattern );
            if (!validZIP)
            {
                Console.WriteLine("please write a valid ZIP");
            }
        }
        catch (Exception ex)
        {
            
            Console.WriteLine("please enter valid ZIP");
        };

    }

    Console.WriteLine("when is the plant available till?");
    DateTime experationDate = new DateTime();
    bool correctDate = false;
    while (!correctDate)
    {
        int year;
        int months;
        int day;
       try
       {
            Console.WriteLine("enter year");
             year = int.Parse(Console.ReadLine().Trim());
             Console.WriteLine("enter month (1-12)");
             months = int.Parse(Console.ReadLine().Trim());
             Console.WriteLine("enter day:");
             day = int.Parse(Console.ReadLine().Trim());
            
            experationDate =  new DateTime(year, months, day);
            TimeSpan experationLength = experationDate - now;
            if (experationLength < TimeSpan.Zero)
            {
                Console.WriteLine("The expiration date is in the past.");
            }
            else
            {
                correctDate = true;
            }
       }
       catch (ArgumentOutOfRangeException)
       {
            Console.WriteLine("the date is out of range!");
            
        
       }
       catch(FormatException)
       {
            Console.WriteLine("the date needs to be in number format only!");
       } 

    }

    Plant newPlant = 
    new Plant()
    {
        Species = newSpecies,
        LightNeeds = newLightNeeds,
        AskingPrice = newAskingPrice,
        City = newCity,
        ZIP = newZip,
        Sold = false,
        AvailableUntil = experationDate


    };

    plants.Add(newPlant);
    Console.WriteLine($"{PlantDetails(newPlant)} has been successfully added");

}
void AdoptPlant()
{
    int Choice = 0;
    while (Choice > nonSoldPlants.Count || Choice < 1)
    {
        try
        {   Console.Clear();
            Console.WriteLine(@"Adopt one of these Plants");
            for (int i = 0; i < nonSoldPlants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {PlantDetails(nonSoldPlants[i])}");
            }

            Console.WriteLine("select plant:");
            Choice = int.Parse(Console.ReadLine().Trim());
            if (Choice <= nonSoldPlants.Count && Choice >= 1)
            {
            nonSoldPlants[Choice - 1].Sold = true;
            }
            else
            {
            Console.WriteLine("pick an option by its number");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("invalid input");
        }
    }
}
void delistPlant()
{
    PlantList();
    Console.WriteLine("select a plant to delete: ");
    int Choice = 0;
    while (Choice > plants.Count || Choice < 1)
    {
        try
        {   Console.Clear();
            Console.WriteLine(@"Adopt one of these Plants");
            for (int i = 0; i < plants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {PlantDetails(plants[i])}");
            }

            Console.WriteLine("select plant:");
            Choice = int.Parse(Console.ReadLine().Trim());
            if (Choice <= plants.Count && Choice >= 1)
            {
                plants.RemoveAt(Choice - 1);
                Console.Clear();
                PlantList();
                Console.ReadKey();
                Console.Clear();
                break;
            }
            else
            {
            Console.WriteLine("pick an option by its number");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("invalid input");
        }
    }
}
void randomPlant()
{
    int plantOfTheDay = 0;
    bool PlantSold = true;
    while (PlantSold)
    {
        plantOfTheDay = random.Next(1, plants.Count);
        PlantSold = !plants[plantOfTheDay].Sold ? false : PlantSold;
        
    }
    Console.WriteLine($"{PlantDetails(plants[plantOfTheDay])} in {plants[plantOfTheDay].City} {(plants[plantOfTheDay].Sold? "was sold" : "is avaiable")} for {plants[plantOfTheDay].AskingPrice} dollars");
}
void searchByLight()
{
    int choice = 0;
    while (choice < 1 || choice > 5)
    {
        try
        {
            Console.WriteLine("choose 1-5 based on level of light the plant needs");
            choice = int.Parse(Console.ReadLine().Trim());
            
        }
        catch (Exception ex)
        {
            
            Console.WriteLine("selecton needs to be a int from 1-5");
        }
        
    }

    var lightSelections = plants.Where(p => p.LightNeeds <= choice).ToList();
    Console.Clear();
    for (int i = 0; i < lightSelections.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {PlantDetails(lightSelections[i])}");
    }
    Console.ReadKey();
}

void stats()
{
    Plant lowestCostPlant= plants[0];
    foreach (Plant plant in plants)
    {
        if (plant.AskingPrice < lowestCostPlant.AskingPrice)
        {
            lowestCostPlant = plant;
        }
    }
    Plant highestLightPlant = plants.Aggregate(plants[0],(highestPlant, plant) => 
    {
        if (plant.LightNeeds > highestPlant.LightNeeds)
        {
            highestPlant = plant;
        }
        return highestPlant;
    });
    
    double aveLightNeeds = plants.Aggregate(0.0, (total, plant) => 
    {
        total += plant.LightNeeds;
        return total;
    }) / plants.Count;

    var adoptedPlants = plants.Where( plant => plant.Sold).ToList();
    double adoptionAve = (double)adoptedPlants.Count / plants.Count * 100;


    Console.WriteLine($"the cheapest plant is {PlantDetails(lowestCostPlant)}");
    Console.WriteLine($"there are {nonSoldPlants.Count} plants available");
    Console.WriteLine($"the plant with the highest light needs is {highestLightPlant.Species}");
    Console.WriteLine($"the average light needs is {aveLightNeeds}");
    Console.WriteLine($"{adoptionAve}% of plants have been adopted");

    Console.ReadKey();
    
}

string greeting = "Hello welcome to ExtraVert select one";
Console.WriteLine(greeting);
string response = null;                                        
while (response != "h")
{
    Console.Clear();
    Console.WriteLine(@"choose an option:
                        a. Displat all plants
                        b. Post a plant to be adopted
                        c. Adopt a plant
                        d. Delist a plant
                        e. Plant of the day
                        f. Search for Plants by Light Needs
                        g. ExtraVert Stats
                        h. Exit");
  try
    {
        response = Console.ReadLine();
        
        switch (response)
        {
            
            case "a":
                Console.Clear(); 
                PlantList();
                Console.ReadKey();
                break;
            case "b":
                Console.Clear();
                postPlant();
                Console.ReadKey();
                break;
            case "c":
                Console.Clear();
                AdoptPlant();
                Console.ReadKey();
                break;
            case "d":
                Console.Clear();
                delistPlant();
                break;
            case "e":
                Console.Clear();
                randomPlant();
                Console.ReadKey();
                break;
            case "f":
                Console.Clear();
                searchByLight();
                break;
            case "g":
                Console.Clear();
                stats();
                break;


            case "h": 
                Console.WriteLine("BYE!!");
                break;
            default:
                Console.Clear();
                Console.WriteLine("please select a valid option!");
                break;

        }


    }
    catch(Exception ex)
    {
        Console.WriteLine(ex);
    }
    
    
}     

string PlantDetails(Plant plant)
{
    string plantString = plant.Species;
    return plantString;
}
