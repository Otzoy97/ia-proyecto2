using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Muebles : MonoBehaviour
{
    public GameObject piso1, piso2, piso3, piso4, piso5, piso6;
    /*
     * Position: x, y, z    -> [0],[1],[2]
     * Rotation: x, y, z    -> [3],[4],[5]
     * Scale:    x, y, z    -> [6],[7],[8]
     *
     */
    void Awake()
    {
        piso1.gameObject.SetActive(false);
        piso2.gameObject.SetActive(false);
        piso3.gameObject.SetActive(false);
        piso4.gameObject.SetActive(false);
        piso5.gameObject.SetActive(false);
        piso6.gameObject.SetActive(false);

        foreach (var esc in Principal.Espacios)
        {
            string[] espacio = (string[])esc.Value;
            switch (espacio[0])
            {
                case "Piso 1":
                    establecerParametros(piso1, espacio, "Piso 1");
                    break;
                case "Piso 2":
                    establecerParametros(piso2, espacio, "Piso 2");
                    break;
                case "Piso 3":
                    establecerParametros(piso3, espacio, "Piso 3");
                    break;
                case "Piso 4":
                    establecerParametros(piso4, espacio, "Piso 4");
                    break;
                case "Piso 5":
                    establecerParametros(piso5, espacio, "Piso 5");
                    break;
                default:
                    establecerParametros(piso6, espacio, "Piso 6");
                    break;
            }
        }
    }

    private void establecerParametros(GameObject espacio, string[] escenario, string pActual)
    {
        // piso, sofa, escritorio, silla, mesa, librera
        // Debug.Log("============= " + pActual + " =============");
        Transform sofa = espacio.transform.GetChild(0);
        Transform mesa = espacio.transform.GetChild(1);
        Transform silla = espacio.transform.GetChild(2);
        Transform librera = espacio.transform.GetChild(3);
        Transform cafe = espacio.transform.GetChild(4);
        List<double> posSilla = Silla(pActual, escenario[3].ToLower(), escenario[2].ToLower());
        //Posiciones
        posicionar(sofa, escenario[1], "sofa");
        posicionar(mesa, escenario[2], "mesa");
        posicionar(silla, escenario[3], "silla");
        posicionar(cafe, escenario[4], "cafe");
        posicionar(librera, escenario[5], "librera");

        //sofa.transform.position = new Vector3((float)posSilla[0], (float)posSilla[1], (float)posSilla[2]);
        //cuadro.transform.Rotate((float) posCuadro[3], (float) posCuadro[4], (float) posCuadro[5], Space.Self);
        //silla.transform.Rotate((float) posSilla[3], (float) posSilla[4], (float) posSilla[5], Space.Self);
        //desktop.transform.Rotate((float) posDesktop[3], (float) posDesktop[4], (float) posDesktop[5], Space.Self);

        espacio.gameObject.SetActive(true);
    }

    /// <summary>
    /// Coloca el mueble en la posición especificada
    /// </summary>
    ///
    private void posicionar(Transform mueble, string posicion, string nombre)
    {
        switch (posicion.ToLower())
        {
            case "superior izquierda":
                mueble.transform.position = new Vector3(-4, 0, 4);
                break;
            case "superior derecha":
                mueble.transform.position = new Vector3(4, 0, 4);
                break;
            case "inferior izquierda":
                mueble.transform.position = new Vector3(-4, 0, -4);
                break;
            case "inferior derecha":
                mueble.transform.position = new Vector3(4, 0, -4);
                break;
            case "centro":
                mueble.transform.position = new Vector3(0, 0, 0);
                break;
        }
    }

    // public void rotar(Transform mueble, string nombre)
    // {
    //     switch (posicion.ToLower())
    //     {
    //         case "sofa":
    //             mueble.transform.Rotate
    //             break;
    //         case "mesa":
    //             break;
    //         case "silla":
    //             break;
    //         case "librera":
    //             break;
    //         case "cafe":
    //             break;
    //     }
    // }
    public void GoScene()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
