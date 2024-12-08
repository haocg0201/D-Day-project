using System.Collections;
using UnityEngine;

public class WeaponIdle : MonoBehaviour
{
    public float moveDistance;
    public float moveDuration = 1f;
    
    void Start()
    {
        moveDistance = 0.02f;
        StartCoroutine(MoveUpMoveDown());
    }

    IEnumerator MoveUpMoveDown(){
        while(true){
            yield return StartCoroutine(MoveToY(transform.position.y, transform.position.y + moveDistance));
            yield return StartCoroutine(MoveToY(transform.position.y + moveDistance, transform.position.y));
            yield return StartCoroutine(MoveToY(transform.position.y, transform.position.y - moveDistance));
            yield return StartCoroutine(MoveToY(transform.position.y - moveDistance, transform.position.y));
        }
    }

    IEnumerator MoveToY(float startY, float endY)
    {
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            float newY = Mathf.Lerp(startY, endY, elapsedTime / moveDuration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, endY, transform.position.z);
    }
}
