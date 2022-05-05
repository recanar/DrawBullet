using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject gun;
    public Transform ammo;

    //Invoked when a button is clicked.
    public void Start()
    {
        //Finds and assigns the child named "Gun".
        gun = transform.Find("Gun").gameObject;

        //If the child was found.
        if (gun != null)
        {
            //Find the child named "ammo" of the gameobject "magazine" (magazine is a child of "gun").
            ammo = gun.transform.Find("Magazine/Ammo");
        }
        else Debug.Log("No child with the name 'Gun' attached to the player");
    }
}