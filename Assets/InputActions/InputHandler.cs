using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private GameInput input;

    void Awake()
    {
        input = new GameInput();
    }

    void OnEnable()
    {
        input.Enable();
        input.Gameplay.Click.performed += OnClick;
    }

    void OnDisable()
    {
        input.Gameplay.Click.performed -= OnClick;
        input.Disable();
    }

    void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Click");
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit != null && hit.TryGetComponent<IClickable>(out var clickable))
        {
            clickable.OnClicked();
        }
    }
}
