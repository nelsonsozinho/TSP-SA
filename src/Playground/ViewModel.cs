using System;
using System.Collections.Generic;
using TspSA.Domain;

namespace Playground
{
    class ViewModel
    {
        private readonly List<City> _cities = new List<City>();
        private Planner _planner;
 
        public ViewModel()
        {
            _planner = new Planner();
        }
        
        public void PickAt(int x, int y)
        {
            var index = (_cities.FindIndex(c =>
                Math.Abs(c.LocationX - x) < 0.001 &&
                Math.Abs(c.LocationY - y) < 0.001 
                )) ;

            if (index != -1)
            {
                _cities.RemoveAt(index);
            }

            _cities.Add(new City(x, y));
            _planner = new Planner(_cities.ToArray());
        }

        public IEnumerable<City> Cities
        {
            get { return 
                _cities; }
        }

        public bool CanCompute { get { return _cities.Count > 2; } }
        public void Complete() { _planner.Complete(); }
        public void Iterate() { _planner.Iterate();}

        public void Reset()
        {
            _cities.Clear();
            _planner = new Planner(_cities.ToArray());
        }

        public IEnumerable<City> Route { get { return _planner.BestSolution.Route; } }
        public int PlanningGeneration { get { return _planner.Generation; } }

        
    }
}
