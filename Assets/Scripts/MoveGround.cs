using UnityEngine;
public class MoveGround : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z<-20f)
        {
            transform.position = Vector3.forward * 40;
        }
    }
}
