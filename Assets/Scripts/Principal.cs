using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Principal : MonoBehaviour {
    public static Dictionary<string, string[]> Espacios = new Dictionary<string, string[]>();
    public static List<int> index = new List<int>();

    public void GoScene(string nameScene){
        if(nameScene=="VerEspacio"){
            newAccion("[ACCION] El usuario visualizo los espacios\n");
        }
        
        SceneManager.LoadScene(nameScene);
    }

    public void newAccion(string msj)
    {
        string ruta = Application.dataPath +  "/../bitacora_201602782_201602723.txt";  // En carpeta Assets
        if(!File.Exists(ruta))
        {
            File.WriteAllText(ruta, "            Cliente: twitch.tv/k7droid\n\n");
        }
        File.AppendAllText(ruta, msj);
    }
}
