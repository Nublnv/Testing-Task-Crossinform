using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Testing_Task_Crossinform
{
    class Top
    {
        private ConcurrentQueue<NumberOfOccurrences> top;
        public Top()
        {
            top = new ConcurrentQueue<NumberOfOccurrences>();
        }

        public void Add(string Triplet)
        {
            foreach (var item in top)
            {
                if (Triplet == item.nameOfTriplet)
                {
                    item.Occurrence();
                    return;
                }
            }
            top.Enqueue(new NumberOfOccurrences(Triplet));
        }

        private void Swap(ref NumberOfOccurrences x, ref NumberOfOccurrences y)
        {
            var t = x;
            x = y;
            y = t;
        }

        private int Partition(NumberOfOccurrences[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i].numberOfOccurrences > array[maxIndex].numberOfOccurrences)
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        private NumberOfOccurrences[] QuickSort(NumberOfOccurrences[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        private NumberOfOccurrences[] QuickSort(NumberOfOccurrences[] array)
        {
            return QuickSort(array, 0, array.Length - 1);
        }

        public List<NumberOfOccurrences> Top10()
        {
            top = new ConcurrentQueue<NumberOfOccurrences>(QuickSort(top.ToArray()));
            List<NumberOfOccurrences> top10 = new List<NumberOfOccurrences>();
            if (top.Count >= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    NumberOfOccurrences tmp;
                    top.TryDequeue(out tmp);
                    top10.Add(tmp);
                }
            }
            else
                foreach (var item in top)
                {
                    NumberOfOccurrences tmp;
                    top.TryDequeue(out tmp);
                    top10.Add(tmp);
                }
            return top10;
        }
    }
}
