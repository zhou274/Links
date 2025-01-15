namespace GameTemplate.Scripts
{
    public static class PlayerPrefsConsts
    {
        public static class Player
        {
            public const string Coins = "player.coins";
            public const string LastDailyAward = "player.lastDailyAward";
        }

        public static class Settings
        {
            public const string IsSoundEffectsEnabled = "settings.sfx.enabled";
            public const string IsMusicEnabled = "settings.music.enabled";
            public const string IsVibrateEnabled = "settings.vibrate.enabled";
            public const string Volume = "settings.volume";

            public const int ValueEnabled = 1;
            public const int ValueDisabled = -1;

        }

        public static class Collections
        {
            public const string State = "collection.{0}.state";
        }

        public static class Levels
        {
            public const string State = "collection.{0}.level.{1}.state";
            public const string Stars = "collection.{0}.level.{1}.stars";
        }
    }
}