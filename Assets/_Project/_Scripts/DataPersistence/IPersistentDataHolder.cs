namespace SnekTech.DataPersistence
{
    public interface IPersistentDataHolder
    {
        void LoadData(GameData gameData);
        void SaveData(GameData gameData);
    }
}