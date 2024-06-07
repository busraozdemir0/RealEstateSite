﻿using RealEstate.API.DTOs.AppUserDtos;

namespace RealEstate.API.Models.Repositories.AppUserRepositories
{
    public interface IAppUserRepository
    {
        Task<GetAppUserByProductIdDto> GetAppUserByProductId(int id); // ilani ekleyen kullanicinin bilgilerini gosterecegiz
    }
}