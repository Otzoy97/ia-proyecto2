using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Eliminar : MonoBehaviour {
    
    public Toggle tEsc1,tEsc2,tEsc3;
    public GameObject panel1,panel2,panel3,panel;
    public GameObject Alert;
    public Text txtAlert;

    void Awake() { cargarEliminar(); }

    private void cargarEliminar(){
        tEsc1.isOn = false;
        tEsc2.isOn = false;
        tEsc3.isOn = false;
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(false);

        for(int i = 1; i <= Principal.index.Count; i++){
            if(i==1) panel = panel1;
            else if(i==2) panel = panel2;
            else if(i==3) panel = panel3;

            panel.SetActive(true);
            Toggle ch = panel.GetComponentInChildren<Toggle>();
            Image img = panel.transform.GetChild(0).GetComponent<Image>();

            ch.GetComponentInChildren<Text>().text = "          Espacio #" + Principal.index[i-1];
            string[] escena = Principal.Espacios[Principal.index[i-1]+""];
            string nombre;

            if(escena[0].Equals("Piso 1")){
                nombre = "casa";
            }else if(escena[0].Equals("Piso 2")){
                nombre = "perro";
            }else{
                nombre = "usac";
            }
            img.enabled = true;
            img.sprite = Resources.Load<Sprite>(nombre);
        }
    }

    public void EliminarEspacio(){
        string mensaje = "";
        bool msg = false;
        List<int> indices = new List<int>();

        if(tEsc1.isOn){
            mensaje += "Se eliminó el espacio #" + Principal.index[0] + "\n";
            indices.Add(Principal.index[0]);
            msg = true;
            newAccion("[ACCION] El usuario elimino espacio "+Principal.index[0]+"\n");
        }
        if(tEsc2.isOn){
            mensaje += "Se eliminó el espacio #" + Principal.index[1] + "\n";
            indices.Add(Principal.index[1]);
            msg = true;
            newAccion("[ACCION] El usuario elimino espacio "+Principal.index[1]+"\n");
        }
        if(tEsc3.isOn){
            mensaje += "Se eliminó el espacio #" + Principal.index[2] + "\n";
            indices.Add(Principal.index[2]);
            msg = true;
            newAccion("[ACCION] El usuario elimino espacio "+Principal.index[2]+"\n");
        }

        if(msg){
            for(int i = 0; i < indices.Count; i++){
                Principal.Espacios.Remove(indices[i].ToString());
                Principal.index.Remove(indices[i]);
            }
            cargarEliminar();
            openAlert(mensaje);
        }else{
            newAccion("[ERROR] Se intentó eliminar espacio sin seleccionarlo\n");
            openAlert("No se ha seleccionado ningun espacio.");
        }
    }

    private void openAlert(string msg){
        this.txtAlert.text = msg;
        this.Alert.gameObject.SetActive(true);
    }

    public void cerrarAlert(){
        this.Alert.gameObject.SetActive(false);
    }

    public void GoScene(){
        SceneManager.LoadScene("MenuPrincipal");
    }
    
    public void newAccion(string msj)
    {
        string ruta = Application.dataPath +  "/../bitacora_201602782_201602723.txt";  // En carpeta Assets
        if(!File.Exists(ruta))
        {
            File.WriteAllText(ruta, "            ---> Bitacora - Proyecto IA1 <---\n\n");
        }
        File.AppendAllText(ruta, msj);
    }
}
