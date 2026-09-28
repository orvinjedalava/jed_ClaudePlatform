namespace GameScratch.Contracts.DTOs;

public record EquipmentDto
{
    public required WeaponDto Weapon { get; set; }
}