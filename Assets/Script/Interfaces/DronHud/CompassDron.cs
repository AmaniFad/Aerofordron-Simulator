using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompassDron : MonoBehaviour
{
    [SerializeField] private Transform player;          
    [SerializeField] private Transform dron;
    [SerializeField] private RawImage compassImage;
    [SerializeField] private RectTransform playerMarker;
    private float compassWidth;

    [SerializeField] private Sprite GreenBanner;
    [SerializeField] private Sprite YellowBanner;
    [SerializeField] private GameObject BannerObj;
    [SerializeField] private TMP_Text BannerText;
    
    private void Start()
    {
        player = PlayerReferences.instance.GetPlayer().transform;
        dron = PlayerReferences.instance.GetDron().transform;

        if(compassImage != null)
            compassWidth = compassImage.rectTransform.rect.width;
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
        UpdatePlayerMarker();
    }
    private void UpdatePlayerMarker()
    {
        if (playerMarker != null)
        {
            // Vector desde el dron hacia el player (plano XZ)
            Vector3 toPlayer = player.position - dron.position;
            toPlayer.y = 0f;

            if (toPlayer.sqrMagnitude > 0.0001f)
            {
                // Ángulo absoluto del vector (0° = norte/+Z)
                float bearingWorld = Mathf.Atan2(toPlayer.x, toPlayer.z) * Mathf.Rad2Deg;

                // Ángulo relativo al frente del dron (-180..180)
                float relativeDeg = Mathf.DeltaAngle(dron.eulerAngles.y, bearingWorld);

                // Mapea -180..180 a -ancho/2..+ancho/2 (centro = frente del dron)
                float halfWidth = compassImage.rectTransform.rect.width * 0.5f;
                float x = (relativeDeg / 180f) * halfWidth;

                // Si tu compás “va al revés”, invierte con x = -x;
                playerMarker.anchoredPosition = new Vector2(x, playerMarker.anchoredPosition.y);
                if (!playerMarker.gameObject.activeSelf) playerMarker.gameObject.SetActive(true);
            }
            else
            {
                // Opcional: ocultar si está exactamente encima
                playerMarker.gameObject.SetActive(false);
            }
        }
    }
}
