using System;
using System.Collections.Generic;

namespace gilded_rose
{
	public class GildedRose
	{
		private static readonly Dictionary<string, IProduct> Products =
			new Dictionary<string, IProduct>
			{
				{"Aged Brie", new Product(new QualityAdjuster(+1))},
				{"Backstage passes to a TAFKAL80ETC concert",
					new Product(new BackstageQualityAdjuster(+1))},
				{"Sulfuras, Hand of Ragnaros", new SulfurasProduct()},
				{"Default Item", new Product(new QualityAdjuster(-1))}
			};

		public void UpdateQuality(List<Item> items)
		{
			foreach (Item item in items)
			{
				if (Products.TryGetValue(item.Name, out IProduct product))
					product.Update(item);
				else {
					var defaultProduct = Products.GetValueOrDefault("Default Item");
					defaultProduct.Update(item);
				}
			}
		}
	}

	public interface IProduct
	{
		void Update(Item item);
	}

	public class Product : IProduct
	{
		private readonly IAdjustQuality _qualityAdjuster;

		public Product(IAdjustQuality qualityAdjuster)
		{
			_qualityAdjuster = qualityAdjuster;
		}

		public void Update(Item item)
		{
			item.SellIn = item.SellIn - 1;
			item.Quality = item.Quality + (_qualityAdjuster.Adjust(item));

			item.Quality = Math.Min(50, item.Quality);
			item.Quality = Math.Max(0, item.Quality);
		}
	}

	public class SulfurasProduct : IProduct
	{
		public void Update(Item item)
		{
		}
	}

	public interface IAdjustQuality
	{
		int Adjust(Item item);
	}

	public class QualityAdjuster : IAdjustQuality
	{
		private readonly int _defaultQuality;

		public QualityAdjuster(int defaultQuality)
		{
			_defaultQuality = defaultQuality;
		}

		public int Adjust(Item item)
		{
			return item.SellIn < 0 ? 2 * _defaultQuality : _defaultQuality;
		}
	}

	public class BackstageQualityAdjuster : IAdjustQuality
	{
		private readonly int _defaultQuality;

		public BackstageQualityAdjuster(int defaultQuality)
		{
			_defaultQuality = defaultQuality;
		}

		public int Adjust(Item item)
		{
			if (item.SellIn < 0)
				return -item.Quality;
			if (item.SellIn < 5)
				return +3;
			if (item.SellIn < 10)
				return +2;

			return _defaultQuality;
		}
	}
}