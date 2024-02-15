using UnityEngine;
using UnityEngine.Analytics;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    //Pour gérer la rotation de la caméra
    [SerializeField]
    private Camera cam;

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRotation; 
    private float horizontal; 
    private float vertical; 
    private bool touchGround = true; 

    // rb: L'element où l'on appliquera les deplacements (le joueur)
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }

    // executer sur un interval de temps fixe
    private void FixedUpdate() 
    {
        PerformMovement();
        PerformRotation(); 
        PerformJump();
    }

    // recupere la velocité venant de PlayerMotor et l'applique
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    
    // récupere la rotation du joueur venant de PlayerMotor et l'applique
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // récupere le deplacement horizontal et vertical venant de PlayerMotor et l'applique
    public void Jump(float _horizontal, float _vertical){
        horizontal = _horizontal; 
        vertical = _vertical; 
    }

    // récupere  la rotation de la camera venant de PlayerMotor et l'applique
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    // applique le deplacement du joueur
    private void PerformMovement()
    {
        //s'il y a deplacement
        if (velocity != Vector3.zero) 
        {
            //deplacement du joueur au fil du temps (fixedDeltaTime) et non d'un coup
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); 
        }
    }

    // applique la rotation du joueur
    private void PerformRotation()
    {
        // rotation gauche/ droite
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation)); //quaternion gere les rotation dans unity 
        // rotation haut/ bas
        cam.transform.Rotate(-cameraRotation);
    }

    // applique le saut
    private void PerformJump(){
        transform.Translate(horizontal,0, vertical);
        if(Input.GetButtonDown("Jump") && touchGround){
            rb.AddForce(new Vector3(0,5,0), ForceMode.Impulse);
            touchGround = false; 
        }        
   }

   // verifie si le joueur touche le sol afin d'appliquer le saut 
   private void OnCollisionEnter(Collision collision){
    if(collision.gameObject.tag == "Ground"){
        touchGround = true; 
    }
   }
}
