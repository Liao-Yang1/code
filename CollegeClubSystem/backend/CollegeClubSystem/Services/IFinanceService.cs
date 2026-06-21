public interface IFinanceService
{
    Task<FinanceRecord> CreateFinanceRecord(FinanceRequest request, int userId);
    Task<List<FinanceRecord>> GetFinanceRecords(int clubId);
    Task<bool> ApproveFinanceRecord(int recordId);
}