using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompassDron : MonoBehaviour
{
    [SerializeField] private Transform player;          // Cámara o jugador
    [SerializeField] private Transform dron;          // Cámara o jugador
    [SerializeField] private RawImage compassImage;     // RawImage con la textura de brújula

    [SerializeField] private Sprite GreenBanner;
    [SerializeField] private Sprite YellowBanner;
    [SerializeField] private GameObject BannerObj;
    [SerializeField] private TMP_Text BannerText;
    
    private void Start()
    {
        player = PlayerReferences.instance.GetPlayer().transform;
        dron = PlayerReferences.instance.GetDron().transform;  
    }
    void Update()
    {
        if (player == null || compassImage == null) return;

        // Rotación del jugador en Y normalizada
        float yaw = player.eulerAngles.y - dron.eulerAngles.y;
        float normalizedYaw = yaw / 360f;

        // Desplaza la textura en X (horizontal) sin modificar el tamaño
        compassImage.uvRect = new Rect(normalizedYaw, 0, compassImage.uvRect.width, 1);

        float _attiMode = DronInputController.Instance.GetModeAtti();

        if (_attiMode == 1)
        {
            BannerObj.GetComponent<Image>().sprite = YellowBanner;
            BannerText.text = "ATTI MODE";
        }
        else  
        {
            BannerObj.GetComponent<Image>().sprite = GreenBanner;
            BannerText.text = "GPS MODE";
        }
    }
}
