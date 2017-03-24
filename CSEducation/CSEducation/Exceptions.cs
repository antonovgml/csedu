using System;
using System.Runtime.Serialization;
using Validation;

namespace CustomExceptions
{

    /*
     Develop console application to store a list of workers. Develop a custom exception class which will throw when user tries to add a worker with incorrect personal info.
     */


    class IncorrectWorkerException : Exception
    {
        public MyWorker Worker { get; private set; }

        public IncorrectWorkerException(MyWorker worker): base(String.Format("One of the object properties is not valid value: {0}", (worker==null)?null:worker.ToString()))
        {
            Worker = worker;
        }

        public IncorrectWorkerException(string message) : base(message) { }

        public IncorrectWorkerException(MyWorker worker, string message): base(message)
        {
            Worker = worker;
        }

        public IncorrectWorkerException(MyWorker worker, string message, Exception innerException): base(message, innerException)
        {
            Worker = worker;            
        }



    }

    class WorkerValidator
    {
        IValidate nameVtor;
        IValidate ageVtor;
        public WorkerValidator()
        {
            nameVtor = new NameValidator();
            ageVtor = new RangeValidator(16, 50);

        }

        public bool validate(MyWorker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException();
            }

            if (!nameVtor.validate(worker.FirstName))
            {
                throw new IncorrectWorkerException(worker, "First name is not valid value");
            }

            if (!nameVtor.validate(worker.LastName))
            {
                throw new IncorrectWorkerException("Last name is not valid value: " + worker.LastName);
            }

            if (!ageVtor.validate(worker.Age.ToString()))
            {
                throw new IncorrectWorkerException(worker);
            }


            return true;
        }
    }

    class MyWorker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public string JobPosition { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1} {2} ({3} years old) - {4}", this.GetType().Name, this.FirstName, this.LastName, this.Age, this.JobPosition);
        }
    }


}