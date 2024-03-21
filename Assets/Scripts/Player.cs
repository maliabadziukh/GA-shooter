
using UnityEngine;


public class Player : Character
{
    private Vector2 moveInput;
    private Vector3 target;
    private Camera mainCamera;

    protected override void Awake()
    {
        base.Awake();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    protected override void Update()
    {
        base.Update();
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        MoveInDirection(moveInput);
        print("player moved");
        RotateToTarget(GetMousePos());
    }



    Vector3 GetMousePos()
    {
        target = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return target;
    }
}
