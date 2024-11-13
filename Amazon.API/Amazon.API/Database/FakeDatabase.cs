using Amazon.MAUI.Models;

namespace Amazon.API.Database
{
    public static class FakeDatabase
    {
        public static int LastItemId
        {
            get
            {
                if (Items?.Any() ?? false)
                {
                    return Items?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }
        public static List<Item> Items { get; } = new List<Item>() {

                new Item{Name = "Item 1", Description = "spicy", Id = 1, Price = 5.5M, Quantity = 3, IsBOGO = false, Markdown = 0},
                new Item{Name = "Item 2", Description = "mild", Id = 2, Price = 10M, Quantity = 15, IsBOGO = true, Markdown = 0},
                new Item{Name = "Item 3", Description = "medium", Id = 3, Price = 6M, Quantity = 4, IsBOGO = false, Markdown = 10},
                new Item{Name = "Item 4", Description = "test", Id = 4, Price = 7M, Quantity = 5, IsBOGO = false, Markdown = 0},
            };
    }
}
