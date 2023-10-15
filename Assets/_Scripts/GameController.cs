using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LP.TurnBasedStrategyTutorial
{
    public class GameController : MonoBehaviour
    {
        // reference objects
        [SerializeField] private GameObject player = null;
        // [SerializeField] private GameObject fire = null;
        // [SerializeField] private GameObject water = null;
        // [SerializeField] private GameObject grass = null;
        [SerializeField] private GameObject enemy = null;
        [SerializeField] private Slider playerHealth = null;
        [SerializeField] private Slider enemyHealth = null;
        [SerializeField] private Button attackBtn1 = null;
        [SerializeField] private Button attackBtn2 = null;
        [SerializeField] private Button attackBtn3 = null;
        [SerializeField] private Button attackBtn4 = null;
        private string playerType = null;
        private bool isPlayerTurn = true;
        private string enemyType = "Fire";
        private bool playerWeaker;
        private int playerDmg = 10;
        private int enemyDmg = 10;
        public GameObject prefab;

        public void Start()
        {
            // Displays cube character based on selection
            playerType = PlayerPrefs.GetString("Type");
            player = GameObject.Find(playerType);
            Debug.Log(playerType);

            if (playerType.Equals("Grass"))
            {
                playerWeaker = true;
                attackBtn1.GetComponentInChildren<TMP_Text>().text = "Leafage";
                attackBtn2.GetComponentInChildren<TMP_Text>().text = "Razor Leaf";
                attackBtn3.GetComponentInChildren<TMP_Text>().text = "Energy Ball";
                attackBtn4.GetComponentInChildren<TMP_Text>().text = "Leaf Storm";
            } else if (playerType.Equals("Water")) 
            {
                playerWeaker = false;
                attackBtn1.GetComponentInChildren<TMP_Text>().text = "Bubble";
                attackBtn2.GetComponentInChildren<TMP_Text>().text = "Water Gun";
                attackBtn3.GetComponentInChildren<TMP_Text>().text = "Hydro Pump";
                attackBtn4.GetComponentInChildren<TMP_Text>().text = "Hydro Cannon";
            } else
            {
                attackBtn1.GetComponentInChildren<TMP_Text>().text = "Ember";
                attackBtn2.GetComponentInChildren<TMP_Text>().text = "Fire Blast";
                attackBtn3.GetComponentInChildren<TMP_Text>().text = "Flamethrower";
                attackBtn4.GetComponentInChildren<TMP_Text>().text = "Blast Burn";
            }
        }

        // attack function
        private IEnumerator Attack(GameObject target, float damage, int size)
        {
            if (target == enemy)
            {

                AttackAmin(player, true, playerType, size);
                yield return new WaitForSeconds(0.2f);
                if (playerWeaker)
                {
                    // if playerWeaker, enemy takes less damage
                    enemyHealth.value -= (playerDmg + damage) * 0.67f;
                } else
                {
                    enemyHealth.value -= (playerDmg + damage);
                }
            }
            else
            {
                AttackAmin(enemy, false, enemyType, size);
                yield return new WaitForSeconds(0.2f);
                if (playerWeaker)
                {
                    // if playerWeaker, player takes more damage
                    playerHealth.value -= damage * 1.33f;
                } else
                {
                    playerHealth.value -= damage;
                }
            }

            if (playerHealth.value <= 0 || enemyHealth.value <= 0)
            {
                //Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
            Debug.Log("Now change");
            ChangeTurn();
        }

        public void BtnAtack1()
        {
            StartCoroutine(Attack(enemy, 5, 1));
        }

        public void BtnAtack2()
        {
            StartCoroutine(Attack(enemy, 10, 2));
        }

        public void BtnAtack3()
        {
            StartCoroutine(Attack(enemy, 15, 3));
        }

        public void BtnAtack4()
        {
            StartCoroutine(Attack(enemy, 20, 4));
        }

        private void ChangeTurn()
        {
            isPlayerTurn = !isPlayerTurn;

            if (!isPlayerTurn)
            {
                attackBtn1.interactable = false;
                attackBtn2.interactable = false;
                attackBtn3.interactable = false;
                attackBtn4.interactable = false;
                Debug.Log("Starting enemy");
                StartCoroutine(EnemyTurn());
            }
            else 
            {
                attackBtn1.interactable = true;
                attackBtn2.interactable = true;
                attackBtn3.interactable = true;
                attackBtn4.interactable = true;
            }
        }

        private IEnumerator EnemyTurn()
        {
            Debug.Log("In enemy");
            yield return new WaitForSeconds(1);

            int random = 0;
            random = Random.Range(1, 5); // inclusive, exclusive
            // StartCoroutine(AttackAmin(enemy, false, enemyType, random));
            StartCoroutine(Attack(player, enemyDmg, random));
        }

        private void AttackAmin(GameObject attacker, bool isPlayer, string type, int size)
        {
            if (isPlayer)
            {
                if (type.Equals("Fire"))
                {
                    // prefabInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    prefab = Resources.Load("Fire Attack") as GameObject;
                }
                else if (type.Equals("Grass"))
                {
                    // prefabInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    prefab = Resources.Load("Grass Attack") as GameObject;
                }
                else
                {
                    // prefabInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    prefab = Resources.Load("Water Attack") as GameObject;
                }

                GameObject prefabInstance;
                prefabInstance = Instantiate(
                    prefab,
                    attacker.transform.position + new Vector3(1, 0, 0),
                    Quaternion.Euler(new Vector3(0, 0, 90)));

                if (size == 2)
                {
                    prefabInstance.transform.localScale += new Vector3(.25f, .25f, .25f);
                } else if (size == 3)
                {
                    prefabInstance.transform.localScale += new Vector3(.5f, .5f, .5f);
                } else if (size == 4)
                {
                    prefabInstance.transform.localScale += new Vector3(.75f, .75f, .75f);
                }
                
                prefabInstance.AddComponent<Rigidbody>();
                prefabInstance.GetComponent<Rigidbody>().AddForce(attacker.transform.right * 3000f);
                Destroy(prefabInstance, 0.2f);
            } else
            {
                if (type.Equals("Fire"))
                {
                    // prefabInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    prefab = Resources.Load("Fire Attack") as GameObject;
                }
                else if (type.Equals("Grass"))
                {
                    // prefabInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    prefab = Resources.Load("Grass Attack") as GameObject;
                }
                else
                {
                    // prefabInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    prefab = Resources.Load("Water Attack") as GameObject;
                }

                GameObject prefabInstance;
                prefabInstance = Instantiate(
                    prefab,
                    attacker.transform.position - new Vector3(1, 0, 0),
                    Quaternion.Euler(new Vector3(0, 0, 90)));

                if (size == 2)
                {
                    prefabInstance.transform.localScale += new Vector3(.25f, .25f, .25f);
                }
                else if (size == 3)
                {
                    prefabInstance.transform.localScale += new Vector3(.5f, .5f, .5f);
                }
                else if (size == 4)
                {
                    prefabInstance.transform.localScale += new Vector3(.75f, .75f, .75f);
                }

                prefabInstance.AddComponent<Rigidbody>();
                prefabInstance.GetComponent<Rigidbody>().AddForce(attacker.transform.right * -3000f);
                Destroy(prefabInstance, 0.2f);
            }
        }




    }
}
