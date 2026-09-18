namespace GameScratch.Core.Common.Weapons;

public class WeaponsFactory
{
    public static Weapon BareHands = WeaponBuilder.Create().FromWeaponType(WeaponType.ShortSword).Build();
}

public class WeaponBuilder
{
    private Weapon? _weapon;

    public static WeaponBuilder Create() => new();
    public WeaponBuilder FromWeaponType(WeaponType? weaponType)
    {
        if (weaponType == null)
            weaponType = WeaponType.ShortSword;

        switch(weaponType)
        {
            case WeaponType.ShortSword:
                _weapon = new()
                {
                    Name = weaponType.Value.ToString(),
                    HitPointsDamageDiceType = weaponType.Value.GetHitPointsDamageDiceType(),
                    StaminaPointsDamage = weaponType.Value.GetStaminaPointsDamage(),
                    StaminaCost = weaponType.Value.GetStaminaCost(),
                    PoiseDamageModifier = weaponType.Value.GetPoiseDamageModifier(),
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