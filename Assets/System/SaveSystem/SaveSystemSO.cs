using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
[CreateAssetMenu(fileName = "SaveSystem", menuName = "System/Save System")]
public class SaveSystemSO: ScriptableObject
{
    public SessionHandlerSO session;
    public static void LoadSession(){

    }

    public static void SaveSession(){
        
    }


}
