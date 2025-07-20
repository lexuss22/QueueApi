using OfficeOpenXml;
using Queue.BLL.Services.Interfaces;
using Queue.DAL.Entities;
using Queue.DAL.Repositories.Interfaces;
using Queue.DTO.Models;
using System.Globalization;
using System.Text.Json;

namespace Queue.BLL.Services
{
    public class ImportExportServices : IImportExportServices
    {
        private readonly IImportExportRepository _repository;

        public ImportExportServices(IImportExportRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> ImportFromExcelAsync(Stream fileStream)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using var package = new ExcelPackage(fileStream);

                var worksheet = package.Workbook.Worksheets[0];
                var newSchedulesDto = new List<GroupScheduleDTO>();

                for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    var cellValue = worksheet.Cells[row, 1].Text.Trim();
                    if (string.IsNullOrWhiteSpace(cellValue)) continue;

                    var dotIndex = cellValue.IndexOf('.');
                    if (dotIndex == -1) throw new Exception($"Ошибка формата в строке {row}");

                    if (!int.TryParse(cellValue[..dotIndex], out int groupNumber))
                        throw new Exception($"Ошибка формата номера группы в строке {row}");

                    var periodsPart = cellValue[(dotIndex + 1)..].Trim();
                    var periodStrings = periodsPart.Split(';', StringSplitOptions.RemoveEmptyEntries);

                    var periods = new List<SchedulePeriodDTO>();
                    foreach (var periodString in periodStrings)
                    {
                        var times = periodString.Trim().Split('-', StringSplitOptions.RemoveEmptyEntries);
                        if (times.Length != 2) throw new Exception($"Ошибка формата интервала времени в строке {row}");

                        if (!TimeOnly.TryParseExact(times[0].Trim(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var start))
                            throw new Exception($"Ошибка времени начала в строке {row}");

                        if (!TimeOnly.TryParseExact(times[1].Trim(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var end))
                            throw new Exception($"Ошибка времени окончания в строке {row}");

                        periods.Add(new SchedulePeriodDTO { StartTime = start, EndTime = end });
                    }

                    newSchedulesDto.Add(new GroupScheduleDTO { GroupNumber = groupNumber, Periods = periods });
                }
                var newScedules = MapToGroupSchedules(newSchedulesDto);
                
                await _repository.AddSchedulesAsync(newScedules);
                return "Файл успешно импортирован и данные сохранены в базу.";
            }
            catch (Exception ex)
            {
                return $"Ошибка при импорте: {ex.Message}";
            }
        }

        public async Task<byte[]> ExportToJsonAsync()
        {
            var schedules = await _repository.GetAllSchedulesAsync();
            var schedulesDto = MapToGroupScheduleDTOs(schedules);
            if (schedules.Count == 0) throw new Exception("Нет данных для экспорта.");

            var json = JsonSerializer.Serialize(schedulesDto, new JsonSerializerOptions { WriteIndented = true });
            return System.Text.Encoding.UTF8.GetBytes(json);
        }

        private List<GroupSchedule> MapToGroupSchedules(List<GroupScheduleDTO> dtos)
        {
            return dtos.Select(dto => new GroupSchedule
            {
                GroupNumber = dto.GroupNumber,
                Periods = dto.Periods.Select(p => new SchedulePeriod
                {
                    StartTime = p.StartTime,
                    EndTime = p.EndTime
                }).ToList()
            }).ToList();
        }

        private List<GroupScheduleDTO> MapToGroupScheduleDTOs(List<GroupSchedule> schedules)
        {
            return schedules.Select(s => new GroupScheduleDTO
            {
                GroupNumber = s.GroupNumber,
                Periods = s.Periods.Select(p => new SchedulePeriodDTO
                {
                    StartTime = p.StartTime,
                    EndTime = p.EndTime
                }).ToList()
            }).ToList();
        }
    }
}
