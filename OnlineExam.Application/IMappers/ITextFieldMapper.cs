﻿using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.IMappers
{
    public interface ITextFieldMapper
    {
        TextField? AddDTOToEntity(int questionId, AddTextFieldDTO? addTextFieldDTO);
        ShowTextFieldDTO EntityToShowDTO(TextField addTextFieldDTO);
    }
}