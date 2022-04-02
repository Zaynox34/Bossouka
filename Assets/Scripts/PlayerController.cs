using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControl playerControl;
    [SerializeField] private Transform centerPivotTransform;
    [SerializeField] private GameObject projectileGroup;
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
        transform.forward = transform.position.normalized;
        Move();
        //Targeter();
    }
    public void Move()
    {
        Vector2 move = playerControl.BaseControl.Move.ReadValue<Vector2>();
        if (move != Vector2.zero)
        {
            centerPivotTransform.Rotate(new Vector3(0, -move.x, 0) * Time.deltaTime * speed);
            transform.position += transform.forward*-move.y * Time.deltaTime * speed / Mathf.PI;
        }
    }
    /*public void Targeter()
    {
        int targeterIndex = 0;
        for(int i=0;i<projectileGroup.transform.childCount;i++)
        {
            if((projectileGroup.transform.GetChild(i).position-transform.position).magnitude > (projectileGroup.transform.GetChild(casContactIndex).position - transform.position).magnitude)
            {
                argeterIndex = i;
            }
        }
        projectileGroup.transform.GetChild(targeterIndex).GetComponent<ProjectileManager>.isTarget = true;
    }*/
}
