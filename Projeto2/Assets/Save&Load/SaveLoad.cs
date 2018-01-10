using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameData
{
    // Variaveis
    public static string PlayerName = "MasterLeo11";
    public static List<int> pila = new List<int>();
}

public class SaveLoad
{
    public static void Save()
    {
        GameData.pila.Add(323);
        //Gravar 1 a 1
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fs = new FileStream("gamesave.bin", FileMode.Create, FileAccess.Write))
        {
            binaryFormatter.Serialize(fs, GameData.PlayerName);
            binaryFormatter.Serialize(fs, GameData.pila[0]);
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
            GameData.pila = (List<int>)binaryFormatter.Deserialize(fs);
            Debug.Log("Olha: " + (List<int>)binaryFormatter.Deserialize(fs));
        }
        
    }

}
