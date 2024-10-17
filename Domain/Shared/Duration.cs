using System;

namespace DDDSample1.Domain.Shared
{

    public class Duration : IValueObject
    {

        public int value { get; private set; }

        public Duration(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Duration value must be greater than 0.");
            } else 
            this.value = value;

        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Duration other = (Duration)obj;
            return value == other.value;
        }

        public override int GetHashCode(){

            return value.GetHashCode();

        }


    }


}