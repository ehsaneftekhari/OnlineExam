﻿using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IAllowedFileTypesFieldService
    {
        ShowAllowedFileTypesFieldDTO Add(AddAllowedFileTypesFieldDTO dTO);
        ShowAllowedFileTypesFieldDTO? GetById(int id);
        IEnumerable<ShowAllowedFileTypesFieldDTO> GetAll(int skip = 0, int take = 20);
        void Update(int id, UpdateAllowedFileTypesFieldDTO dTO);
        void Delete(int id);
    }
}