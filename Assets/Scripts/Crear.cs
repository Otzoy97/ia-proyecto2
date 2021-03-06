using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Crear : MonoBehaviour
{

    public Dropdown dPisos, dSofa, dEscritorio, dSilla, dMesa, dLibrera;
    public GameObject Alert, imgPanel;
    public Text txtAlert;

    void Awake()
    {
        //Agregar a los dropdown
        //this.dPisos = GetComponent<DropDown>();
        this.dPisos.AddOptions(new List<string> { "Piso 1", "Piso 2", "Piso 3", "Piso 4", "Piso 5", "Piso 6" });
        this.dPisos.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dPisos);
        });
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
        Image image = imgPanel.transform.GetChild(0).GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("floor1");
    }

    void DropdownValueChanged(Dropdown change)
    {
        Image image = imgPanel.transform.GetChild(0).GetComponent<Image>();
        switch (change.captionText.text)
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

    public void CrearEspacio()
    {
        // Verifica que no se crean m??s de 6 espacios
        if (Principal.Espacios.Count >= 6)
        {
            newAccion("[ERROR] Se intent?? registrar m??s de 6 espacios\n");
            openAlert("No se pueden crear m??s de 6 espacios!");
            return;
        }
        string piso = this.dPisos.captionText.text;
        // Verifica que no se usa el misma espacio dos veces
        foreach (var esc in Principal.Espacios)
        {
            string[] espacio = (string[])esc.Value;

            if (piso.Equals(espacio[0]))
            {
                newAccion("[ERROR] Se intent?? utilizar el mismo piso en 2 espacios distintos\n");
                openAlert("ERROR: Ya existe un espacio con este mismo piso");
                return;
            }
        }

        string sofa = this.dSofa.captionText.text;
        string escritorio = this.dEscritorio.captionText.text;
        string silla = this.dSilla.captionText.text;
        string mesa = this.dMesa.captionText.text;
        string librera = this.dLibrera.captionText.text;
        // Verifica que no haya duplicados
        var tempList = new List<string> { sofa, escritorio, silla, mesa, librera };
        //IEnumerable<string> duplicados = tempList.GroupBy(x => y).Where(g => g.Count() > 1).Select(x => x.Key);
        if (tempList.Count != tempList.Distinct().Count())
        {
            newAccion("[ERROR] Se intent?? colocar dos muebles en una misma posici??n\n");
            openAlert("ERROR: Hay dos muebles que se encuentran en la misma posicion");
            return;
        }

        if (!Principal.Espacios.ContainsKey("1"))
        {
            Principal.Espacios.Add("1", new string[] { piso, sofa, escritorio, silla, mesa, librera });
            Principal.index.Add(1);
            newAccion("[ACCION] El usuario cre?? espacio 1\n");
        }
        else if (!Principal.Espacios.ContainsKey("2"))
        {
            Principal.Espacios.Add("2", new string[] { piso, sofa, escritorio, silla, mesa, librera });
            Principal.index.Add(2);
            newAccion("[ACCION] El usuario cre?? espacio 2\n");
        }
        else if (!Principal.Espacios.ContainsKey("3"))
        {
            Principal.Espacios.Add("3", new string[] { piso, sofa, escritorio, silla, mesa, librera });
            Principal.index.Add(3);
            newAccion("[ACCION] El usuario cre?? espacio 3\n");
        }
        else if (!Principal.Espacios.ContainsKey("4"))
        {
            Principal.Espacios.Add("4", new string[] { piso, sofa, escritorio, silla, mesa, librera });
            Principal.index.Add(4);
            newAccion("[ACCION] El usuario cre?? espacio 4\n");
        }
        else if (!Principal.Espacios.ContainsKey("5"))
        {
            Principal.Espacios.Add("5", new string[] { piso, sofa, escritorio, silla, mesa, librera });
            Principal.index.Add(5);
            newAccion("[ACCION] El usuario cre?? espacio 5\n");
        }
        else
        {
            Principal.Espacios.Add("6", new string[] { piso, sofa, escritorio, silla, mesa, librera });
            Principal.index.Add(6);
            newAccion("[ACCION] El usuario cre?? espacio 6\n");
        }
        Principal.index.Sort();
        openAlert("Espacio creado con exito!");
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
