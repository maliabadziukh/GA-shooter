
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
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (currentHealth < 0)
        {
            print("Game over");
            Destroy(gameObject);

        }
    }

    void FixedUpdate()
    {
        moveInput.Normalize();
        MoveInDirection(moveInput);
        RotateToTarget(GetMousePos());
    }

    Vector3 GetMousePos()
    {
        target = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return target;
    }




}
