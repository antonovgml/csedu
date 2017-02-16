using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{

    /*
     Develop custom class Crew which implements IList, Worker is a custom class containing personal info and his working position. Also implement IComparer to sort workers by their working position.
     */

    public class Worker
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string workPosition { get; set; }
        public byte age { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} (age {2}) - {3}", firstName, lastName, age, workPosition);
        }

    }

    public class Crew<T> : IList<T>, IComparer<T> where T: Worker
    {
        private List<T> team = new List<T>();


        T IList<T>.this[int index]
        {
            get
            {
                return team[index];
            }

            set
            {
                team[index] = value;
            }
        }

        int ICollection<T>.Count
        {
            get
            {
                return team.Count;
            }
        }

        bool ICollection<T>.IsReadOnly
        {
            get
            {
                return ((ICollection<T>)team).IsReadOnly;
            }
        }


        public void Add(T item)
        {
            team.Add(item);
        }

        void ICollection<T>.Clear()
        {
            team.Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            return team.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            team.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return team.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return team.GetEnumerator();
        }

        int IList<T>.IndexOf(T item)
        {
            return team.IndexOf(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            team.Insert(index, item);
        }

        bool ICollection<T>.Remove(T item)
        {
            return team.Remove(item);
        }

        void IList<T>.RemoveAt(int index)
        {
            team.RemoveAt(index);
        }
        
        int IComparer<T>.Compare(T x, T y)
        {
            return x.workPosition.CompareTo(y.workPosition);
        }

        public T[] ToArray()
        {
            return team.ToArray();
        }
    }
    
}