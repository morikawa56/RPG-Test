using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> PropertyDict;

    public int Hp = 100;

    public int Energy = 100;

    public int Mental = 100;

    public int Level = 1;

    public int CurrentExp = 0;

    // Start is called before the first frame update
    void Awake()
    {
        PropertyDict = new Dictionary<PropertyType, List<Property>>();
        PropertyDict.Add(PropertyType.SpeedValue, new List<Property>());
        PropertyDict.Add(PropertyType.AttackValue, new List<Property>());

        AddProperty(PropertyType.SpeedValue, 5);
        AddProperty(PropertyType.AttackValue, 20);

        EventCenter.OnEnemyDied += OnEnemyDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseDrug(ItemScriptObject itemSO)
    {
        foreach (var p in itemSO.propertyList)
        {
            AddProperty(p.propertyType, p.value);
        }
    }

    void AddProperty(PropertyType pt, int value)
    {
        switch (pt)
        {
            case PropertyType.HPValue:
                Hp += value;
                return;
            case PropertyType.EnergyValue:
                Energy += value;
                return;
            case PropertyType.MentalValue:
                Mental += value;
                return;
        }
        List<Property> list;
        PropertyDict.TryGetValue(pt, out list);
        list.Add(new Property(pt, value));
    }

    public void RemoveProperty(PropertyType pt, int value)
    {
        List<Property> list;
        PropertyDict.TryGetValue(pt, out list);
        list.Remove(list.Find(x => x.value == value));
    }

    private void OnDestroy()
    {
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        this.CurrentExp += enemy.Exp;
        if (CurrentExp >= Level * 30)
        {
            CurrentExp -= Level * 30;
            Level++;
        }
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }
}
