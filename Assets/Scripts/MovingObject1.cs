using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject1 : MonoBehaviour
{
    public string currentMapName;
    public static MovingObject1 instance;

    public float speed;
    public int walkCount;
    private int currentWalkCount;

    private Vector3 vector;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;
    private Animator animator;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;
    private bool canMove = true;

    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }       
    }
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            RaycastHit2D hit;
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            while (currentWalkCount < walkCount)
            {

                transform.Translate(vector.x * (speed + applyRunSpeed), vector.y * (speed + applyRunSpeed), 0);
                if (applyRunFlag)
                    currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);

                //if(currentWalkCount % 9 == 2)
                //{
                //    int temp = Random.Range(1, 2);
                //    switch(temp)
                //    {
                //        case 1:
                //            break;
                //        case 2:
                //            break;
                //    }
                //}
            }
            currentWalkCount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;

    }
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}