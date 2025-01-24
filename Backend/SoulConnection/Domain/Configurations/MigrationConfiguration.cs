namespace Domain.Configurations;

public class MigrationConfiguration
{
    public SynchronizationPeriod SynchronizationPeriod { get; set; }
    public int DataBatchSize { get; set; }
    public DelayAfterBatch DelayAfterBatch { get; set; }
}