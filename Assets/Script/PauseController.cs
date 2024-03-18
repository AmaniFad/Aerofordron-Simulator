using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//El input para pausar se pone en el SerializeField de este script y no en el inputController
public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPausing;
    private GameObject pauseMenuInstance;
    // Start is called before the first frame update
    void Start()
    {
        isPausing = false;
    }


    private void Update()
    {
        if (PlayerInputController.Instance.IsPausing())
        {
            Debug.Log("Entra");
            TryPause();
        }
    }

    public void TryPause()
    {
        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = Instantiate(pauseMenu);
        }
        else
        {
            if (!isPausing)
            {
                isPausing = true;
                pauseMenuInstance.SetActive(true);
                Time.timeScale = 0f;
                PlayerInputController.Instance.HasPaused();

            }
            else
            {
                Time.timeScale = 1f;
                isPausing = false;
                pauseMenuInstance.SetActive(false);
                PlayerInputController.Instance.HasPaused();
            }
        }

    }
}
