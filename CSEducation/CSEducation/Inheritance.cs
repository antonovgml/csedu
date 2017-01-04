using System;
using Util;
namespace Inheritance
{

    /* 
     * Develop a custom class Human containing basic information about a person and two subclasses of a Human class – Boy and Girl classes. 
     * Represent differences of two classes by adding new methods and properties into subclasses; represent similarities by overriding methods and properties of a base class.
     */

    enum Sex { Male, Female }

    internal class Human
    {
        public byte Age { get; set; }

        public Sex sex { get; }
        public string SexString {
            get
            {
                string result;
                switch (sex)
                {
                    case Sex.Male:
                        result = "man";
                        break;
                    case Sex.Female:
                        result = "woman";
                        break;
                    default:
                        result = "undefined";
                        break;
                }
                return result;
            }
        }

        public string Pronoun
        {
            get
            {
                string result;
                switch (sex)
                {
                    case Sex.Male:
                        result = "his";
                        break;
                    case Sex.Female:
                        result = "her";
                        break;
                    default:
                        result = "its";
                        break;
                }
                return result;
            }

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        

        public Human(Sex sex, string firstName, string lastName, byte age)
        {
            this.sex = sex;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }

        public void BrushTeeth()
        {
            C.p("{0} {1} is BRUSHING {2} TEETH", new string[] { FirstName, LastName, Pronoun });
        }

        public virtual void WashHead()
        {
            C.p("{0} {1} is WASHING {2} HEAD", new string[] { FirstName, LastName, Pronoun });
        }

        public override string ToString()
        {
            return String.Format("{0} {1} is {2} years old", this.SexString, this.FullName, this.Age);
        }
    }

    internal class Boy : Human
    {

        public float PenisLength { get; }

        public Boy(string firstName, string lastName, byte age, float penisLength) : base(Sex.Male, firstName, lastName, age)
        {
            this.PenisLength = penisLength;
        }

        public void ShaveFace()
        {
            C.p("{0} {1} is shaving HIS FACE", new string[] { FirstName, LastName});
        }

        public override void WashHead()
        {
            base.WashHead();
        }

        public override string ToString()
        {
            return String.Format("{0} {1} is {2} years old with penis {3} cm long", this.SexString, this.FullName, this.Age, this.PenisLength);
        }
    }

    internal class Girl : Human
    {
        public byte BreastSize { get; }

        public Girl(string firstName, string lastName, byte age, byte breastSize) : base(Sex.Female, firstName, lastName, age)
        {
            this.BreastSize = breastSize;
        }

        public void ShaveLegs()
        {
            C.p("{0} {1} is shaving HER LEGS", new string[] { FirstName, LastName });
        }

        public override void WashHead()
        {
            C.p("{0} {1} is WASHING HER HEAD with shampoo...", new string[] { FirstName, LastName});
            C.p("{0} {1} is WASHING HER HEAD with shampoo one more time...", new string[] { FirstName, LastName });
            C.p("{0} {1} is WASHING HER HEAD with hair balsam finally", new string[] { FirstName, LastName });
        }

        public override string ToString()
        {
            return String.Format("{0} {1} is {2} years old with breast size {3}", this.SexString, this.FullName, this.Age, this.BreastSize);
        }
    }

}