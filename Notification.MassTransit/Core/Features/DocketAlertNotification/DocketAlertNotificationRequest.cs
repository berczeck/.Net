namespace Core
{
    public class DocketAlertNotificationRequest
    {
        public int DocketId { get; }
        public DocketAlertNotificationRequest(int docketId)
        {
            DocketId = docketId;
        }
    }
}
