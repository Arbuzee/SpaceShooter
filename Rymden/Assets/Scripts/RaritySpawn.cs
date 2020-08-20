using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RaritySpawn : MonoBehaviour
{
    public Material[] rarityColor;
    public ItemTemplateClass[] items;
    
    public bool debugMode = false;
    private Asteroid _astroidInfo;
    public MeshRenderer objectMaterial;

    [SerializeField] int _powerupIndex = default;
    [SerializeField] int _rarityRandom;
    [SerializeField] int _rarity;


    private void Awake()
    {
        _powerupIndex = Random.Range(0, 2);
        _rarityRandom = Random.Range(0, 100);
        _astroidInfo = transform.GetComponent<Asteroid>();

        //Kan optimeras sen
        #region Rarity Gamble
        if (_rarityRandom > 40)       //Common
        {
            Debug.Log("Common");
            _rarity = 0;
        }

        if (_rarityRandom <= 40 && _rarityRandom > 10)        //Rare
        {
            Debug.Log("Rare");
            _rarity = 1;
        }

        if (_rarityRandom <= 10)                              //Legendary
        {
            Debug.Log("Legendary");
            _rarity = 2;
        }

        Decide();
        #endregion
    }

    private void Update()
    {
        if (_astroidInfo.spawnOnDeath)
        {

        }
    }

    void Decide()
    {
        switch (_powerupIndex)  //Choos an ability
        {
            #region Ability 1
            case 0:
                {
                    #region Rarity

                    switch (_rarity)    //Choose rarity
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
                                break;
                            }
                    }

                    return;
                    #endregion
                }
            #endregion

            #region Ability 2
            case 1:
                {
                    #region Rarity
                    switch (_rarity)    //Choose rarity
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
                                break;
                            }
                    }
                    return;
                    #endregion
                }
                #endregion
        }

    }
}
