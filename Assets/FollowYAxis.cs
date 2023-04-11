using UnityEngine;

public class FollowYAxis : MonoBehaviour
{
    public float speed = 1.0f; // The speed at which to move towards the target
    public float delayMultiplier = 0.2f; // The amount by which to multiply the delay for each subsequent sphere
    public Transform target; // The target to follow along the Y axis
    private int index; // The index of the current object

    void Start()
    {
        // Get all of the child objects of this object and sort them by their Y position
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }
        System.Array.Sort(children, (Transform a, Transform b) => a.position.y.CompareTo(b.position.y));

        // Loop through each child object and set its target to the previous child object
        for (int i = 1; i < children.Length; i++)
        {
            if (children[i].GetComponent<FollowYAxis>() == null)
            {
                FollowYAxis follower = children[i].gameObject.AddComponent<FollowYAxis>();
                follower.target = children[i - 1];
                follower.speed = speed;
                follower.index = i;
                follower.delayMultiplier = delayMultiplier;
            }
        }

        // Set the target for the first sphere to the target specified in the inspector
        if (index == 0 && target != null)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
    }

    void Update()
    {
        if (index > 0 && target != null)
        {
            // Calculate the position to move towards based on the target's Y position
            Vector3 targetPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);

            // Move towards the target position at the specified speed with a delay based on the object's index
            float delay = (index - 1) * delayMultiplier;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime * (1f / delay));
        }
    }
}
