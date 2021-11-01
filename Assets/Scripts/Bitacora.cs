using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Bitacora : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initBitacora(){
        string ruta = Application.dataPath +  "/bitacora_201602782_201602723.txt";  // En carpeta Assets
        if(!File.Exists(ruta))
        {
            File.WriteAllText(ruta, "            Cliente: twitch.tv/k7droid\n\n");
        }
    }
    public void newAccion(string msj)
    {
        string ruta = Application.dataPath +  "/bitacora_201602782_201602723.txt";  // En carpeta Assets
        if(File.Exists(ruta))
        {
            File.AppendAllText(ruta, msj);  // "[ACCION] Se realizo una accion\n"
        }
    }
}
