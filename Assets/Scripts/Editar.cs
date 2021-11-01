using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Editar : MonoBehaviour
{
    public Dropdown dPisosActivos, dPisos, dSofa, dEscritorio, dSilla, dMesa, dLibrera;
    public GameObject Alert, imgPanelActivo, imgPanel;
    public Text txtAlert;
    private List<string> pisos;
    private Dictionary<string, string> imgSprites = new Dictionary<string, string>
        {
            { "Piso 1", "floor1" },
            { "Piso 2", "floor2" },
            { "Piso 3", "floor3" },
            { "Piso 4", "floor4" },
            { "Piso 5", "floor5" },
            { "Piso 6", "usac" }
        };
    void Awake()
    {
        //Agregar a los dropdown
        pisos = new List<string>();
        foreach (KeyValuePair<string, string[]> esc in Principal.Espacios)
        {
            string[] espacio = esc.Value;
            pisos.Add(espacio[0]);
        }
        // Indica si hay espacios para editar
        if (pisos.Count <= 0)
        {
            newAccion("[WARN] Se accedió a editar espacios, pero no hay ninguno creado\n");
            openAlert("No hay espacios para editar");
        }
        else
        {
            setImageSprite(imgPanelActivo, pisos[0]);
        }
        // Agrega los espacios activos
        this.dPisosActivos.AddOptions(pisos);
        this.dPisosActivos.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dPisosActivos, imgPanelActivo);
        });
        // Agrega todos los espacios
        this.dPisos.AddOptions(new List<string> { "Piso 1", "Piso 2", "Piso 3", "Piso 4", "Piso 5", "Piso 6" });
        this.dPisos.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dPisos, imgPanel);
        });
        setImageSprite(imgPanel, "Piso 1");
        this.dSofa.AddOptions(new List<string> {
            "Superior izquierda","Superior derecha",
            "Centro", "Inferior izquierda", "Inferior derecha"});
        this.dEscritorio.AddOptions(new List<string> {
            "Superior izquierda","Superior derecha",
            "Centro", "Inferior izquierda", "Inferior derecha"});
        this.dSilla.AddOptions(new List<string> {
            "Superior izquierda","Superior derecha",
            "Centro", "Inferior izquierda", "Inferior derecha"});
        this.dMesa.AddOptions(new List<string> {
            "Superior izquierda","Superior derecha",
            "Centro", "Inferior izquierda", "Inferior derecha"});
        this.dLibrera.AddOptions(new List<string> {
            "Superior izquierda","Superior derecha",
            "Centro", "Inferior izquierda", "Inferior derecha"});
    }


    void setImageSprite(GameObject panel, string pisoName)
    {
        Image image = panel.transform.GetChild(0).GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>(imgSprites[pisoName]);
        image.enabled = true;
    }
    void DropdownValueChanged(Dropdown change, GameObject panel)
    {
        setImageSprite(panel, change.captionText.text);
    }

    public void EditarEspacio()
    {
        // Si no hay espacios creados, no se puede editar nada
        if (pisos.Count <= 0)
        {
            newAccion("[ERROR] Se intentó editar espacios, pero no hay ninguno creado\n");
            openAlert("No hay espacios para editar");
            return;
        }
        // Recupera la posición de los muebles
        string sofa = this.dSofa.captionText.text;
        string escritorio = this.dEscritorio.captionText.text;
        string silla = this.dSilla.captionText.text;
        string mesa = this.dMesa.captionText.text;
        string librera = this.dLibrera.captionText.text;
        // Verifica que no muebles en posiciones duplicadas
        var tempList = new List<string> { sofa, escritorio, silla, mesa, librera };
        if (tempList.Count != tempList.Distinct().Count())
        {
            newAccion("[ERROR] Se intentó colocar dos muebles en una misma posición\n");
            openAlert("ERROR: Hay dos muebles que se encuentran en la misma posicion");
            return;
        }
        string pisoNuevo = this.dPisos.captionText.text;
        string pisoAnterior = this.dPisosActivos.captionText.text;
        string pisoAnteriorKey = ""; // Llave del piso anterior
        // Busca la llave del piso
        foreach (KeyValuePair<string, string[]> esc in Principal.Espacios)
        {
            if (pisoAnterior.Equals(esc.Value[0]))
            {
                pisoAnteriorKey = esc.Key;
            }
        }
        // El pisoNuevo y el pisoAnterior son iguales
        if (pisoNuevo.Equals(pisoAnterior))
        {
            // Actualiza el valor en el diccionario
            Principal.Espacios[pisoAnteriorKey] = new string[] { pisoAnterior, sofa, escritorio, silla, mesa, librera };        // if (!Principal.Espacios.ContainsKey("1"))
            newAccion("[ACCION] El usuario editó espacio " + pisoAnteriorKey + "\n");
            openAlert("Espacio editado con exito!");
        }
        else
        {
            // Verifica que no se usa el misma espacio dos veces
            foreach (KeyValuePair<string, string[]> esc in Principal.Espacios)
            {
                string[] espacio = esc.Value;

                if (pisoNuevo.Equals(espacio[0]))
                {
                    newAccion("[ERROR] El " + pisoNuevo + " ya se está usando. Elimine el espacio " + esc.Key + "\n");
                    openAlert("ERROR: El " + pisoNuevo + " ya se está usando. Primero elimine el espacio " + esc.Key + "\n");
                    return;
                }
            }
            // Actualiza el valor en el diccionario
            Principal.Espacios[pisoAnteriorKey] = new string[] { pisoNuevo, sofa, escritorio, silla, mesa, librera };        // if (!Principal.Espacios.ContainsKey("1"))
            newAccion("[ACCION] El usuario editó espacio " + pisoAnteriorKey + "\n");
            openAlert("Espacio editado con exito!");
        }
        if (Input.GetKeyDown(0))
        {
            GoScene();
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

