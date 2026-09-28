using GameScratch.Contracts.DTOs;
using GameScratch.Core.Common.Players;

namespace GameScratch.MinimalApi.Mappers;

public static class ProfileMapper
{
    public static ProfileDto ToDto(this Profile profile) => 
        new()
        {
            RoleType = profile.RoleType.ToString(),
            Name = profile.Name
        };
}