using TipsterApp.Data;

namespace TipsterApp.Services
{
    public class StatsBackgroundService : IHostedService, IDisposable
    {
        private readonly ITipStorage _storage;
        private Timer _timer;
        public StatsData Stats { get; private set; } = new();

        public StatsBackgroundService(ITipStorage storage)
        {
            _storage = storage;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateStats, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        private async void UpdateStats(object state)
        {
            var tips = await _storage.GetAllAsync();

            Stats.TotalTips = tips.Sum(t => t.TipAmount);
            Stats.HighestTip = tips.OrderByDescending(t => t.TipAmount).FirstOrDefault();
            Stats.AvgPercent = tips.Any() ? tips.Average(t => t.TipPercent) : 0;
            Stats.AvgRating = tips.Any() ? tips.Average(t => t.Rating) : 0;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }

    public class StatsData
    {
        public decimal TotalTips { get; set; }
        public TipRecord HighestTip { get; set; }
        public double AvgPercent { get; set; }
        public double AvgRating { get; set; }
    }

}
