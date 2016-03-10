using System.Collections.Generic;

namespace GildedRose.Console
{
    public class ItemUpdater
    {
        public void UpdateItems(List<Item> items)
        {
            foreach (var item in items)
            {
                UpdateQuality(item);
                UpdateSellIn(item);
            }
        }

        private void UpdateQuality(Item item)
        {
            var decreasesBy = item.SellIn < 1 ? 2 : 1;

            item.Quality -= decreasesBy;

            if (item.Quality < 0)
            {
                item.Quality = 0;
            }
        }

        private void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }
    }
}