using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RaritySpawn : MonoBehaviour
{
    [Header("Item Relevent Variables")]
    public int rarity;
    public Material[] rarityColor;
    public GameObject spawnItem;

    [Header("Nessesary stuff")]
    public MeshRenderer objectMaterial;

    int _rarityRandom;
    bool _itemIsLegendary = false;
    Asteroid _asteroidObject;

    private void Awake()
    {
        _asteroidObject = transform.GetComponent<Asteroid>();
        _rarityRandom = Random.Range(0, 10);

        //Kan optimeras sen
        #region Rarity Gamble
        if (_rarityRandom > 4)       //Common
        {
            rarity = 0;
        }

        if (_rarityRandom <= 4 && _rarityRandom > 1)        //Rare
        {
            rarity = 1;
        }

        if (_rarityRandom <= 1)                              //Legendary
        {
            rarity = 2;
        }

        Decide();
        #endregion
    }

    void Decide()
    {
        switch (rarity)    //Choose rarity
        {
            case 0:     //Common
                {
                    objectMaterial.material = rarityColor[0];
                    break;
                }

            case 1:     //Rare
                {
                    objectMaterial.material = rarityColor[1];
                    break;
                }

            case 2:     //Legendery
                {
                    objectMaterial.material = rarityColor[2];
                    _itemIsLegendary = true;
                    break;
                }
        }
        return;
    }

    public void SpawnItem(bool spawn)
    {
        if (spawn)
        {
            GameObject _itemClone = Instantiate(spawnItem, transform.position, transform.rotation);
            _itemClone.GetComponent<Ability>().ItemFunction(rarity);
        }
    }

}
