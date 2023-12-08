using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Mappers;
using OnlineExam.Application.Services.AnswerServices;
using OnlineExam.Application.Services.CheckFieldServices;
using OnlineExam.Application.Services.ExamServices;
using OnlineExam.Application.Services.ExamUserServices;
using OnlineExam.Application.Services.FileFieldServices;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Application.Services.SectionServices;
using OnlineExam.Application.Services.TextFieldServices;
using OnlineExam.Application.Services.UserServices;
using OnlineExam.Application.Validators;

namespace OnlineExam.Application
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IExamService, ExamService>();
            serviceDescriptors.AddScoped<IExamAccessValidator, ExamAccessValidator>();
            serviceDescriptors.AddScoped<IExamInternalService, ExamInternalService>();
            serviceDescriptors.AddScoped<IExamMapper, ExamMapper>();
            serviceDescriptors.AddScoped<IdentityTokenService>();

            serviceDescriptors.AddScoped<IUserService, UserService>();
            serviceDescriptors.AddScoped<IUserInternalService, UserInternalService>();

            serviceDescriptors.AddScoped<ISectionMapper, SectionMapper>();
            serviceDescriptors.AddScoped<ISectionAccessValidator, SectionAccessValidator>();
            serviceDescriptors.AddScoped<ISectionService, SectionService>();
            serviceDescriptors.AddScoped<ISectionInternalService, SectionInternalService>();

            serviceDescriptors.AddScoped<IQuestionService, QuestionService>();
            serviceDescriptors.AddScoped<IQuestionInternalService, QuestionInternalService>();
            serviceDescriptors.AddScoped<IQuestionAccessValidator, QuestionAccessValidator>();
            serviceDescriptors.AddScoped<IQuestionMapper, QuestionMapper>();

            serviceDescriptors.AddScoped<ITextFieldMapper, TextFieldMapper>();
            serviceDescriptors.AddScoped<ITextFieldService, TextFieldService>();
            serviceDescriptors.AddScoped<ITextFieldInternalService, TextFieldInternalService>();
            serviceDescriptors.AddScoped<ITextFieldValidator, TextFieldValidator>();

            serviceDescriptors.AddScoped<ICheckFieldMapper, CheckFieldMapper>();
            serviceDescriptors.AddScoped<ICheckFieldService, CheckFieldService>();
            serviceDescriptors.AddScoped<ICheckFieldInternalService, CheckFieldInternalService>();
            serviceDescriptors.AddScoped<ICheckFieldDTOValidator, CheckFieldDTOValidator>();
            serviceDescriptors.AddScoped<IQuestionComponentAccessValidator, QuestionComponentAccessValidator>();

            serviceDescriptors.AddScoped<ICheckFieldOptionService, CheckFieldOptionService>();
            serviceDescriptors.AddScoped<ICheckFieldOptionInternalService, CheckFieldOptionInternalService>();
            serviceDescriptors.AddScoped<ICheckFieldOptionMapper, CheckFieldOptionMapper>();
            serviceDescriptors.AddScoped<ICheckFieldOptionDTOValidator, CheckFieldOptionDTOValidator>();
            serviceDescriptors.AddScoped<ICheckFieldOptionRelationValidator, CheckFieldOptionRelationValidator>();
            serviceDescriptors.AddScoped<ICheckFieldOptionAccessValidator, CheckFieldOptionAccessValidator>();

            serviceDescriptors.AddScoped<IAllowedFileTypesFieldService, AllowedFileTypesFieldService>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldInternalService, AllowedFileTypesFieldInternalService>();
            serviceDescriptors.AddScoped<IFileFieldMapper, FileFieldMapper>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldMapper, AllowedFileTypesFieldMapper>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldRelationValidator, AllowedFileTypesFieldRelationValidator>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldDTOValidator, AllowedFileTypesFieldDTOValidator>();
            serviceDescriptors.AddScoped<IFileFieldDTOValidator, FileFieldDTOValidator>();
            serviceDescriptors.AddScoped<IFileFieldService, FileFieldService>();
            serviceDescriptors.AddScoped<IFileFieldInternalService, FileFieldInternalService>();

            serviceDescriptors.AddScoped<IExamUserMapper, ExamUserMapper>();
            serviceDescriptors.AddScoped<IExamUserService, ExamUserService>();
            serviceDescriptors.AddScoped<IExamUserInternalService, ExamUserInternalService>();
            serviceDescriptors.AddScoped<IExamUserDTOValidator, ExamUserDTOValidator>();
            serviceDescriptors.AddScoped<IExamUserAccessValidator, ExamUserAccessValidator>();
            serviceDescriptors.AddScoped<IExamUserActionValidator, ExamUserActionValidator>();
            
            serviceDescriptors.AddScoped<IAnswerMapper, AnswerMapper>();
            serviceDescriptors.AddScoped<IAnswerValidator, AnswerValidator>();
            serviceDescriptors.AddScoped<IDatabaseBasedAnswerValidator, DatabaseBasedAnswerValidator>();
            serviceDescriptors.AddScoped<IAnswerService, AnswerService>();
            serviceDescriptors.AddScoped<IAnswerInternalService, AnswerInternalService>();
        }
    }
}
