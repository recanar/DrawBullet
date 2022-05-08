using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum States
{
    TapToStart,
    Playing,
    Lose
    //Win
}
public class GameManager : MonoBehaviour
{
    [HideInInspector] public States currentState;
    [HideInInspector] public static GameManager instance;

    [HideInInspector] public int currentAmmo;
    [HideInInspector] public int point;

    [SerializeField] private GameObject loseText;
    [SerializeField] private Text pointText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;//create instance
        }
    }
    void Start()
    {
        currentAmmo = 5;//ammo start value
        currentState = States.TapToStart;//
        StartCoroutine("ReloadAmmo");
    }
    private void Update()
    {
        pointText.text = "Point:" + point;
        if (currentState==States.Lose)
        {
            loseText.SetActive(true);
        }
    }
    void LateUpdate()//to not create bullet on first click, late update used
    {
        if (MouseDown()&&currentState==States.TapToStart)//waiting input to start game
        {
            currentState = States.Playing;
        }
    }
    IEnumerator ReloadAmmo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.75f);
            if (currentState == States.Playing && currentAmmo < 5)
            {
                currentAmmo++;
            }
        }
    }
    #region Inputs
    public bool MouseDown()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool MouseButtonClicking()
    {
        return Input.GetMouseButton(0);
    }
    public bool MouseUp()
    {
        return Input.GetMouseButtonUp(0);
    }
    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
    #endregion

}
