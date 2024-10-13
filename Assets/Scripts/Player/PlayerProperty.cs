using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> PropertyDict;

    public int Hp = 100;

    public int Energy = 100;

    public int Mental = 100;
    // Start is called before the first frame update
    void Start()
    {
        PropertyDict = new Dictionary<PropertyType, List<Property>>();
        PropertyDict.Add(PropertyType.HPValue, new List<Property>());
        PropertyDict.Add(PropertyType.EnergyValue, new List<Property>());
        PropertyDict.Add(PropertyType.MentalValue, new List<Property>());
        PropertyDict.Add(PropertyType.SpeedValue, new List<Property>());
        PropertyDict.Add(PropertyType.AttackValue, new List<Property>());

        AddProperty(PropertyType.SpeedValue, 5);
        AddProperty(PropertyType.AttackValue, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
