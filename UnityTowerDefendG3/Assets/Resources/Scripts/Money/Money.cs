using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static Money instance;
    [Header ("Clone button")]
    public Transform container;
    public MoneyCard cardPrefab;
    public TowerItem[] towers;

    [Header ("Coin infomation")]
    public TextMeshProUGUI textWarningTmp;
    public int currentMoney = 30;
    public TextMeshProUGUI moneyTmp;
    public MoneyCard SelectedCard{ get;set;}
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadStore();
       
    }
    private void Update()
    {
        moneyTmp.text = currentMoney.ToString();
    }
    private void LoadStore()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            MoneyCard card = Instantiate(cardPrefab,container);
            card.InitializeCard(towers[i]);
        }
    }

    public void GiveMoney(int amount)
    {
        currentMoney += amount;
    }

    public bool SpendMoney(int amountToSpend)
    {
        bool spent = false;
        if (amountToSpend <= currentMoney)
        {
            spent = true;
            currentMoney -= amountToSpend;
        }else
        {
            textWarningTmp.text = "Not enough gold";
        }
        
        return spent;
    }

}
