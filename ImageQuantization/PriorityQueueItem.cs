using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    //Class Represent The Item Pushed In The Priority Queue Impelements IComparable Interface 
    public class PriorityQueueItem : IComparable<PriorityQueueItem>
    {
        public double Key = new double();
        public int Value = new int();

        //Constructor To Set Values
        public PriorityQueueItem(double key, int value)
        {
            Key = key;
            Value = value;
        }
        //Overrideing Function To Compare Between Priority Queue Items
        public int CompareTo(PriorityQueueItem other)
        {
            int ret = -1;
            //If First Item < Second Item
            if (Key < other.Key)
                ret = -1;
            //If First Item > Second Item
            else if (Key > other.Key)
                ret = 1;
            //If First Item = Second Item
            else if (Key == other.Key)
                ret = 0;
            return ret;
        }
    }
}
