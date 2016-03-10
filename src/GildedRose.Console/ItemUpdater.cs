using System.Collections.Generic;

namespace GildedRose.Console
{
    public class ItemUpdater
    {
        public void UpdateItems(List<Item> items)
        {
            foreach (var item in items)
            {
                if(item.Name != "Sulfuras, Hand of Ragnaros")
                {
                   UpdateQuality(item);
                   UpdateSellIn(item);
                }
            }
        }

        private void UpdateQuality(Item item)
        {
            if (item.Name == "Aged Brie")
            {
                IncreaseQuality(item);
            }
            else
            {
                DecreaseQuality(item);
            }
        }

        private void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        private void DecreaseQuality(Item item)
        {
            var decreasesBy = item.SellIn < 1 ? 2 : 1;

            item.Quality -= decreasesBy;

            if (item.Quality < 0)
            {
                item.Quality = 0;
            }
        }

        private void IncreaseQuality(Item item)
        {
            if (item.Quality < 50)
                item.Quality += 1;
        }
    }
}