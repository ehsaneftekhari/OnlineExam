﻿using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface ICheckFieldInternalService : IBaseInternalService<CheckField, int, Question, int>
    {

    }
}
