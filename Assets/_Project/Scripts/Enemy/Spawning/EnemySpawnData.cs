namespace gameoff.Enemy
{
    public class EnemySpawnData
    {
        public Hive HiveOrigin { get; private set; }
        public string PrefabID { get; private set; }

        public EnemySpawnData(Hive hiveOrigin, string prefabID)
        {
            HiveOrigin = hiveOrigin;
            PrefabID = prefabID;
        }
    }
}