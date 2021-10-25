using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Muebles : MonoBehaviour {
    public GameObject piso1, piso2, piso3, piso4, piso5, piso6;
    
    void Awake() {
        piso1.gameObject.SetActive(false);
        piso2.gameObject.SetActive(false);
        piso3.gameObject.SetActive(false);
        piso4.gameObject.SetActive(false);
        piso5.gameObject.SetActive(false);
        piso6.gameObject.SetActive(false);

        foreach (var esc in Principal.Espacios) {
            string[] espacio = (string[]) esc.Value;
            switch(espacio[0]) {
                case "Piso 1":
                    establecerParametros(piso1, espacio, "Piso 1");            
                    break;
                case "Piso 2":
                    establecerParametros(piso2, espacio, "Piso 2");
                    break;
                case "Piso 3":
                    establecerParametros(piso2, espacio, "Piso 3");
                    break;
                case "Piso 4":
                    establecerParametros(piso2, espacio, "Piso 4");
                    break;
                case "Piso 5":
                    establecerParametros(piso2, espacio, "Piso 5");
                    break;
                default:
                    establecerParametros(piso3, espacio, "Piso 6");
                    break;
            }
        }
    }

   private void establecerParametros(GameObject espacio, string[] escenario, string pActual){
        Debug.Log("============= " + pActual + " =============");
        Transform cuadro = espacio.transform.GetChild(0);
        Transform silla = espacio.transform.GetChild(1);
        Transform desktop = espacio.transform.GetChild(2);/*
        Debug.Log("Antes Escritorio: " + "[" + 
                    desktop.transform.position[0] + "]-[" + 
                    desktop.transform.position[1] + "]-[" + 
                    desktop.transform.position[2]+"]"); 
        Debug.Log("Antes Silla: " + "[" + 
                    silla.transform.position[0] + "]-[" + 
                    silla.transform.position[1] + "]-[" + 
                    silla.transform.position[2]+"]"); */
        List<double> posCuadro = Cuadro(pActual, escenario[4]);
        List<double> posSilla = Silla(pActual, escenario[3].ToLower(), escenario[2].ToLower());
        List<double> posDesktop = Escritorio(pActual, escenario[2].ToLower(), escenario[3].ToLower());
        //Posiciones
        cuadro.transform.position = new Vector3((float) posCuadro[0], (float) posCuadro[1], (float) posCuadro[2]);                    
        silla.transform.position = new Vector3((float) posSilla[0], (float) posSilla[1], (float) posSilla[2]);
        desktop.transform.position = new Vector3((float) posDesktop[0], (float) posDesktop[1], (float) posDesktop[2]);
        /*
        Debug.Log("Despues Escritorio: " + "[" + 
                    desktop.transform.position[0] + "]-[" + 
                    desktop.transform.position[1] + "]-[" + 
                    desktop.transform.position[2]+"]"); 
        Debug.Log("Despues Silla: " + "[" + 
                    silla.transform.position[0] + "]-[" + 
                    silla.transform.position[1] + "]-[" + 
                    silla.transform.position[2]+"]"); */
        //Rotaciones
        cuadro.transform.Rotate((float) posCuadro[3], (float) posCuadro[4], (float) posCuadro[5], Space.Self);
        silla.transform.Rotate((float) posSilla[3], (float) posSilla[4], (float) posSilla[5], Space.Self);
        desktop.transform.Rotate((float) posDesktop[3], (float) posDesktop[4], (float) posDesktop[5], Space.Self);

        espacio.gameObject.SetActive(true);
    }

    /*
     * Position: x, y, z    -> [0],[1],[2]
     * Rotation: x, y, z    -> [3],[4],[5]
     * Scale:    x, y, z    -> [6],[7],[8]
     *
    */
	public List<double> Cuadro(string piso, string pared){
		List<double> pared1 = new List<double> { 0, 6, 7.5, 0, 0, 0, 1, 4, 4};             // Pared 1
        List<double> pared2 = new List<double> { 7.5, 6, 0, 0, 270, 0, 1, 4, 4};           // Pared 2

        List<double> retorno = pared2;
        if(pared.Equals("Pared 1")){
            retorno = pared1;
        }

        if(piso.Equals("Piso 2")){
            retorno[0] += -20;
            if(retorno[4] == 0){
                retorno[4] = 270;
            }else{
                retorno[4] = 0;
            }
        }else if(piso.Equals("Piso 3")){
            retorno[0] += -40;
        }
        return retorno;
	}
    /*
     * Parametro 1: Centro - Posicion del escritorio
     * Parametro 2: Centro - Posicion de la silla
    */
	public List<double> Escritorio(string piso, string esc, string si){
		List<double> SI = new List<double> { -6.6, 0, 5.2, 0, 90, 0, 0.06, 0.06, 0.06};     // Superior izquierda
        List<double> SD = new List<double> { 5.65, 0, 6.9, 0, 0, 0, 0.06, 0.06, 0.06};    // Superior derecha
        List<double> II = new List<double> { -5.6, 0, -6.9, 0, 0, 0, 0.06, 0.06, 0.06};     // Inferior izquierda
        List<double> ID = new List<double> { 6.9, 0, -5.6, 0, 270, 0, 0.06, 0.06, 0.06};    // Inferior derecha
        
		List<double> CSI = new List<double> { 1, 0, 0.5, 0, -90, 0, 0.06, 0.06, 0.06};      // Centro - Superior izquierda
        List<double> CSD = new List<double> { 0.5, 0, -1, 0, 0, 0, 0.06, 0.06, 0.06};       // Centro - Superior derecha
        List<double> CII = new List<double> { 0, 0, 1, 0, 180, 0, 0.06, 0.06, 0.06};        // Centro - Inferior izquierda
        List<double> CID = new List<double> { -1, 0, -2, 0, 90, 0, 0.06, 0.06, 0.06};       // Centro - Inferior derecha

        List<double> retorno = new List<double>();

        if(esc.Equals("superior izquierda")){           // Ya
            retorno = SI; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 0;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 270;
            }
            Debug.Log("SI - " + retorno[4]);
        }
        else if(esc.Equals("superior derecha")){        // YA
            retorno = SD; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 90;
                retorno[0] += -1;
                retorno[2] += -1;
            }else if(piso.Equals("Piso 2")){
                retorno[0] += -1;
                retorno[2] += -1;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 180;
                retorno[0] += -1;
                retorno[2] += -1;
            }
            Debug.Log("SD - " + retorno[4]);
        }
        else if(esc.Equals("inferior izquierda")){      // YA
            retorno = II; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 270;
                retorno[0] += 1;
                retorno[2] += 1;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 180;
                retorno[0] += 1;
                retorno[2] += 1;
            }else if(piso.Equals("Piso 3")){
                retorno[0] += 1;
                retorno[2] += 1;
            }
            Debug.Log("II - " + retorno[4]);
        }
        else if(esc.Equals("inferior derecha")){        // YA
            retorno = ID; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 180;
                retorno[0] += -1;
                retorno[2] += 1;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 90;
                retorno[0] += -1;
                retorno[2] += 1;
            }else if(piso.Equals("Piso 3")){
                retorno[0] += -1;
                retorno[2] += 1;
            }
            Debug.Log("ID - " + retorno[4]);
        }
        else if(si.Equals("superior izquierda")){       // YA
            retorno = CSI; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 180;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 90;
                retorno[0] += -1;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 3")){
                retorno[0] += 0;
                retorno[2] += 0;
            } 
            Debug.Log("CSI - " + retorno[4]);
        }
        else if(si.Equals("superior derecha")){         // YA
            retorno = CSD;
            if(piso.Equals("Piso 1")){
                retorno[4] = 270;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 180;
                retorno[0] += -1;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 0;
                retorno[0] += 0;
                retorno[2] += 0;
            } 
            Debug.Log("CSD - " + retorno[4]);
        }
        else if(si.Equals("inferior izquierda")){       // YA
            retorno = CII; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 90;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 0;
                retorno[0] += -1;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 3")){
                //retorno[4] = 0;
                retorno[0] += 0;
                retorno[2] += 0;
            } 
            Debug.Log("CII - " + retorno[4]);
        }
        else if(si.Equals("inferior derecha")){         // YA
            retorno = CID; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 0;
                retorno[0] += 0;
                retorno[2] += 1;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = -90;
                retorno[0] += 0;
                retorno[2] += 1;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 90;
                retorno[0] += 0;
                retorno[2] += 1;
            } 
            Debug.Log("CID - " + retorno[4]);
        }

        if(piso.Equals("Piso 2")){
            retorno[0] += -20;
        }else if(piso.Equals("Piso 3")){
            retorno[0] += -40;
        }

        return retorno;
	}

    /*
     * Parametro 1: Centro - Posicion de la silla
     * Parametro 2: Centro - Posicion del escritorio
    */
	public List<double> Silla(string piso, string si, string esc){
        List<double> SI = new List<double> { -5.3, 2.2, 5, 0, 135, 0, 0.05, 0.05, 0.05};    // Superior izquierda
        List<double> SD = new List<double> { 5, 2.2, 5.3, 0, 225, 0, 0.05, 0.05, 0.05};     // Superior derecha
		List<double> ce = new List<double> { 0, 2.2, 0, 0, 135, 0, 0.05, 0.05, 0.05};       // Centro   (1:-45, 2:50, 3:230, 4:135)
        List<double> II = new List<double> { -5.4, 2.2, -5.5, 0, 50, 0, 0.05, 0.05, 0.05};  // Inferior izquierda
        List<double> ID = new List<double> { 5, 2.2, -5.3, 0, 135, 0, 0.05, 0.05, 0.05};    // Inferior derecha

        List<double> retorno = new List<double>();

        if(si.Equals("superior izquierda")){            // YA
            retorno = SI; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 180;
                retorno[0] += 1;
                retorno[2] += -0.5;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 90;
                retorno[0] += 1;    // X
                retorno[2] += -0.5; // Y
            }else if(piso.Equals("Piso 3")){
                retorno[4] = -90;
                retorno[0] += 1;
                retorno[2] += -0.5;
            } 
            Debug.Log("S - SI ");
        }
        else if(si.Equals("superior derecha")){         // YA
            retorno = SD; 
            if(piso.Equals("Piso 1")){
                retorno[4] = -90;
                retorno[0] += -0.5;
                retorno[2] += -0.5;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 180;
                retorno[0] += -0.5;
                retorno[2] += -0.5;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 0;
                retorno[0] += -0.5;
                retorno[2] += -0.5;
            } 
            Debug.Log("S - SD ");
        }
        else if(si.Equals("inferior izquierda")){       // YA
            retorno = II;
            if(piso.Equals("Piso 1")){
                retorno[4] = 90;
                retorno[0] += 1;
                retorno[2] += 1;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 0;
                retorno[0] += 1;
                retorno[2] += 1;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 180;
                retorno[0] += 1;
                retorno[2] += 1;
            } 
            Debug.Log("S - II ");
        }
        else if(si.Equals("inferior derecha")){         // YA
            retorno = ID; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 0;
                retorno[0] += -0.3;
                retorno[2] += 0.7;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = -90;
                retorno[0] += -0.5;
                retorno[2] += 0.5;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 90;
                retorno[0] += -0.5;
                retorno[2] += 0.5;
            } 
            Debug.Log("S - ID ");
        }
        else if(esc.Equals("superior izquierda")){      // YA
            retorno = ce; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 0;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = -90;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 90;
                retorno[0] += 0;
                retorno[2] += 0;
            } 
            Debug.Log("S - CSI ");
        }
        else if(esc.Equals("superior derecha")){        // YA
            retorno = ce; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 90;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 0;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 180;
                retorno[0] += 0;
                retorno[2] += 0;
            } 
            Debug.Log("S - CSD ");
        }
        else if(esc.Equals("inferior izquierda")){      // YA
            retorno = ce; 
            if(piso.Equals("Piso 1")){
                retorno[4] = -90;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 180;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = 0;
                retorno[0] += 0;
                retorno[2] += 0;
            } 
            Debug.Log("S - CII ");
        }
        else if(esc.Equals("inferior derecha")){        // YA
            retorno = ce; 
            if(piso.Equals("Piso 1")){
                retorno[4] = 180;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 2")){
                retorno[4] = 90;
                retorno[0] += 0;
                retorno[2] += 0;
            }else if(piso.Equals("Piso 3")){
                retorno[4] = -90;
                retorno[0] += 0;
                retorno[2] += 0;
            } 
            Debug.Log("S - CID ");
        }

        if(piso.Equals("Piso 2")){
            retorno[0] += -20;
        }else if(piso.Equals("Piso 3")){
            retorno[0] += -40;
        }
        //Debug.Log("Nuevo valor: " + retorno[0]);

        return retorno;
	}

    public void GoScene(){
        SceneManager.LoadScene("MenuPrincipal");
    }
}
