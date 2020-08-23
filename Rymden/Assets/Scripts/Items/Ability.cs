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

    [Header("Debug Menu")]
    public bool debugMode = false;
    [Tooltip("0: Shield\n1: Tracking Bombs\n")]
    [Range(0, 1)]public int itemIndexDebug;
    [Tooltip("0: Common\n1: Rare\n2: Legendary")]
    [Range(0, 2)]public int rarityDebug;

    [Header("Stats")]
    [Tooltip("0: Shield\n1: Mulitshot")]
    [SerializeField] int _itemIndex;
    [Tooltip("0: Common\n1: Rare\n2: Legendary")]
    [SerializeField] int _itemRarity;
    [SerializeField] string _rarityName;
    [SerializeField] PlayerAbilities _player;

    public void Awake()
    {
        if (debugMode)
        {
            _itemIndex = itemIndexDebug;     //Chooses ability while debuging
            _itemRarity = rarityDebug;       //Chooses rarity whlie debuging
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
            ApplyAssets(true);
        }
        else
        {
            Destroy(transform.gameObject, _destroyTime);
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
        }
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
                    break;
                }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject _playerHud = GameObject.Find("PlayerHud").transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject;

        if(other.CompareTag("Player") && !_player.activateAbility)
        {
            _playerHud.GetComponent<Image>().sprite = imageObject.sprite;
            _playerHud.GetComponent<Image>().color = imageObject.color;
            _player.GetAbilityInfo(_itemIndex, _itemRarity);   
            Destroy(transform.gameObject);
        }

        if (other.CompareTag("Courser"))
        {
            GameObject _infoScreen = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            _infoScreen.SetActive(true);
            _infoScreen.transform.GetChild(1).GetComponent<Text>().text = itemProperties[_itemIndex].name.ToUpper();            //Title
            _infoScreen.transform.GetChild(2).GetComponent<Text>().text = $"<b>{_rarityName}</b>";                              //Rarity
            _infoScreen.transform.GetChild(2).GetComponent<Text>().color = imageObject.color;

            if (_itemRarity == 2)
            {
                _infoScreen.transform.GetChild(3).GetComponent<Text>().text = $"<i>{itemProperties[_itemIndex].description}\n\n<color=yellow><b>Legendary Ability:</b></color> \n{itemProperties[_itemIndex].specialPower}</i>";   //Discripton

            }
            else
            {
                _infoScreen.transform.GetChild(3).GetComponent<Text>().text = $"<i>{itemProperties[_itemIndex].description}</i>";   //Discripton
            }
            //Color of rarity
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
