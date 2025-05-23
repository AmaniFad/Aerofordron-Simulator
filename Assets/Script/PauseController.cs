using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//El input para pausar se pone en el SerializeField de este script y no en el inputController
public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPausing;
    [SerializeField] private bool pauseWithoutPauseMenu;
    [SerializeField] private GameObject pauseMenuInstance;
    // Start is called before the first frame update
    void Start()
    {
        pauseWithoutPauseMenu = false;
        isPausing = false;
    }


    private void Update()
    {
        //Si el menu de pausa no es nulo mira si esta activo, si esta activo pausa el juego y sino despausalo, esto podrias generar problemas a futuro pero es una forma de
        //asegurarse que el juego este pausado cuando toca
        if (pauseWithoutPauseMenu)
        {
            if (pauseMenuInstance)
            {
                if (!pauseMenuInstance.activeInHierarchy)
                {
                    Time.timeScale = 1f;
                }
                else
                {
                    Time.timeScale = 0f;
                }
            }
            else
            {
                Time.timeScale = 1f;
            }
            
        }


        //el bool canPause es para que no si le llegan dos inputs en un lapso de tiempo muy corto no se abre y se cierre el menu de pausa
        if (DisplayInputData.isMenuPressed)
        {

            TryPause();

        }
    }


    public void PauseWihoutPauseMenu()
    {
        pauseWithoutPauseMenu = true;
        Time.timeScale = 0;

    }

    public void UnPauseWithoutMenu()
    {
        pauseWithoutPauseMenu = false;
        Time.timeScale = 1;
    }

    public void TryPause()
    {
        //Mira si existe una instancia del menu de pausa si no exista la instancia y si existe simplemente la activa o desactiva dependiendom, este metodo es un toggle
        if (!pauseMenuInstance)
        {
            pauseMenuInstance = Instantiate(pauseMenu);
            pauseMenuInstance.GetComponent<ChangeCurrentButtonSelected>().SelectButton();
            isPausing = true;
            pauseMenuInstance.SetActive(true);
            pauseMenuInstance.GetComponent<ChangeCurrentButtonSelected>().SelectButton();
            Time.timeScale = 0f;
            PlayerInputController.Instance.HasPaused();
            Cursor.visible = true;

        }
        else
        {
            if (!isPausing)
            {
                isPausing = true;
                pauseMenuInstance.SetActive(true);
                pauseMenuInstance.GetComponent<ChangeCurrentButtonSelected>().SelectButton();
                Time.timeScale = 0f;
                PlayerInputController.Instance.HasPaused();
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
                Time.timeScale = 1f;
                isPausing = false;
                pauseMenuInstance.SetActive(false);
                PlayerInputController.Instance.HasPaused();
            }
        }
        PlayerInputController.Instance.HasPaused();

    }

    public void StopPause()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPausing = false;
        PlayerInputController.Instance.HasPaused();
    }
}
