using UnityEngine;
using UnityEngine.Localization.Components;

public class ChangePanelContol : MonoBehaviour
{
    [SerializeField] private GameObject panelMandos;
    [SerializeField] private GameObject panelTeclado;

    private bool isKeyboard;
    void Start()
    {
        Debug.Log(PlayerInputController.Instance.IsUsingKeyboard());
        isKeyboard = PlayerInputController.Instance.IsUsingKeyboard();
        if (isKeyboard)
        {
            panelMandos.SetActive(false);
            panelTeclado.SetActive(true);
        }
        else
        {
            panelMandos.SetActive(true);
            panelTeclado.SetActive(false);
        }
    }
}
