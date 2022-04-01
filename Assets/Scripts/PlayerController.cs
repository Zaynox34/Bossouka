using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControl playerControl;
    [SerializeField] private Transform centerPivotTransform;

    [SerializeField] private float speed;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 40;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = playerControl.BaseControl.Move.ReadValue<Vector2>();
        if (move != Vector2.zero)
        {
            centerPivotTransform.Rotate(new Vector3(0, -move.x, 0) * Time.deltaTime * speed);
            transform.position += new Vector3(0, 0, move.y) * Time.deltaTime * speed;


            //transform.position += new Vector3(move.x,0, move.y) *Time.deltaTime*speed;
        }
    }
}
