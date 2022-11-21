using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public DynamicJoystick dynamicJoystick;
    public float speed;

    #region SerializeFields
    [SerializeField] private Animator anim;
    #endregion
   
    private void Update()
    {
       // if (!GameManager.instance.isGameRunning)
        //    return;
        JoyStickMovement();
    }
    public void JoyStickMovement()
    {
        
            Vector3 moveForward = new Vector3(dynamicJoystick.Horizontal, 0, dynamicJoystick.Vertical);
            transform.Translate(speed * Time.fixedDeltaTime * moveForward, Space.World);
            moveForward.Normalize();
            if (dynamicJoystick.Horizontal != 0 || dynamicJoystick.Vertical != 0)
            {
                transform.forward = moveForward;
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
        
    }
}