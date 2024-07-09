namespace SendGridDemo.Endpoints;

public static class Emailing
{
    public static void ConfigureEmailing(this WebApplication app)
    {
        app.MapPost("/api/sendDynamicEmails", SendDynamicEmails)
          .WithName("sendDynamicEmails")
          .Produces<Object>(200)
          .Produces(401)
          .Produces(400);

        app.MapPost("/api/scheduleEmails", ScheduleEmails)
          .WithName("scheduleEmails")
          .Produces<Object>(200)
          .Produces(401)
          .Produces(400);

        app.MapPost("/api/cancelOrPauseSendingEmails", CancelOrPauseSendingScheduledEmails)
          .WithName("cancelOrPauseSendingEmails")
          .Produces<Object>(201)
          .Produces(401)
          .Produces(400);

        app.MapDelete("/api/deleteCalcellationOrPause", DeleteCalcellationOrPause)
          .WithName("deleteCalcellationOrPause")
          .Produces<Object>(201)
          .Produces(401)
          .Produces(400);


    }
    private async static Task<IResult> SendDynamicEmails(IConfiguration _config,
                                                         IEmailingService _emailingService,
                                                         [FromBody] EmailDTO emailDTO)
    {
        var sent = await _emailingService.SendDynamicEmails(emailDTO);
        if (sent == true)
        {
            return Results.Ok();
        }
        return Results.Problem();

    }
    private async static Task<IResult> ScheduleEmails(IConfiguration _config,
                                                         IEmailingService _emailingService,
                                                         [FromBody] ScheduledEmailDTO emailDTO)
    {
        string batchId = await _emailingService.ScheduleDynamicEmails(emailDTO);
        if (batchId != null)
        {
            return Results.Ok(batchId);
        }
        return Results.Problem();
    }

    private async static Task<IResult> CancelOrPauseSendingScheduledEmails(IConfiguration _config,
                                                     IEmailingService _emailingService,
                                                     [FromBody] CancelOrPauseDTO cancelOrPauseDTO)
    {
        bool canceled = await _emailingService.CancelorPauseScheduledEmails(cancelOrPauseDTO);
        if (canceled != false)
        {
            return Results.Ok(canceled);
        }
        return Results.Problem();
    }

    private async static Task<IResult> DeleteCalcellationOrPause(IConfiguration _config,
                                                     IEmailingService _emailingService,
                                                     [FromBody] BatchId batchIdObj)
    {
        bool deleted = await _emailingService.DeleteCancellationOrPause(batchIdObj);
        if (deleted != false)
        {
            return Results.Ok(deleted);
        }
        return Results.Problem();
    }



}
