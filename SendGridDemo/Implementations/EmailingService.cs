namespace SendGridDemo.Implementations;

public class EmailingService : IEmailingService
{

    readonly private IConfiguration _configuration;
    readonly private ISender _sender;

    public EmailingService(IConfiguration configuration)
    {
        _configuration = configuration;
        _sender = new SendGridSender(configuration["SendGrid:API_KEY"],
                                     configuration["SendGrid:Domain"],
                                     configuration["SendGrid:AppName"]);

    }

    public async Task<bool> SendDynamicEmails(EmailDTO emailDTO)
    {
        string templateId = _configuration["SendGrid:Templates:Invitation"];
        return await _sender.SendEmailUsingTemplateAsync(templateId, emailDTO.To, emailDTO.Cc, emailDTO.Variables);
    }

    public async Task<string> ScheduleDynamicEmails(ScheduledEmailDTO emailDTO)
    {
        string templateId = _configuration["SendGrid:Templates:Invitation"];
        string batchId = await _sender.GenerateBatchIdAsync();
        var isValidated = await _sender.ValidateBatchIdAsync(batchId);
        var response = _sender.ScheduleEmailUsingTemplateAsync(templateId, emailDTO.To, emailDTO.Cc, emailDTO.Variables,
           emailDTO.SendAt, batchId);
        if (response != null)
        {
            return batchId;
        }
        else return string.Empty;
    }

    public async Task<bool> CancelorPauseScheduledEmails(CancelOrPauseDTO cancelOrPauseDTO)
    {
        return await _sender.CancelorPauseScheduledEmailsAsync(cancelOrPauseDTO.batchId, cancelOrPauseDTO.status);
    }

    public async Task<bool> DeleteCancellationOrPause(BatchId batchIdObj)
    {
        return await _sender.DeleteCancellationOrPauseAsync(batchIdObj.batchId);
    }



}
