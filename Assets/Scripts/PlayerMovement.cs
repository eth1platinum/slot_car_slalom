using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerPosition
{
    POSITION_LEFT,
    POSITION_CENTRE,
    POSITION_RIGHT
}

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 6.0F;

    PlayerPosition position = PlayerPosition.POSITION_CENTRE;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && Input.anyKeyDown)
        { // if A or left is pressed then move left
            if (position > PlayerPosition.POSITION_LEFT)
            { // if exceeded left limit then don't move
                transform.Translate(Vector3.left * 5.5F);
                position--;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && Input.anyKeyDown)
        { // if D or right is pressed then move right
            if (position < PlayerPosition.POSITION_RIGHT)
            { // if exceeded right limit then don't move
                transform.Translate(Vector3.right * 5.5F);
                position++;
            }
        }
    }
}
