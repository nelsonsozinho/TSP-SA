using System;
using System.Linq;

namespace TspSA.Domain
{
    public class Planner
    {
        private readonly City[] _cities;

        public Planner(
            params City[] cities
            )
        {
            _cities = cities;

            CurrentTemperature = 100000d;
            CoolingRate = 0.0005;
            Generation = 0;

            CurrentSolution = new Tour(_cities);
            BestSolution = CurrentSolution;

            picker = new Random();
        }

        public int Generation { get; private set; }

        public double CoolingRate { get; set; }
        public double CurrentTemperature { get; set; }
        public Tour CurrentSolution { get; set; }
        public Tour BestSolution { get; set; }

        private Random picker;

        public bool IsCompleted { get { return CurrentTemperature < 1; } }
        public void Iterate()
        {
            if (IsCompleted)
            {
                return;
            }

            Generation ++;

            var indexToSwap1 = (int)Math.Round((CurrentSolution.Route.Count() - 1) * picker.NextDouble());
            var indexToSwap2 = (int)Math.Round((CurrentSolution.Route.Count() - 1) * picker.NextDouble());

            var newSolution = CurrentSolution.Swap(indexToSwap1, indexToSwap2);

            if (ShouldUse(newSolution))
            {
                Console.WriteLine("{2} - Replacing {0} with {1}", CurrentSolution.TotalDistance, newSolution.TotalDistance, Generation);
                CurrentSolution = newSolution;
            }

            if (newSolution.IsBestThan(BestSolution))
            {
                Console.WriteLine("Reducing distance from {0} to {1}", BestSolution.TotalDistance, newSolution.TotalDistance);
                BestSolution = newSolution;
            }

            CurrentTemperature *= (1 - CoolingRate);
        }

        public void Complete()
        {
            CurrentTemperature = 100000d;
            Console.Clear();
            while (!IsCompleted)
            {
                Iterate();
            }
        }

        private bool ShouldUse(Tour tour)
        {
            return ComputeAcceptanceProbability(tour) > picker.NextDouble();
        }

        private double ComputeAcceptanceProbability(Tour tour)
        {
            return tour.IsBestThan(CurrentSolution) ? 
                1.0 : 
                Math.Exp((CurrentSolution.TotalDistance - tour.TotalDistance) / CurrentTemperature);
        }
    }
}
