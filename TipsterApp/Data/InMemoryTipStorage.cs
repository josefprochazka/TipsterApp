namespace TipsterApp.Data
{
    public class InMemoryTipStorage : ITipStorage
    {
        private readonly List<TipRecord> _tips = new();

        public Task<List<TipRecord>> GetAllAsync() => Task.FromResult(_tips);

        public Task<TipRecord?> GetByTableIdAsync(int tableId)
            => Task.FromResult(_tips.FirstOrDefault(t => t.TableId == tableId));

        public Task AddOrUpdateAsync(TipRecord record)
        {
            var existing = _tips.FirstOrDefault(t => t.TableId == record.TableId);
            if (existing != null)
            {
                existing.TipAmount = record.TipAmount;
                existing.TipPercent = record.TipPercent;
                existing.Email = record.Email;
                existing.Rating = record.Rating;
                existing.Updated = DateTime.Now;
                existing.IsActive = record.IsActive;
            }
            else
            {
                record.Inserted = DateTime.Now;
                record.Updated = DateTime.Now;
                _tips.Add(record);
            }
            return Task.CompletedTask;
        }
    }

}
