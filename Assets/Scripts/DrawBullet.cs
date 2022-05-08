using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBullet : MonoBehaviour
{
    public LineRenderer Line;
    public float lineWidth = 0.04f;
    public float minimumVertexDistance = 0.1f;

    private bool isLineStarted;
    public GameObject bulletPrefab;

    private List<Bullet> bullets=new List<Bullet>();
    private RaycastHit hit;
    public TextMesh ammoText;

    void Start()
    {
        // set the color of the line
        Line.startColor = Color.red;
        Line.endColor = Color.red;

        // set width of the renderer
        Line.startWidth = lineWidth;
        Line.endWidth = lineWidth;

        isLineStarted = false;
        Line.positionCount = 0;
    }

    void Update()
    {
        ammoText.text = GameManager.instance.currentAmmo + "/5";
        if (GameManager.instance.currentState==States.Playing)
        {
            if (GameManager.instance.MouseDown())
            {
                StopCoroutine("MoveBullets");
                bullets.Clear();
                CreateBullet();
                CreateLineOnClick();
            }
            if (GameManager.instance.MouseButtonClicking() && isLineStarted)
            {
                UpdateLineWithDistance();
            }
            if (GameManager.instance.MouseUp())
            {
                AddCoordinatesToBullet();
                StartCoroutine("MoveBullets");
                RemoveLine();
            }
        }
    }
    IEnumerator MoveBullets()
    {
        foreach (var bullet in bullets)
        {
            bullet.FollowLine();
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void CreateBullet()
    {
        int j = 0;
        for (int i = GameManager.instance.currentAmmo; i >0; i--)
        {
            bullets.Add(Instantiate(bulletPrefab).GetComponent<Bullet>());
            bullets[j].AddCoordinate(GetWorldCoordinate());
            GameManager.instance.currentAmmo--;
            j++;
        }
    }
    private void AddCoordinatesToBullet()//add coordinates while keep clicking
    {
        for (int i = 0; i < Line.positionCount; i++)
        {
            foreach (var bullet in bullets)
            bullet.AddCoordinate(Line.GetPosition(i));
        }
    }
    private void CreateLineOnClick()
    {
        Line.positionCount = 0;
        Vector3 mousePos = GetWorldCoordinate();

        Line.positionCount = 2;
        Line.SetPosition(0, mousePos);
        Line.SetPosition(1, mousePos);
        isLineStarted = true;
    }

    private void UpdateLineWithDistance()
    {
        Vector3 currentPos = GetWorldCoordinate();
        float distance = Vector3.Distance(currentPos, Line.GetPosition(Line.positionCount - 1));
        if (distance > minimumVertexDistance)
        {
            UpdateLine();
        }
    }
    private void UpdateLine()
    {
        Line.positionCount++;
        Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate());
    }

    private void RemoveLine()
    {
        Line.positionCount = 0;
        isLineStarted = false;
    }
    private Vector3 GetWorldCoordinate()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(GameManager.instance.GetMousePosition()), out hit, 100,1);
        return hit.point + Vector3.up / 2;
    }
}