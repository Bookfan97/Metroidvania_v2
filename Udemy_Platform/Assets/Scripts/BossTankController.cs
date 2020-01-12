using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossState {shooting, hurt, moving };
    public bossState currentState;
    public Transform theBoss;
    public Animator anim;
    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    [Header("Shooting")]
    public GameObject bullet;
    public float timeBetweenShots;
    private float shotCounter;
    public Transform firePoint;
    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    // Start is called before the first frame update
    void Start()
    {
        currentState = bossState.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case bossState.shooting:
                break;
            case bossState.hurt:
                if(hurtCounter>0)
                {
                    hurtCounter -= Time.deltaTime;
                    if(hurtCounter<= 0)
                    {
                        currentState = bossState.moving;
                    }
                }
                break;
            case bossState.moving:
                if(moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed*Time.deltaTime, 0f, 0f);
                    if(theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one;
                        moveRight = false;
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x < rightPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                        EndMovement();
                    }
                }
                break;
        }
    }

    private void EndMovement()
    {
        currentState = bossState.shooting;
        shotCounter = timeBetweenShots;
        anim.SetTrigger("stopMoving");
    }

    public void TakeHit()
    {
        currentState = bossState.hurt;
        hurtCounter = hurtTime;
        anim.SetTrigger("hit");
    }

}
