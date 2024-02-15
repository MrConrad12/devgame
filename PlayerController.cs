using UnityEngine;

[RequireComponent (typeof(PlayerMotor))] 
public class PlayerController : MonoBehaviour
{
    // [SerializeField] : permet de modifier la valeur via l'Inspector
    [SerializeField] 
    private float speed = 5f;
    [SerializeField]
    private float mouseSensitivityX = 3.5f;
    [SerializeField]
    private float mouseSensitivityY = 3.5f;

    private PlayerMotor motor;

    private void Start()
    {
        // nous permetra d'envoyer les données nécessaires au déplacement
        motor = GetComponent<PlayerMotor>(); 
    }

    private void Update()
    {
        // Recuperation des touches
        // valeur entre -1 et 1
        // changement des touches dans "input manager"
        float xMov = Input.GetAxisRaw ("Horizontal"); // prenne les info brut gauche/droite
        float zMov = Input.GetAxisRaw ("Vertical"); // prenne les info brut avant/arriere
        
        Vector3 moveHorizontal = transform.right * xMov; // mouvement horizontal
        Vector3 moveVertical = transform.forward * zMov; // mouvement vertical

        //  gestion de la velocite du joueur
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed; //normaliser : magnitude de 1
        motor.Move(velocity);

        // gestion la rotation du joueur (rotation gauche droite)
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;
        motor.Rotate(rotation);

        // gestion la rotation de la camera (voir en haut et en bas)
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRot, 0, 0) * mouseSensitivityY;
        motor.RotateCamera(cameraRotation);

        // gestion du saut en fonction du deplacement du joueur
        float horizontal = xMov * Time.fixedDeltaTime * speed; 
        float vertical = zMov * Time.fixedDeltaTime * speed; 
        motor.Jump(horizontal, vertical);
    }
}
