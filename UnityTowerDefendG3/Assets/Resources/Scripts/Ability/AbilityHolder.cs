using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    public GameObject abilityIndicator;
    public float cooldownTime;
    public GameObject abilityLayer;
    public Image cooldownLayer;
    public Text cooldownText;


    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;

    public void ActivateIndicator()
    {
        abilityIndicator.SetActive(true);
        abilityIndicator.GetComponent<AbilityIndicator>().SetAbility(ability);
    }


    void Start()
    {
        // Convert the non-UI object's position to screen space
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        // Set the UI Image's anchored position to the screen position
        abilityLayer.GetComponent<RectTransform>().position = screenPosition;
        cooldownLayer.fillAmount = 0f;
        cooldownText.text = "";
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                //Debug.Log("ready");
                if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            ActivateIndicator();
                            state = AbilityState.active;
                        }
                        //Debug.Log("Trigger");
                    }
                }
                break;

            case AbilityState.active:
                //Debug.Log("active");
                if (Input.GetMouseButtonDown(0))
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    state = AbilityState.ready;

                }

                break;

            case AbilityState.cooldown:
                //Debug.Log("cooldown");
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                    CooldownAbility();
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    void CooldownAbility()
    {
        if (cooldownTime <= 0f)
        {
            cooldownLayer.fillAmount = 0f;
            cooldownText.text = "";
        }
        else
        {
            cooldownLayer.fillAmount = cooldownTime / ability.cooldownTime;
            cooldownText.text = Mathf.Ceil(cooldownTime).ToString();
        }
    }

}
