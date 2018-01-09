using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData
{
    // Variaveis
    public static string PlayerName = "MasterLeo11";
}

public class SaveLoad
{
    public static void Save()
    {
        //Gravar 1 a 1
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fs = new FileStream("gamesave.bin", FileMode.Create, FileAccess.Write))
        {
            binaryFormatter.Serialize(fs, GameData.PlayerName);
        }
    }
    
    public static void Load()
    {
        if (!File.Exists("gamesave.bin"))
            return;

        // Carregar 1 a 1
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fs = new FileStream("gamesave.bin", FileMode.Open, FileAccess.Read))
        {
            GameData.PlayerName = (string)binaryFormatter.Deserialize(fs);
        }
        
    }

}
