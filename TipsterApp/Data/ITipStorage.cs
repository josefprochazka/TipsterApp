namespace TipsterApp.Data
{
    public interface ITipStorage
    {
        Task<List<TipRecord>> GetAllAsync();
        Task<TipRecord?> GetByTableIdAsync(int tableId);
        Task AddOrUpdateAsync(TipRecord record);
    }
}
