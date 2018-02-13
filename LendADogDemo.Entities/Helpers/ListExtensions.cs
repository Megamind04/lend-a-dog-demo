using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendADogDemo.Entities.Helpers
{
    public static class ListExtensions
    {
        public static IEnumerable<List<T>> SplitList<T>(this List<T> listToBeSplit,int size)
        {
            int countListItems = listToBeSplit.Count;

            for (int i = 0; i < countListItems; i+=size)
            {
                yield return listToBeSplit.GetRange(i, Math.Min(size, countListItems - i));
            }
        }
    }
}
