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
    int _itemType;
    [Tooltip("0: Shield\n1: Mulitshot")]
    [SerializeField] int _itemIndex;
    [Tooltip("0: Common\n1: Rare\n2: Legendary")]
    [SerializeField] int _itemRarity;
    [SerializeField] string _rarityName;
    [SerializeField] bool _isLegendary;
    [SerializeField] PlayerAbilities _player;

    public void Awake()
    {
        Destroy(transform.gameObject, _destroyTime);
        _itemType = Random.Range(0, itemProperties.Length);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
    }

    public void ItemFunction(int raryity)
    {
        _itemRarity = raryity;
        _itemIndex = Random.Range(0, itemProperties.Length);
        ApplyAssets(true);
    }

    public void ApplyAssets(bool apply)
    {
        imageObject.sprite = itemProperties[_itemIndex].itemImage;
        imageObject.color = imageColor[_itemRarity];

        switch (_itemRarity)
        {
            case 0:
                {
                    _rarityName = "Common";
                    break;
                }

            case 1:
                {
                    _rarityName = "Rare";
                    break;
                }

            case 2:
                {
                    _rarityName = "Legendary";
                    _isLegendary = true;
                    break;
                }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject _playerHud = GameObject.Find("PlayerHud");

        if(other.CompareTag("Player") && !_player.activateAbility)
        {
            _playerHud.transform.GetChild(1).GetComponent<Image>().sprite = imageObject.sprite;
            _playerHud.transform.GetChild(1).GetComponent<Image>().color = imageObject.color;
            _player.GetAbilityInfo(_itemIndex, _itemRarity);   
            Destroy(transform.gameObject);
        }

        if (other.CompareTag("Courser"))
        {
            GameObject _infoScreen = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            _infoScreen.SetActive(true);
            _infoScreen.transform.GetChild(1).GetComponent<Text>().text = itemProperties[_itemIndex].name.ToUpper();            //Title
            _infoScreen.transform.GetChild(3).GetComponent<Text>().text = $"<i>{itemProperties[_itemIndex].description}</i>";   //Discripton
            _infoScreen.transform.GetChild(2).GetComponent<Text>().text = $"<b>{_rarityName}</b>";                              //Rarity
            _infoScreen.transform.GetChild(2).GetComponent<Text>().color = imageObject.color;                                   //Color of rarity
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Courser"))
        {
            GameObject _infoScreen = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            _infoScreen.SetActive(false);
        }
    }
}
