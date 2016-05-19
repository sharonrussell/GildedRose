using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public class ItemUpdater
    {
        public void UpdateItems(List<Item> items)
        {
            foreach (var item in items.Where(item => item.Name != "Sulfuras, Hand of Ragnaros"))
            {
                UpdateQuality(item);
                UpdateSellIn(item);
            }
        }

        private void UpdateQuality(Item item)
        {
            if (IncreasesOverTime(item.Name))
                IncreaseQuality(item);
            else
                DecreaseQuality(item);
        }

        private void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        private void DecreaseQuality(Item item)
        {
            if(item.Quality - CalculateIncrease(item) >= 0)
                item.Quality -= CalculateDecrease(item);
        }

        private int CalculateDecrease(Item item)
        {
            var amountToDecreaseBy = item.SellIn < 1 ? 2 : 1;

            if (IsConjured(item.Name))
                amountToDecreaseBy *= 2;

            return amountToDecreaseBy;
        }

        private bool IsConjured(string itemName)
        {
            return itemName.Contains("Conjured");
        }

        private bool IncreasesOverTime(string itemName)
        {
            return IsAgedBrie(itemName) || IsBackStagePass(itemName);
        }

        private static bool IsBackStagePass(string itemName)
        {
            return itemName.Equals("Backstage passes to a TAFKAL80ETC concert");
        }

        private static bool IsAgedBrie(string itemName)
        {
            return itemName.Equals("Aged Brie");
        }

        private void IncreaseQuality(Item item)
        {
            if (IsBackStagePass(item.Name))
                UpdateBackStagePass(item);
            else
                item.Quality += CalculateIncrease(item);
        }

        private int CalculateIncrease(Item item)
        {
            return item.Quality >= 50 ? 0 : 1;
        }

        private void UpdateBackStagePass(Item item)
        {
            if (item.SellIn == 0)
                item.Quality = 0;
            else
                item.Quality += CalculateBackStagePassQuality(item);
        }

        private int CalculateBackStagePassQuality(Item item)
        {
            if (item.SellIn <= 5)
                return 3;

            return item.SellIn <= 10 ? 2 : 1;
        }
    }
}