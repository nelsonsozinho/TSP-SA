using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TspSA.Domain;

namespace Playground
{
    class ViewModel
    {
        private readonly List<City> _cities = new List<City>();
 
        public ViewModel()
        {
            
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
        }

        public IEnumerable<City> Cities
        {
            get { return 
                _cities; }
        }

    }
}
