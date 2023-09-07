using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Mappers;
using OnlineExam.Application.Services;
using OnlineExam.Application.Validators;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ISectionMapper, SectionMapper>();
            serviceDescriptors.AddScoped<ISectionService, SectionService>();
            serviceDescriptors.AddScoped<IExamService, ExamService>();
            serviceDescriptors.AddScoped<IExamMapper, ExamMapper>();
            serviceDescriptors.AddScoped<IQuestionService, QuestionService>();
            serviceDescriptors.AddScoped<IQuestionMapper, QuestionMapper>();
            serviceDescriptors.AddScoped<ITextFieldMapper, TextFieldMapper>();
            serviceDescriptors.AddScoped<ITextFieldService, TextFieldService>();
            serviceDescriptors.AddScoped<ITextFieldValidator, TextFieldValidator>();
            serviceDescriptors.AddScoped<ICheckFieldMapper, CheckFieldMapper>();
            serviceDescriptors.AddScoped<ICheckFieldService, CheckFieldService>();
            serviceDescriptors.AddScoped<ICheckFieldValidator, CheckFieldValidator>();
            serviceDescriptors.AddScoped<ICheckFieldOptionService, CheckFieldOptionService>();
            serviceDescriptors.AddScoped<ICheckFieldOptionMapper, CheckFieldOptionMapper>();
            serviceDescriptors.AddScoped<ICheckFieldOptionValidator, CheckFieldOptionValidator>();
            serviceDescriptors.AddScoped<IDatabaseBasedCheckFieldOptionValidator, DatabaseBasedCheckFieldOptionValidator>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldService, AllowedFileTypesFieldService>();
            serviceDescriptors.AddScoped<IFileFieldMapper, FileFieldMapper>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldMapper, AllowedFileTypesFieldMapper>();
            serviceDescriptors.AddScoped<IDatabaseBasedAllowedFileTypesFieldValidator, DatabaseBasedAllowedFileTypesFieldValidator>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldValidator, AllowedFileTypesFieldValidator>();
            serviceDescriptors.AddScoped<IFileFieldValidator, FileFieldValidator>();
        }
    }
}
