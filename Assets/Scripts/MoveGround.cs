using UnityEngine;
public class MoveGround : MonoBehaviour
{
    [SerializeField] private float speed;//speed of platform
    private float zLimit=30f;
    void FixedUpdate()//fixedupdate gives better result while teleporting ground 
    {
        if (GameManager.instance.currentState!=States.TapToStart)//while playing ground will move
        {
            if (transform.position.z >= zLimit)//teleport ground back to player when reach limit
            {
                transform.position = Vector3.back * 10;
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);//move platform with speed
        }
    }
}