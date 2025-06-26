using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(true);
        background.anchoredPosition = new Vector2(150, 150); // fixed position on screen
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // Don't move joystick background on pointer down
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // Joystick stays visible on pointer up
        base.OnPointerUp(eventData);
    }
}

