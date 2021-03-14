using System;
using System.Collections.Generic;
using System.Text;

namespace Testing_Task_Crossinform
{
    class NumberOfOccurrences
    {
        private String Triplet;
        private int Count = 0;
        public String nameOfTriplet { get { return Triplet; } }
        public int numberOfOccurrences { get { return Count; } }

        public NumberOfOccurrences(string Triplet)
        {
            this.Triplet = Triplet;
            Count += 1;
        }

        public void Occurrence()
        {
            Count += 1;
        }
    }
}
