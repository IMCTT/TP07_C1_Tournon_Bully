using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Error: La clase MainScreen está en un archivo que se llama distinto ("Main Screen.cs"). Unity lo compila pero tira un warning: renombrá el archivo o la clase para que coincidan.
// Si renombrás el archivo a MainScreen.cs, hacelo desde el editor para conservar el .meta.
public class MainScreen : MonoBehaviour
{
    [SerializeField] Button buttonStartGame;
    [SerializeField] Button buttonEndGame;

    private void Awake()
    {
        buttonStartGame.onClick.AddListener(OnButtonStartGameClicked);
        buttonEndGame.onClick.AddListener(OnButtonEndGameClicked);
    }
    // Suggestion: Update está vacío. Borralo.
    void Update()
    {
        


    }

    

    private void OnButtonStartGameClicked()
    {
        // Suggestion: el nombre de la escena es un string mágico. Guardalo en una const o en un campo serializado.
        SceneManager.LoadScene("Game");

    }
    private void OnButtonEndGameClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    private void OnDestroy()
    {
        buttonStartGame.onClick.RemoveAllListeners();
        buttonEndGame.onClick.RemoveAllListeners();

    }
}
