using UnityEngine;

public class PlaneGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Goal");
        }
    }
}
