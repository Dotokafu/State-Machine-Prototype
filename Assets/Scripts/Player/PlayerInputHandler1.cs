using UnityEngine;

public class PlayerInputHandler1 : MonoBehaviour
{
   private InputSystem_Actions actions;

    public Vector2 RawMoveInput {  get; private set; } 
    public int NormInputX { get; private set; }

    public int NormInputY { get; private set; }

    private void Awake()
    {
        actions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        actions.Enable();

    }
    private void OnDisable()
    {
        actions.Disable();
    }
    private void Update()
    {
        RawMoveInput = actions.Player.Move.ReadValue<Vector2>();
        NormInputX = (int)(RawMoveInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMoveInput * Vector2.up).normalized.y;
    }
}
