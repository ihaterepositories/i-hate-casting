namespace Models.Weapons.PlayerWeapon.Data
{
    public static class PlayerWeaponStats
    {
        public static int Damage { get; set; }
        public static int MagazineCapacity { get; set; } = 10;
        public static float ReloadTime { get; set; } = 5;
        public static float Spread { get; set; }
    }
}