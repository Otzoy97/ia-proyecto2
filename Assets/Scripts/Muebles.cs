using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Muebles : MonoBehaviour
{
    public GameObject[] pisos = new GameObject[6];// piso1, piso2, piso3, piso4, piso5, piso6; 
    /// (X, Y, ANGLE)
    private float[][][] vector =
    {
        new float[][]{new float[]{-4f,4f,135f},new float[]{4f,4f,-135f},new float[]{-4f,-4f,45f},new float[]{4f,-4f,-45f},new float[]{0f,0f,0f}},
        new float[][]{new float[]{-5f,5.5f,0f},new float[]{5f,5.5f,0f},new float[]{-5f,-5.5f,0f},new float[]{5f,-5.5f,0f},new float[]{0f,0f,0f}},
        new float[][]{new float[]{-5.5f,5.5f,135f},new float[]{5.5f,5.5f,-135f},new float[]{-5.5f,-5.5f,45f},new float[]{5.5f,-5.5f,-45f},new float[]{0f,0f,0f}},
        new float[][]{new float[]{-6.4f,3.7f,0f},new float[]{6.4f,3.7f,0f},new float[]{-6.4f,-5.4f,0f},new float[]{6.4f,-5.4f,0f},new float[]{0f,0f,0f}},
        new float[][]{new float[]{-6f,6f,0f},new float[]{6f,6f,0f},new float[]{-6f,-6f,0f},new float[]{6f,-6f,0f},new float[]{0f,0f,0f}}
    };

    /*
     * Position: x, y, z    -> [0],[1],[2]
     * Rotation: x, y, z    -> [3],[4],[5]
     * Scale:    x, y, z    -> [6],[7],[8]
     */
    void Awake()
    {
        foreach (var esc in Principal.Espacios)
        {
            string[] espacio = (string[])esc.Value;
            switch (espacio[0])
            {
                case "Piso 1":
                    establecerParametros(pisos[0], espacio, "Piso 1", 0f);
                    break;
                case "Piso 2":
                    establecerParametros(pisos[1], espacio, "Piso 2", -20f);
                    break;
                case "Piso 3":
                    establecerParametros(pisos[2], espacio, "Piso 3", -40f);
                    break;
                case "Piso 4":
                    establecerParametros(pisos[3], espacio, "Piso 4", -60f);
                    break;
                case "Piso 5":
                    establecerParametros(pisos[4], espacio, "Piso 5", -80f);
                    break;
                default:
                    establecerParametros(pisos[5], espacio, "Piso 6", -100f);
                    break;
            }
        }
    }

    private void establecerParametros(GameObject espacio, string[] escenario, string pActual, float offset)
    {
        // piso, sofa, escritorio, silla, mesa, librera
        // Debug.Log("============= " + pActual + " =============");
        Transform sofa = espacio.transform.GetChild(0);
        Transform mesa = espacio.transform.GetChild(1);
        Transform silla = espacio.transform.GetChild(2);
        Transform librera = espacio.transform.GetChild(3);
        Transform cafe = espacio.transform.GetChild(4);
        //Posiciones
        posicionar(sofa, escenario[1], 0, offset);
        posicionar(mesa, escenario[2], 1, offset);
        posicionar(silla, escenario[3], 2, offset);
        posicionar(librera, escenario[5], 3, offset);
        posicionar(cafe, escenario[4], 4, offset);

        espacio.gameObject.SetActive(true);
    }

    /// <summary>
    /// Coloca el mueble en la posición especificada
    /// </summary>
    ///
    private void posicionar(Transform mueble, string posicion, int idxMueble, float offset)
    {
        switch (posicion.ToLower())
        {
            case "superior izquierda":
                mueble.transform.position = new Vector3(offset + vector[idxMueble][0][0], 0, vector[idxMueble][0][1]);
                mueble.transform.Rotate(0, vector[idxMueble][0][2], 0, Space.Self);
                break;
            case "superior derecha":
                mueble.transform.position = new Vector3(offset + vector[idxMueble][1][0], 0, vector[idxMueble][1][1]);
                mueble.transform.Rotate(0, vector[idxMueble][1][2], 0, Space.Self);
                break;
            case "inferior izquierda":
                mueble.transform.position = new Vector3(offset + vector[idxMueble][2][0], 0, vector[idxMueble][2][1]);
                mueble.transform.Rotate(0, vector[idxMueble][2][2], 0, Space.Self);
                break;
            case "inferior derecha":
                mueble.transform.position = new Vector3(offset + vector[idxMueble][3][0], 0, vector[idxMueble][3][1]);
                mueble.transform.Rotate(0, vector[idxMueble][3][2], 0, Space.Self);
                break;
            case "centro":
                mueble.transform.position = new Vector3(offset + vector[idxMueble][4][0], 0, vector[idxMueble][4][1]);
                mueble.transform.Rotate(0, vector[idxMueble][4][2], 0, Space.Self);
                break;
        }
    }

    public void GoScene()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
