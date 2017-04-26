using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;
    public LayerMask blockingLayer;

    BoxCollider2D boxCollider;
    Rigidbody2D rigidBody;
    float inverseMoveTime;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move(int xDirection, int yDirection, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDirection, yDirection);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));

            return true;
        }
        else
        {
            return false;
        }
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float squareRemainingDistance = (transform.position - end).sqrMagnitude;

        while (squareRemainingDistance < float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidBody.position, end, inverseMoveTime * Time.deltaTime);
            rigidBody.MovePosition(newPosition);

            squareRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
    }

    protected virtual void AttemptMove<T>(int xDirection, int yDirection) where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDirection, yDirection, out hit);

        if (hit.transform == null)
        {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected abstract void OnCantMove<T>(T Component) where T : Component;
}
