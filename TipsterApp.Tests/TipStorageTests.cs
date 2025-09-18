using TipsterApp.Data;

public class TipStorageTests
{
    [Fact]
    public async Task AddOrUpdateAsync_SavesAndLoadsTip()
    {
        var storage = new InMemoryTipStorage();
        var tip = new TipRecord
        {
            TableId = 123,
            BillAmount = 200,
            TipAmount = 40,
            TipPercent = 20,
            Email = "test@email.cz",
            Rating = 5,
            Inserted = DateTime.Now,
            Updated = DateTime.Now,
            IsActive = true
        };

        await storage.AddOrUpdateAsync(tip);
        var all = await storage.GetAllAsync();

        Assert.Single(all);
        Assert.Equal(123, all[0].TableId);
        Assert.Equal(40, all[0].TipAmount);
    }
}
