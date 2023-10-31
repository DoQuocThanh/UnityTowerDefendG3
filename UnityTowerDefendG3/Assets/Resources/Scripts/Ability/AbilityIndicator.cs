using UnityEditor.Playables;
using UnityEngine;

public class AbilityIndicator : MonoBehaviour
{
    private Ability ability;
    public GameObject abilityAnimation;
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
                    ability.Activate(this);
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
	public Vector2 GetGridPosition()
	{
		Vector2 location = Vector2.zero;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 500f, Color.red);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 200f))
		{
			location = hit.point;
		}

		return location;
	}

	void ActivateAnimation()
    {
        abilityAnimation.SetActive(true);
    }

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
    }
}
