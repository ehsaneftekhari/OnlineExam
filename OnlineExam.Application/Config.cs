﻿using Microsoft.Extensions.DependencyInjection;
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
using OnlineExam.Application.Validators;

namespace OnlineExam.Application
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IExamService, ExamService>();
            serviceDescriptors.AddScoped<ExamInternalService>();
            serviceDescriptors.AddScoped<IExamMapper, ExamMapper>();
            serviceDescriptors.AddScoped<IdentityTokenService>();
            serviceDescriptors.AddScoped<IUserService, UserService>();
            serviceDescriptors.AddScoped<ISectionMapper, SectionMapper>();
            serviceDescriptors.AddScoped<ISectionService, SectionService>();
            serviceDescriptors.AddScoped<SectionInternalService>();
            serviceDescriptors.AddScoped<IQuestionService, QuestionService>();
            serviceDescriptors.AddScoped<QuestionInternalService>();
            serviceDescriptors.AddScoped<IQuestionMapper, QuestionMapper>();
            serviceDescriptors.AddScoped<ITextFieldMapper, TextFieldMapper>();
            serviceDescriptors.AddScoped<ITextFieldService, TextFieldService>();
            serviceDescriptors.AddScoped<TextFieldInternalService>();
            serviceDescriptors.AddScoped<ITextFieldValidator, TextFieldValidator>();
            serviceDescriptors.AddScoped<ICheckFieldMapper, CheckFieldMapper>();
            serviceDescriptors.AddScoped<ICheckFieldService, CheckFieldService>();
            serviceDescriptors.AddScoped<CheckFieldInternalService>();
            serviceDescriptors.AddScoped<ICheckFieldValidator, CheckFieldValidator>();
            serviceDescriptors.AddScoped<ICheckFieldOptionService, CheckFieldOptionService>();
            serviceDescriptors.AddScoped<CheckFieldOptionInternalService>();
            serviceDescriptors.AddScoped<ICheckFieldOptionMapper, CheckFieldOptionMapper>();
            serviceDescriptors.AddScoped<ICheckFieldOptionValidator, CheckFieldOptionValidator>();
            serviceDescriptors.AddScoped<IDatabaseBasedCheckFieldOptionValidator, DatabaseBasedCheckFieldOptionValidator>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldService, AllowedFileTypesFieldService>();
            serviceDescriptors.AddScoped<AllowedFileTypesFieldInternalService>();
            serviceDescriptors.AddScoped<IFileFieldMapper, FileFieldMapper>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldMapper, AllowedFileTypesFieldMapper>();
            serviceDescriptors.AddScoped<IDatabaseBasedAllowedFileTypesFieldValidator, DatabaseBasedAllowedFileTypesFieldValidator>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldValidator, AllowedFileTypesFieldValidator>();
            serviceDescriptors.AddScoped<IFileFieldValidator, FileFieldValidator>();
            serviceDescriptors.AddScoped<IFileFieldService, FileFieldService>();
            serviceDescriptors.AddScoped<FileFieldInternalService>();
            serviceDescriptors.AddScoped<IDatabaseBasedExamUserValidator, DatabaseBasedExamUserValidator>();
            serviceDescriptors.AddScoped<IExamUserMapper, ExamUserMapper>();
            serviceDescriptors.AddScoped<IExamUserService, ExamUserService>();
            serviceDescriptors.AddScoped<ExamUserInternalService>();
            serviceDescriptors.AddScoped<IAnswerMapper, AnswerMapper>();
            serviceDescriptors.AddScoped<IAnswerValidator, AnswerValidator>();
            serviceDescriptors.AddScoped<IDatabaseBasedAnswerValidator, DatabaseBasedAnswerValidator>();
            serviceDescriptors.AddScoped<IAnswerService, AnswerService>();
            serviceDescriptors.AddScoped<AnswerInternalService>();
        }
    }
}
