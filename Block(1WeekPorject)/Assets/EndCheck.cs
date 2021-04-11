using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ShootManager.Instance.EndGame();
    }
}
