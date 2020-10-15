using UnityEngine;

public class PlayerControls
{
    public KeyCode moveUpKey, moveDownKey, moveLeftKey, moveRightKey, activateElemental;

    public PlayerControls()
    {
        moveUpKey = KeyCode.W;
        moveDownKey = KeyCode.S;
        moveLeftKey = KeyCode.A;
        moveRightKey = KeyCode.D;
        activateElemental = KeyCode.LeftShift;
    }
}
