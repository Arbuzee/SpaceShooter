using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public ItemClass[] itemProperties;

    [Header("Item Properties")]
    public int _destroyTime = 10;
    public Image imageObject;
    public Color[] imageColor;

    [Header("Debug")]
    [Tooltip("1: Shield")]
    [SerializeField] int _itemType;
    [SerializeField] int _itemIndex;
    [SerializeField] int _itemRarity;
    [SerializeField] Sprite _activeImage;

    public void Awake(int rarity)
    {
        _itemRarity = rarity;
        ItemFunction();
        Destroy(transform.gameObject, _destroyTime);
        _itemType = Random.Range(0, itemProperties.Length);
    }

    void ItemFunction()
    {
        _itemIndex = Random.Range(0, itemProperties.Length);

        ApplyAssets(true);
    }

    public void ApplyAssets(bool apply)
    {
        imageObject.sprite = itemProperties[_itemIndex].itemImage;
        imageObject.color = imageColor[_itemRarity];
    }
    private void OnTriggerEnter(Collider other)
    {
        //Collect item
    }
}
