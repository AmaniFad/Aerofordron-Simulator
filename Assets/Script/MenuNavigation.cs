using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;
    private int currentMenu;
    private int[] previousMenus;
    [SerializeField] private InputAction goBackAction;
    void Start()
    {
        currentMenu = 0;
        previousMenus = new int[menus.Length];
        ResetPreviousMenus();
        goBackAction.performed += GoBack;
    }

    private void OnEnable()
    {
        goBackAction.Enable();
    }

    private void OnDisable()
    {
        goBackAction.Disable();
    }
    public void ChangeMenu(int menu)
    {
        menus[currentMenu].SetActive(false);
        menus[menu].SetActive(true);
        previousMenus[SearchLastMenu()] = currentMenu;
        currentMenu = menu;

    }

    public int SearchLastMenu()
    {
        int aux = 0;
        for (int i = 0; i < previousMenus.Length; i++)
        {
            if (previousMenus[i] != -1)
            {
                aux = i + 1;
            }

        }
        return aux;
    }

    private void ResetPreviousMenus()
    {
        for (int i = 0; i < previousMenus.Length; i++)
        {
            previousMenus[i] = -1;
        }
    }


    public void GoBack(InputAction.CallbackContext action)
    {
        if (SearchLastMenu() > 0)
        {
            Debug.Log(SearchLastMenu());
            menus[currentMenu].SetActive(false);
            menus[SearchLastMenu() - 1].SetActive(true);
            currentMenu = SearchLastMenu() - 1;
            previousMenus[SearchLastMenu() -1 ] = -1;
        }

    }

}
