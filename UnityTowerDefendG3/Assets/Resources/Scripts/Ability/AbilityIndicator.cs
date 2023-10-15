using UnityEngine;

public class AbilityIndicator : MonoBehaviour
{
    public GameObject abilityAnimation;
    public float abilityDmg = 1;
    public BoxCollider boxCollider;
    private void OnEnable()
    {
        abilityAnimation.transform.localScale = gameObject.transform.localScale;
    }

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            // Raycast to detect the object click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    abilityAnimation.transform.position = gameObject.transform.position;
                    DmgEnemy();
                    gameObject.SetActive(false); // Hide Indicator to show animation              
                    ActivateAnimation();
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) // Check for right mouse button click
        {
            gameObject.SetActive(false);
        }
    }

    void DmgEnemy()
    {
        Vector2 center = boxCollider.bounds.center;
        // Size of the BoxCollider
        Vector2 size = boxCollider.bounds.size;
        // Rotation of the BoxCollider
        Quaternion rotation = boxCollider.transform.rotation;
        // Check for objects within the BoxCollider volume
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag(("Enemy")))
            {
                AudioManeger.Instance.PlaySFX("ki_nang");

                col.gameObject.GetComponent<EnemyController>().takeDamage(abilityDmg);
            }
        }
    }
    void ActivateAnimation()
    {
        abilityAnimation.SetActive(true);
    }
}
