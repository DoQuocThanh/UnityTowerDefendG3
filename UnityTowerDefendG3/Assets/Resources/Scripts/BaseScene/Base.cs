using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public float totalHeath = 100f;
    public float currentHeath ;

    public Text currentHeathText;
	public Slider heathSlider;

	// Start is called before the first frame update
	void Start()
    {
        currentHeath = totalHeath;
		heathSlider.maxValue = totalHeath;
		heathSlider.value = currentHeath;
		currentHeathText.text = currentHeath.ToString() + "/" + totalHeath.ToString();

    }

    // Update is called once per frame
    void Update()
    {
    }


    public void takeDamage(float damage)
    {

        currentHeath -= damage;
        if (currentHeath <=20)
        {
            currentHeathText.color = Color.red;
            AudioManeger.Instance.PlaySFX("base");
		}
        if (currentHeath <= 0)
        {
            currentHeath = 0;
            gameObject.SetActive(false);

        }
		heathSlider.value = currentHeath;
		currentHeathText.text = currentHeath.ToString() + "/" + totalHeath.ToString();

    }
}
