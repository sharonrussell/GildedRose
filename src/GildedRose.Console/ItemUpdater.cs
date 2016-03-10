using System.Collections;
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
            switch (item.Name)
            {
                case "Backstage passes to a TAFKAL80ETC concert":
                    CalculateBackStagePassValue(item);
                    break;
                case "Aged Brie":
                    var amountToIncreaseBy = CalculateIncrease(item);
                    IncreaseQuality(item, amountToIncreaseBy);
                    break;
                default:
                    DecreaseQuality(item);
                    break;
            }
        }

        private void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        private void DecreaseQuality(Item item)
        {
            var amountToDecreaseBy = CalculateDecrease(item);

            item.Quality -= amountToDecreaseBy;

            if (item.Quality < 0)
            {
                item.Quality = 0;
            }
        }

        private int CalculateDecrease(Item item)
        {
           var amountToDecreaseBy = item.SellIn < 1 ? 2 : 1;

            if (IsConjured(item.Name))
            {
                amountToDecreaseBy *= 2;
            }

            return amountToDecreaseBy;
        }

        private bool IsConjured(string itemName)
        {
            return itemName.Contains("Conjured");
        }

        private void IncreaseQuality(Item item, int increaseBy)
        {
            item.Quality += increaseBy;
        }

        private int CalculateIncrease(Item item)
        {
            return item.Quality >= 50 ? 0 : 1;
        }

        private void CalculateBackStagePassValue(Item item)
        {
            var daysToConcert = item.SellIn;

            if (daysToConcert == 0)
            {
                item.Quality = 0;
            }
            else if (daysToConcert <= 5)
            {
                item.Quality += 3;
            }
            else if (daysToConcert <= 10)
            {
                item.Quality += 2;
            }
            else
                item.Quality += 1;
        }
    }
}