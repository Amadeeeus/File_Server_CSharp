using FileServer.Models.DTOs;
using FileServer.Models.Views;
using FileServer.Services.Implementations;
using FluentValidation;

namespace FileServer.Models.Validation;

public class FileViewValidation: AbstractValidator<FileView>
{
    public FileViewValidation()
    {
        RuleFor(file=>file.Id).NotNull().NotEmpty();
        RuleFor(file=>file.FileName).Length(1,255).NotEmpty();
        RuleFor(file=>file.ContentType).NotEmpty().Length(1,15);
        RuleFor(file=>file.FilePath).NotEmpty().Length(1,255);
    }
}