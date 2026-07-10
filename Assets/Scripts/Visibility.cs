using UnityEngine;

public class Visibility : MonoBehaviour
{
    [Header("Objects")]
    private Transform player;

    [Header("Components")]
    private Material mat;
    [SerializeField] private LayerMask obstacleMask;

    [Header("Variables")]
    [SerializeField] private float fadeSpeed = 5f;
    private float currentVisibility = 1f;

    private void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // a temporary solution for test task 
        player = playerObj.transform;
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleMask);

        float targetVisibility = (hit.collider != null) ? 0f : 1f;
        currentVisibility = Mathf.MoveTowards(currentVisibility, targetVisibility, fadeSpeed * Time.deltaTime);

        mat.SetFloat("_IsVisible", currentVisibility);
    }
}
