using System.Collections.Generic;

namespace GildedRose.Console
{
    public class ItemUpdater
    {
        public void UpdateQuality(List<Item> items)
        {
            foreach (var item in items)
            {
                item.Quality -= 1;
            }
        }
    }
}