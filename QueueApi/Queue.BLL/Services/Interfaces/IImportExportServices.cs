namespace Queue.BLL.Services.Interfaces
{
    public interface IImportExportServices
    {
        Task<string> ImportFromExcelAsync(Stream fileStream);
        Task<byte[]> ExportToJsonAsync();
    }
}
