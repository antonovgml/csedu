using System;
using System.Collections;
using System.Collections.Generic;

namespace AnimalWorld
{

    /* Abstract classes */

    abstract class Animal
    {
        protected static Random rnd = new Random();
        public abstract void eat();        

        public string nickName { get; set; }

        public Animal(string nickName)
        {
            this.nickName = nickName;
        }

        public override string ToString()
        {
            return String.Format("{0,-30} {1}", this.GetType(), this.nickName);
        }
    }

    // позвоночные
    abstract class Vertebrata: Animal
    {
        public Vertebrata() : base(Guid.NewGuid().ToString()) { }
        public Vertebrata(string nickName) : base(nickName) { }

    }

    abstract class Amphibia : Vertebrata
    {
        public Amphibia() { }
        public Amphibia(string nickName):base(nickName) { }
    }
    abstract class Reptilia : Vertebrata
    {
        public Reptilia() { }
        public Reptilia(string nickName):base(nickName) { }
    }
    // birds
    abstract class Aves : Vertebrata
    {
        public Aves() { }
        public Aves(string nickName):base(nickName) { }
    } 
    abstract class Mammalia : Vertebrata {
        public Mammalia() { }
        public Mammalia(string nickName):base(nickName) { }

    }

    /* Interfaces */

    interface IFly
    {
        uint maxDistance { get; set; } // max fly distance

        // returns true if successfully flew over the distance. Otherwise returns false (dies)
        bool fly(uint distance);
    }

    interface ISwim
    {
        uint maxDistance { get; set; }

        // returns true if successfully swimmed over the distance. Otherwise returns false (dies)
        bool swim(uint distance);
    }

    interface IJump
    {
        uint maxDistance { get; set; }

        // returns true if successfully jumped over the distance. Otherwise returns false (dies)
        bool jump(uint distance);
    }


    /* Concrete classes */

    class Frog : Amphibia, ISwim, IJump
    {
        uint ISwim.maxDistance { get; set; } = (uint)rnd.Next(1, 100);

        public Frog(string nickName) : base(nickName) { }

        bool ISwim.swim(uint distance)
        {
            Console.WriteLine("{0} is swimming {1}. Its max is {2}...", this, distance, ((ISwim)this).maxDistance);
            return distance <= ((ISwim)this).maxDistance;
        }

        uint IJump.maxDistance { get; set; } = (uint)rnd.Next(1, 100);

        bool IJump.jump(uint distance) {
            Console.WriteLine("{0} is jumping {1}. Its max is {2}...", this, distance, ((IJump)this).maxDistance);
            return distance <= ((IJump)this).maxDistance;
        }

        public override void eat()  { Console.WriteLine("{0} is eating an insect", this); }

    }

    class Crocodile: Reptilia, ISwim
    {
        uint ISwim.maxDistance { get; set; } = (uint)rnd.Next(1, 100);

        public Crocodile(string nickName) : base(nickName) { }

        bool ISwim.swim(uint distance) {
            Console.WriteLine("{0} is swimming {1}. Its max is {2}...", this, distance, ((ISwim)this).maxDistance);
            return distance <= ((ISwim)this).maxDistance;
        }

        public override void eat() { Console.WriteLine("{0} is eating a piece of meat", this); }

    }

    class Penguin : Aves, ISwim
    {
        uint ISwim.maxDistance { get; set; } = (uint)rnd.Next(1, 100);

        public Penguin(string nickName) : base(nickName) { }

        public override void eat() { Console.WriteLine("{0} is eating a fish", this); }

        bool ISwim.swim(uint distance) {
            Console.WriteLine("{0} is swimming {1}. Its max is {2}...", this, distance, ((ISwim)this).maxDistance);
            return distance <= ((ISwim)this).maxDistance;
            //Console.WriteLine("{this} is swimming {0} meters"); 
        }

    }

    class Eagle : Aves, IFly
    {
        uint IFly.maxDistance { get; set; } = (uint)rnd.Next(1, 100);

        public Eagle(string nickName) : base(nickName) { }

        public override void eat() { Console.WriteLine("{0} is eating a rabbit", this); }

        bool IFly.fly(uint distance)
        {
            Console.WriteLine("{0} is flying {1}. Its max is {2}...", this, distance, ((IFly)this).maxDistance);
            return distance <= ((IFly)this).maxDistance;            
        }
    }

    class Cat : Mammalia, IJump, ISwim
    {
        uint IJump.maxDistance { get; set; } = (uint)rnd.Next(1, 100);
        uint ISwim.maxDistance { get; set; } = (uint)rnd.Next(1, 100);

        public Cat(string nickName) : base(nickName) { }

        public override void eat() { Console.WriteLine("{0} is eating a mouse", this); }

        bool IJump.jump(uint distance) {
            Console.WriteLine("{0} is jumping {1}. Its max is {2}...", this, distance, ((IJump)this).maxDistance);
            return distance <= ((IJump)this).maxDistance;
        }

        bool ISwim.swim(uint distance) {
            Console.WriteLine("{0} is swimming {1}. Its max is {2}...", this, distance, ((ISwim)this).maxDistance);
            return distance <= ((ISwim)this).maxDistance;
        }
    }

    /* Group of animals */

    class Group<T>:IEnumerable<T> where T: Animal
    {

        bool empty{
            get {
                return _animals.Count == 0;
            }
        }
        List<T> _animals = new List<T>();


        public Group<T> add(T elem)
        {
            _animals.Add(elem);
            return this;
        }

        public bool remove(T elem)
        {
            return _animals.Remove(elem);
        }

        /* Implementing IEnumerable */
        public IEnumerator<T> GetEnumerator()
        {
            return _animals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _animals.GetEnumerator();
        }
    }

}