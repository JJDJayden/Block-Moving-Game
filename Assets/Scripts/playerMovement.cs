using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridMover : MonoBehaviour
{
    public float cellSize = 1f;
    public float moveTime = 0.15f;

    bool moving;

    void Update()
    {
        if (moving) return;

        if (Keyboard.current.wKey.wasPressedThisFrame) TryMove(Vector3.forward);
        else if (Keyboard.current.aKey.wasPressedThisFrame) TryMove(Vector3.left);
        else if (Keyboard.current.sKey.wasPressedThisFrame) TryMove(Vector3.back);
        else if (Keyboard.current.dKey.wasPressedThisFrame) TryMove(Vector3.right);
    }

    void TryMove(Vector3 mov)
    {
        Vector3 target = transform.position + mov * cellSize;

        if (Physics.CheckBox(target, Vector3.one * 0.4f, Quaternion.identity, LayerMask.GetMask("Wall")))
        {
            return;
        }

        StartCoroutine(MoveTo(target));
    }

    IEnumerator MoveTo(Vector3 target)
    {
        moving = true;
        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;
        moving = false;
    }
}