using FileServer.Repositories.Implementations;
using Quartz;

namespace FileServer.Jobs;

public class DeleteFileJob:IJob
{
    public readonly IFileRepository _fileRepository;

    public DeleteFileJob(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var files = await _fileRepository.GetAllFilesAsync();
        foreach (var file in files)
        {
            if (file.CreatedAt < DateTime.UtcNow - TimeSpan.FromDays(file.ExpireDays))
            {
               await _fileRepository.DeleteFileAsync(file.FileName);
            }
        }
    }
}