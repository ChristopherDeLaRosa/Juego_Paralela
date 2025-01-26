using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("¡Bienvenido a la Carrera de Vehículos Autónomos!");
        Console.WriteLine("Iniciando la simulación...\n");

        // Lista de vehículos
        List<string> vehicles = new List<string> { "Coche 1", "Coche 2", "Coche 3", "Coche 4" };

        // Diccionario para rastrear el progreso de cada vehículo
        Dictionary<string, int> progress = vehicles.ToDictionary(v => v, v => 0);

        // Lista de tareas para cada vehículo
        List<Task> vehicleTasks = vehicles.Select(vehicle => SimulateVehicle(vehicle, progress)).ToList();

        // Esperar a que cualquier vehículo termine la carrera
        Task winner = await Task.WhenAny(vehicleTasks);

        // Mostrar resultados
        Console.WriteLine("\nCarrera finalizada.");
        foreach (var vehicle in progress)
        {
            Console.WriteLine($"{vehicle.Key}: {vehicle.Value}% completado.");
        }
    }

    static async Task SimulateVehicle(string vehicleName, Dictionary<string, int> progress)
    {
        Console.WriteLine($"{vehicleName} ha comenzado la carrera.");
        Random random = new Random();

        try
        {
            // Simular la carrera en etapas
            for (int i = 0; i <= 100; i += 10)
            {
                // Simular el progreso con un retraso aleatorio
                await Task.Delay(random.Next(200, 500));
                progress[vehicleName] = i;
                Console.WriteLine($"{vehicleName} ha alcanzado el {i}% de la meta.");

                // Simular fallo aleatorio
                if (random.NextDouble() < 0.1)
                {
                    throw new Exception($"{vehicleName} ha sufrido un fallo y abandonará la carrera.");
                }
            }

            Console.WriteLine($"{vehicleName} ha llegado a la meta!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine($"{vehicleName} ha terminado su participación en la carrera.");
        }
    }
}
