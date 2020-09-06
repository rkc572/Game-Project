using UnityEngine;

public class PlayerControls
{
    public KeyCode moveUpKey, moveDownKey, moveLeftKey, moveRightKey, attackKey;

    public PlayerControls()
    {
        moveUpKey = KeyCode.W;
        moveDownKey = KeyCode.S;
        moveLeftKey = KeyCode.A;
        moveRightKey = KeyCode.D;
        attackKey = KeyCode.M;
    }
}
