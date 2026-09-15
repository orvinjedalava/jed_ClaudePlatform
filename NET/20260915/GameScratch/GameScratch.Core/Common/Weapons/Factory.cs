namespace GameScratch.Core.Common.Weapons;

public class WeaponBuilder
{
    private Weapon? _weapon;

    public static WeaponBuilder Create() => new();
    public WeaponBuilder FromWeaponType(WeaponType weaponType)
    {
        switch(weaponType)
        {
            case WeaponType.BareHands:
                _weapon = new()
                {
                    Name = weaponType.ToString(),
                    BaseDamage = weaponType.GetDiceType(),
                    StaminaCost = weaponType.GetStaminaCost()
                };
                break;
        }

        return this;
    }

    public Weapon Build()
    {
        return _weapon ?? 
            throw new InvalidOperationException("A weapon type must be selected before building.");
    }
    
}