using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum MoveState
    {
        Wait,
        FollowLine,
        MoveForward
    }
    private MoveState moveState = MoveState.Wait;
    private List<Vector3> targets = new List<Vector3>(); // line points
    public int targetIndex = 0;

    [SerializeField] float speed = 10;
    public void AddCoordinate(Vector3 coordinate)
    {
        targets.Add(coordinate);
    }
    public void Patrol()
    {
        var step = speed * Time.deltaTime; // calculate distance to move

        transform.position = Vector3.MoveTowards(transform.position, targets[targetIndex], step);
        if (transform.position == targets[targetIndex])
        {
            targetIndex++; // new target index
            if (targetIndex == targets.Count)
            {
                moveState = MoveState.MoveForward;
            }
            //targetIndex = targetIndex % targets.Count;  // round robin
        }
    }
    public void FollowLine()
    {
        moveState = MoveState.FollowLine;
    }
    private void Start()
    {
        Invoke("DestroySelf",7.5f);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        if (moveState == MoveState.FollowLine)
        {
            Patrol();
        }
        if (moveState == MoveState.MoveForward)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            GameManager.instance.point++;
            Destroy(gameObject);
        }
    }
}
