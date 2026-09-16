namespace GameScratch.Core.Common.Weapons;

public class WeaponsFactory
{
    public static Weapon BareHands = WeaponBuilder.Create().FromWeaponType(WeaponType.BareHands).Build();
}

public class WeaponBuilder
{
    private Weapon? _weapon;

    public static WeaponBuilder Create() => new();
    public WeaponBuilder FromWeaponType(WeaponType? weaponType)
    {
        if (weaponType == null)
            weaponType = WeaponType.BareHands;

        switch(weaponType)
        {
            case WeaponType.BareHands:
                _weapon = new()
                {
                    Name = weaponType.Value.ToString(),
                    BaseDamage = weaponType.Value.GetDiceType(),
                    StaminaCost = weaponType.Value.GetStaminaCost(),
                    WeaponType = weaponType.Value
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