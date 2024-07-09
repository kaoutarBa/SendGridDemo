namespace SendGridDemo;

public record EmailDTO(
                       List<string> To,
                       List<string> Cc,
                       Dictionary<string, string> Variables);


public record ScheduledEmailDTO(
                       List<string> To,
                       List<string> Cc,
                       Dictionary<string, string> Variables,
                       long SendAt);


public record CancelOrPauseDTO(string batchId, string status);

public record BatchId(string batchId);  
