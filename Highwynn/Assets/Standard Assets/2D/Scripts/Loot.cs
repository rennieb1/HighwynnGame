using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    [SerializeField]
    private Text pickupText = null;
    private bool pickupAllowed;

    // Start is called before the first frame update
    void Start()
    {
        pickupText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }
        

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickupText.gameObject.SetActive(true);
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickupText.gameObject.SetActive(false);
            pickupAllowed = false;
        }
    }

    private void Pickup()
    {
        Destroy(gameObject);
    }
}
