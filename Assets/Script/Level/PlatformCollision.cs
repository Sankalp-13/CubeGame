using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    private bool hasBeenTouched = false;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player") && !hasBeenTouched)
        {

            ScoreManager.instance.IncreaseScore(1);


            hasBeenTouched = true;
        }
    }
}
