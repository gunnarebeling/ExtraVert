// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Xml;

List<Plant> plants = new List<Plant>()
{
    new Plant
    {
        Species = "Ficus",
        LightNeeds = 3, // 1=Low, 2=Medium, 3=High
        AskingPrice = 25.50m,
        City = "Los Angeles",
        ZIP = "90001",
        Sold = false
    },
    new Plant
    {
        Species = "Snake Plant",
        LightNeeds = 1,
        AskingPrice = 15.00m,
        City = "New York",
        ZIP = "10001",
        Sold = true
    },
    new Plant
    {
        Species = "Spider Plant",
        LightNeeds = 2,
        AskingPrice = 18.75m,
        City = "Austin",
        ZIP = "73301",
        Sold = false
    },
    new Plant
    {
        Species = "Aloe Vera",
        LightNeeds = 3,
        AskingPrice = 12.30m,
        City = "Phoenix",
        ZIP = "85001",
        Sold = true
    },
    new Plant
    {
        Species = "Peace Lily",
        LightNeeds = 1,
        AskingPrice = 20.00m,
        City = "Chicago",
        ZIP = "60601",
        Sold = false
    }
};
void PlantList()
{
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold? "was sold" : "is avaiable")} for {plants[i].AskingPrice} dollars");
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

    Plant newPlant = 
    new Plant()
    {
        Species = newSpecies,
        LightNeeds = newLightNeeds,
        AskingPrice = newAskingPrice,
        City = newCity,
        ZIP = newZip,
        Sold = false


    };

    plants.Add(newPlant);
    Console.WriteLine($"{newPlant.Species} has been successfully added");

}
void AdoptPlant()
{
    var nonSoldPlants = plants.Where(plant => !plant.Sold).ToList();
    int Choice = 0;
    while (Choice > nonSoldPlants.Count || Choice < 1)
    {
        try
        {   Console.Clear();
            Console.WriteLine(@"Adopt one of these Plants");
            for (int i = 0; i < nonSoldPlants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {nonSoldPlants[i].Species}");
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
                Console.WriteLine($"{i + 1}. {plants[i].Species}");
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
string greeting = "Hello welcome to ExtraVert select one";
Console.WriteLine(greeting);
string response = null;                                        
while (response != "e")
{
    Console.WriteLine(@"choose an option:
                        a. Displat all plants
                        b. Post a plant to be adopted
                        c. Adopt a plant
                        d. Delist a plant
                        e. Exit");
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
