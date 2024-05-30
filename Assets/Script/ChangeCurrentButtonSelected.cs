using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCurrentButtonSelected : MonoBehaviour
{
    [SerializeField] private GameObject buttonToSelect;
    public void SelectButton()
    {
        EventSystem.current.SetSelectedGameObject( buttonToSelect);
    }
}
