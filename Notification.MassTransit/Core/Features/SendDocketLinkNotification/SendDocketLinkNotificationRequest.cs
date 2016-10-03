namespace Core
{
    public interface SendDocketLinkNotificationRequest
    {
        string To { get; }
        int DocketId { get; }
        string DownloadLink { get; }
        string Message { get; }
        string DocumentName { get; }
        string FrequencyCode { get; }
        string NotificationType { get; }
    }
}
