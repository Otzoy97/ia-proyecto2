using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour{

    //Objetos
    public Dropdown pisos,luces,escritorios,sillas,paredes;
    public Toggle tEsc1,tEsc2,tEsc3;
    public GameObject panel1,panel2,panel3,panel;
    private static Dictionary<string, string[]> Espacios = new Dictionary<string, string[]>();
    private static List<int> index = new List<int>();

    void Awake() {
        if(this.pisos!=null){
            List<string> ListaPiso = new List<string> {"Piso 1","Piso 2","Piso 3"};
            List<string> ListaLuces = new List<string> {"Azul","Verde","Rojo"};
            List<string> ListaEscritorio = new List<string> {
                "Superior izquierda","Superior derecha",
                "Centro","Inferior izquierda","Inferior derecho"};
            List<string> ListaSilla = new List<string> {
                "Superior izquierda","Superior derecha",
                "Centro", "Inferior izquierda", "Inferior derecho"};
            List<string> ListaPared = new List<string> {"Pared 1","Pared 2"};

            //Agregar a los dropdown
            this.pisos.AddOptions(ListaPiso);
            this.luces.AddOptions(ListaLuces);
            this.escritorios.AddOptions(ListaEscritorio);
            this.sillas.AddOptions(ListaSilla);
            this.paredes.AddOptions(ListaPared);
        }else if(tEsc1!=null){
            cargarEliminar();
        }
    }

    private void cargarEliminar(){
        tEsc1.isOn = false;
        tEsc2.isOn = false;
        tEsc3.isOn = false;
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(false);

        for(int i = 1; i <= index.Count; i++){
            if(i==1) panel = panel1;
            else if(i==2) panel = panel2;
            else if(i==3) panel = panel3;

            panel.SetActive(true);
            Toggle ch = panel.GetComponentInChildren<Toggle>();
            ch.GetComponentInChildren<Text>().text = "          Espacio #"+index[i-1];
        }
    }

    public void CrearEspacio(){
        if(Espacios.Count < 3){
            string piso = this.pisos.captionText.text;
            string luz = this.luces.captionText.text;
            string escritorio = this.escritorios.captionText.text;
            string silla = this.sillas.captionText.text;
            string pared = this.paredes.captionText.text;

            if(!Espacios.ContainsKey("1")) {
                Espacios.Add("1", new string[]{ piso, luz, escritorio, silla, pared });
                index.Add(1);
            }else if(!Espacios.ContainsKey("2")) {
                Espacios.Add("2", new string[]{ piso, luz, escritorio, silla, pared });
                index.Add(2);
            }else {
                Espacios.Add("3", new string[]{ piso, luz, escritorio, silla, pared });
                index.Add(3);
            }
            index.Sort();
            
            Debug.Log("Espacio creado con exito!");
        }else{
            Debug.Log("No se pueden crear más de 3 espacios!");
        }
    }

    public void EliminarEspacio(){
        string mensaje = "";
        bool msg = false;
        List<int> indices = new List<int>();

        if(tEsc1.isOn){
            mensaje += "Se eliminó el espacio #"+index[0];
            indices.Add(index[0]);
            msg = true;

        }
        if(tEsc2.isOn){
            mensaje += "Se eliminó el espacio #"+index[1];
            indices.Add(index[1]);
            msg = true;
        }
        if(tEsc3.isOn){
            mensaje += "Se eliminó el espacio #"+index[2];
            indices.Add(index[2]);
            msg = true;
        }

        if(msg){
            for(int i = 0; i < indices.Count; i++){
                Debug.Log(indices[i]);
                Espacios.Remove(indices[i].ToString());
                index.Remove(indices[i]);
            }
            cargarEliminar();
            Debug.Log(mensaje);
        }else{
            Debug.Log("No se ha seleccionado ningun espacio");
        }
    }

    public void GoScene(string nameScene){ SceneManager.LoadScene(nameScene); }
}
