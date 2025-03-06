using UnityEngine;

public class Airplane : MonoBehaviour
{
  private Rigidbody rb;
   [SerializeField] private float enginePower = 500f;
   [SerializeField] private float liftBooster = 5f;
   [SerializeField] private float drag = 0.02f;
   [SerializeField] private float angularDrag = 0.02f;
   [SerializeField] private float yawPower = 50f;
   [SerializeField] private float pitchPower = 50f;
   [SerializeField] private float rollPower = 30f;

    void Start()
    {
         rb = GetComponent<Rigidbody>();
       rb.useGravity = false;
       rb.interpolation = RigidbodyInterpolation.Interpolate;
       rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Update is called once per frame
     void FixedUpdate()
   {
       // Thrust
       if (Input.GetKey(KeyCode.Space))
       {
           rb.AddForce(transform.forward * enginePower);
       }
       // Lift
       Vector3 lift = Vector3.Project(rb.velocity, transform.forward);
       rb.AddForce(transform.up * lift.magnitude * liftBooster);
       // Drag
       rb.linearDamping = rb.velocity.magnitude * drag;
       rb.angularDamping = rb.velocity.magnitude * angularDrag;
       // Rotation Controls
       float yaw = Input.GetAxis("Horizontal") * yawPower * Time.deltaTime;
       float pitch = Input.GetAxis("Vertical") * pitchPower * Time.deltaTime;
       float roll = Input.GetAxis("Roll") * rollPower * Time.deltaTime;
       rb.AddTorque(transform.up * yaw);
       rb.AddTorque(transform.right * pitch);
       rb.AddTorque(transform.forward * -roll);
   }
}
