using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaritySpawn : MonoBehaviour
{
    [SerializeField]
    int _powerupIndex;
    int _rarity;

    private void Awake()
    {
        _powerupIndex = Random.Range(0, 4);
        _rarity = Random.Range(0, 3);
    }

    void Decide()
    {
        switch (_powerupIndex)  //Choos an ability
        {
            #region Ability 1
            case 0:     
                {
                    #region Rarity

                    switch (_rarity)    
                    {
                        case 0:     //Common
                            {
                                return;
                            }

                        case 1:     //Rare
                            {
                                return;
                            }

                        case 2:     //Legendery
                            {
                                return;
                            }
                    }

                    return;
                    #endregion
                }
                #endregion

            #region Ability 1
            case 1:     
                {
                    #region Rarity

                    switch (_rarity)    
                    {
                        case 0:     //Common
                            {
                                return;
                            }

                        case 1:     //Rare
                            {
                                return;
                            }

                        case 2:     //Legendery
                            {
                                return;
                            }
                    }

                    return;
                    #endregion
                }
                #endregion
        }

        //Choose a rarity
    }
}
