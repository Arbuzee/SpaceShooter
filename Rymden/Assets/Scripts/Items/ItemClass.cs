using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemClass
{
    public Sprite itemImage;
    public string name = default;
    [TextArea(2, 5)] public string description = default;
}
