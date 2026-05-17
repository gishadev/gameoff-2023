namespace gameoff.Enemy
{
    public class HiveEnemySpawnData : IEnemySpawnData
    {
        public Hive HiveOrigin { get; }
        public string PrefabID { get; }

        public HiveEnemySpawnData(Hive hiveOrigin, string prefabID)
        {
            HiveOrigin = hiveOrigin;
            PrefabID = prefabID;
        }
    }

    public interface IEnemySpawnData
    {
        public string PrefabID { get; }
    }
}