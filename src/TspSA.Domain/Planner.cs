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

            CurrentTemperature = 10000d;
            CoolingRate = 0.005;

            CurrentSolution = new Tour(_cities);
            BestSolution = CurrentSolution;
        }

        public double CoolingRate { get; set; }
        public double CurrentTemperature { get; set; }
        public Tour CurrentSolution { get; set; }
        public Tour BestSolution { get; set; }

        public void Iterate()
        {
            
        }

    }
}
