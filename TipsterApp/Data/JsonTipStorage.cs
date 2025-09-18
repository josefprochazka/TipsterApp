using System.Text.Json;

namespace TipsterApp.Data
{
    public class JsonTipStorage : ITipStorage
    {
        private readonly string _filePath = "tips.json";

        public async Task<List<TipRecord>> GetAllAsync()
        {
            if (!File.Exists(_filePath))
                return new List<TipRecord>();

            using var stream = File.OpenRead(_filePath);
            var tips = await JsonSerializer.DeserializeAsync<List<TipRecord>>(stream);
            return tips ?? new List<TipRecord>();
        }

        public async Task<TipRecord?> GetByTableIdAsync(int tableId)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(t => t.TableId == tableId);
        }

        public async Task AddOrUpdateAsync(TipRecord record)
        {
            var all = await GetAllAsync();
            var existing = all.FirstOrDefault(t => t.TableId == record.TableId);
            if (existing != null)
            {
                all.Remove(existing);
            }
            all.Add(record);

            using var stream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(stream, all);
        }
    }

}
