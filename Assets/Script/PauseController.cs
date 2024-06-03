using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//El input para pausar se pone en el SerializeField de este script y no en el inputController
public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPausing;
    [SerializeField] private GameObject pauseMenuInstance;
    private bool canPause;
    // Start is called before the first frame update
    void Start()
    {
        isPausing = false;
        canPause = true;
    }


    private void Update()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        if (PlayerInputController.Instance.IsPausing())
        {
            Debug.Log(canPause);
            if (canPause) {
                TryPause();

            }


        }
    }

    private IEnumerator _EnablePauseAgain(float time)
    {
        canPause = false;
        yield return new WaitForSeconds(time);
        canPause = true;
    }
    public void TryPause()
    {
        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = Instantiate(pauseMenu);
            pauseMenuInstance.GetComponent<ChangeCurrentButtonSelected>().SelectButton();

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
        StartCoroutine(_EnablePauseAgain(0.2f));

    }

    public void StopPause()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPausing = false;
        PlayerInputController.Instance.HasPaused();
    }
}
