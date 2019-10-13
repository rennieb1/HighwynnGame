
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChangeCameraTargets : MonoBehaviour
{
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    public Camera m_OrthographicCamera;
    public float MaxCamerasSize;
    public float sizeModifer;
    float m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight;


    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }


    // Update is called once per frame
    private void Update()
    {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;
        if (m_OrthographicCamera.orthographicSize <= 18.0f)
        {
            m_OrthographicCamera.orthographicSize += sizeModifer;
        }
        else
        {

        }

       

        m_LastTargetPosition = target.position;
    }

}


/*
 public float moveSpeed = 6.0f;
    public float attackRange = 300.0f;
    public Transform moveLocation;

    // Use this for initialization
    void Start()
    {
        moveLocation = GameObject.FindGameObjectWithTag("SceneCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(moveLocation.position, transform.position) < attackRange)//range
        {
            Vector2 direction = moveLocation.position - transform.position;//How it knows to attack

            direction.y = 0;
            //rotate speed 0.1f had a nice curl
 //           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0f);//rotate speed 0.1f had a nice curl
                transform.Translate(0, 0, 0.0f);
                transform.position += transform.forward * moveSpeed * Time.deltaTime; //Movespeed to attack player
            


        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }
    
}








    /*  public Transform target;
    public float speed = 1f;

    int randomTarget;
    Quaternion newRot;
    Vector2 relPos;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GetNewTarget();
        }
        else
        {
            relPos = target.position - transform.position;
          //  newRot = Quaternion.LookRotation(relPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.time * speed);
        }
    }

    void GetNewTarget()
    {
        GameObject[] possibleTargets;
        possibleTargets = GameObject.FindGameObjectsWithTag("ball");
        if (possibleTargets.Length > 0)
        {
            randomTarget = Random.Range(0, possibleTargets.Length);
            target = possibleTargets[randomTarget].transform;
        }
    } */
