namespace service_access_eventual.Services;

public interface IScanService
{
    Task<(bool success, object data)> ProcessScanAsync(string qrUuid, int scannedByEmployeeId);
}