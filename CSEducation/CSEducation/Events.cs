using System;
using Util;

namespace Evt
{

    /*
     Develop custom classes Plant, Herbivore and Carnivore. Plant slowly grows to be eaten by Herbivore, which in its turn is eaten by Carnivore. Implement this interaction using event delegates.
         */



    public class PlantArgs: EventArgs
    {

        public PlantArgs(int len)
        {
            Value = len;
        }

        public int Value { get; set; }
    }

    public class Plant
    {
        public event EventHandler OnGrow = delegate { };


        public void Grow(int size)
        {            
            C.p(this.GetType() + " is growing {0}cm", new string[] { size.ToString()});
            OnGrow(this, new PlantArgs(size));
        }

    }

    public class Herbivore
    {
        private const int FULLUP_THRESHOLD = 100;
        private int ate = 0;
        public bool Alive { get; private set; } = true;

        public event EventHandler OnEat = delegate { };
        public event EventHandler OnSleep = delegate { };

        public void Graze(Plant plant)
        {
            plant.OnGrow += (sender, evtArgs) =>
            {
                ate += ((PlantArgs)evtArgs).Value;
                OnEat(this, null);
                C.p(this.GetType() + " is eating {0}cm of {1}", new string[] { ate.ToString(), sender.GetType().ToString() });
                if (ate >= FULLUP_THRESHOLD)
                {
                    Sleep();
                }
            };
        }

        private void Sleep()
        {
            C.p("It is time to sleep. Bzzzzzzz...");
            OnSleep(this, null);
        }
        
        public void kill()
        {
            C.p(this.GetType() + " is dead now");
            Alive = false;
        }
    }

    public class Carnivore
    {
        public void Hunt(Herbivore victim)
        {
            victim.OnEat += (sender, evt) => C.p(this.GetType() + " is watching...");
            victim.OnSleep += Eat;
        }

        private void Eat(Object sender, EventArgs args)
        {
            C.p(this.GetType() + " is killing the " + sender.GetType());
            ((Herbivore) sender).kill();
            C.p(this.GetType() + " is Eating the " + sender.GetType());
        }

    }

}