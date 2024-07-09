namespace SendGridDemo.Interfaces;
public interface IEmailingService
{
    public Task<bool> SendDynamicEmails(EmailDTO emailDTO);
    public Task<string> ScheduleDynamicEmails(ScheduledEmailDTO emailDTO);
    public Task<bool> CancelorPauseScheduledEmails(CancelOrPauseDTO cancelOrPauseDTO);
    public Task<bool> DeleteCancellationOrPause(BatchId batchIdObj);

}
