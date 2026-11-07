
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuArrowEffect : MonoBehaviour
{
    [Header("Arrow References")]
    [SerializeField] private RectTransform leftArrow;
    [SerializeField] private RectTransform rightArrow;

    [Header("Animation")]
    [Range(0.5f, 1f)]
    [SerializeField] private float pressedScale = 0.85f;
    [SerializeField] private float animationSpeed = 18f;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField]
    private Color pressedColor =
        new Color(0.65f, 0.65f, 0.65f);

    private Vector3 leftOriginalScale;
    private Vector3 rightOriginalScale;

    private Image leftImage;
    private Image rightImage;

    private void Awake()
    {
        if (leftArrow == null || rightArrow == null)
        {
            Debug.LogError(
                "Assign both arrow RectTransforms in the Inspector.",
                this
            );
            enabled = false;
            return;
        }

        leftOriginalScale = leftArrow.localScale;
        rightOriginalScale = rightArrow.localScale;

        leftImage = leftArrow.GetComponent<Image>();
        rightImage = rightArrow.GetComponent<Image>();
    }

    private void Update()
    {
        bool leftPressed = false;
        bool rightPressed = false;

        // Keyboard
        if (Keyboard.current != null)
        {
            leftPressed =
                Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed;

            rightPressed =
                Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed;
        }

        // All connected gamepads
        foreach (Gamepad pad in Gamepad.all)
        {
            leftPressed |=
                pad.dpad.left.isPressed ||
                pad.leftStick.left.isPressed ||
                pad.buttonWest.isPressed;

            rightPressed |=
                pad.dpad.right.isPressed ||
                pad.leftStick.right.isPressed ||
                pad.buttonEast.isPressed;
        }

        AnimateArrow(
            leftArrow, leftImage, leftOriginalScale, leftPressed
        );

        AnimateArrow(
            rightArrow, rightImage, rightOriginalScale, rightPressed
        );
    }

    private void AnimateArrow(
        RectTransform arrow,
        Image image,
        Vector3 originalScale,
        bool pressed)
    {
        float targetScale = pressed ? pressedScale : 1f;

        arrow.localScale = Vector3.Lerp(
            arrow.localScale,
            originalScale * targetScale,
            Time.unscaledDeltaTime * animationSpeed
        );

        if (image != null)
        {
            Color targetColor = pressed ? pressedColor : normalColor;

            image.color = Color.Lerp(
                image.color,
                targetColor,
                Time.unscaledDeltaTime * animationSpeed
            );
        }
    }
}
