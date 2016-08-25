namespace Core
{
    public interface SendKeyDocumentLinkNotificationRequest
    {
        string To { get; }
        string FrequencyCode { get; }
        string NotificationType { get; }
        int DocumentId { get; }
        string DownloadLink { get; }
        string Message { get; }
    }
}
