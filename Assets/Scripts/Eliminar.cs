using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Eliminar : MonoBehaviour
{

    //public Toggle tEsc1, tEsc2, tEsc3, tEsc4, tEsc5, tEs6;
    public Toggle[] espacios = new Toggle[6];
    public GameObject[] paneles = new GameObject[6];
    //public GameObject panel1, panel2, panel3, panel4, panel5, panel6, panel;
    public GameObject Alert;
    public Text txtAlert;

    void Awake() { cargarEliminar(); }

    private void cargarEliminar()
    {
        Toggle ch;
        Image image;
        string[] escena;
        for (int i = 0; i < 6; i++)
        {
            //espacios[i].isOn = false;
            paneles[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < Principal.index.Count; i++)
        {
            paneles[i].SetActive(true);
            ch = paneles[i].GetComponentInChildren<Toggle>();
            image = paneles[i].transform.GetChild(0).GetComponent<Image>();

            ch.GetComponentInChildren<Text>().text = "          Espacio #" + Principal.index[i];
            escena = Principal.Espacios[Principal.index[i] + ""];
            switch (escena[0])
            {
                case "Piso 1":
                    image.sprite = Resources.Load<Sprite>("floor1");
                    break;
                case "Piso 2":
                    image.sprite = Resources.Load<Sprite>("floor2");
                    break;
                case "Piso 3":
                    image.sprite = Resources.Load<Sprite>("floor3");
                    break;
                case "Piso 4":
                    image.sprite = Resources.Load<Sprite>("floor4");
                    break;
                case "Piso 5":
                    image.sprite = Resources.Load<Sprite>("floor5");
                    break;
                case "Piso 6":
                    image.sprite = Resources.Load<Sprite>("usac");
                    break;
            }
            image.enabled = true;
        }
    }

    public void EliminarEspacio()
    {
        string mensaje = "";
        //bool msg = false;
        List<int> indices = new List<int>();

        for (int i = 0; i < espacios.Length; i++)
        {
            if (espacios[i].isOn)
            {
                mensaje += "Se eliminó el espacio #" + Principal.index[i] + "\n";
                indices.Add(Principal.index[i]);
                //msg = true;
                newAccion("[ACCION] El usuario elimino espacio " + Principal.index[i] + "\n");
            }
        }
        if (indices.Count > 0)
        {
            for (int i = 0; i < indices.Count; i++)
            {
                Principal.Espacios.Remove(indices[i].ToString());
                Principal.index.Remove(indices[i]);
            }
            cargarEliminar();
            openAlert(mensaje);
        }
        else
        {
            newAccion("[ERROR] Se intentó eliminar espacio sin seleccionarlo\n");
            openAlert("No se ha seleccionado ningun espacio.");
        }
    }

    private void openAlert(string msg)
    {
        this.txtAlert.text = msg;
        this.Alert.gameObject.SetActive(true);
    }

    public void cerrarAlert()
    {
        this.Alert.gameObject.SetActive(false);
    }

    public void GoScene()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void newAccion(string msj)
    {
        string ruta = Application.dataPath + "/../bitacora_201602782_201602723.txt";  // En carpeta Assets
        if (!File.Exists(ruta))
        {
            File.WriteAllText(ruta, "            Cliente: twitch.tv/k7droid\n\n");
        }
        File.AppendAllText(ruta, msj);
    }
}
