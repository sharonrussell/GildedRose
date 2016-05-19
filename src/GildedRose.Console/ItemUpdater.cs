using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public class ItemUpdater
    {
        public void UpdateItems(List<Item> items)
        {
            foreach (var item in items.Where(item => !ItemChecker.IsLegendary(item.Name)))
            {
                UpdateQuality(item);
                UpdateSellIn(item);
            }
        }

        private void UpdateQuality(Item item)
        {
            if (ItemChecker.IncreasesOverTime(item.Name))
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
            if (item.Quality - UpdateCalculator.CalculateIncrease(item) >= 0)
                item.Quality -= UpdateCalculator.CalculateDecrease(item, ItemChecker.IsConjured(item.Name));
        }

        private void IncreaseQuality(Item item)
        {
            if (ItemChecker.IsBackStagePass(item.Name))
                UpdateBackStagePass(item);
            else
                item.Quality += UpdateCalculator.CalculateIncrease(item);
        }

        private void UpdateBackStagePass(Item item)
        {
            if (item.SellIn == 0)
                item.Quality = 0;
            else
                item.Quality += UpdateCalculator.CalculateBackStagePassQualityIncrease(item);
        }
    }
}