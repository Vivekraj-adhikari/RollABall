using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private int score;
    private float movementX;
    private float movementY;
    public float speed = 10;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        setCountText();
        winTextObject.SetActive(false);
    }

    public void FixedUpdate(){
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            score += 1;
            setCountText();
        }
    }

    void setCountText (){
        countText.text = "Count: " + score.ToString();
        if(score >= 11){
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);

            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
}
