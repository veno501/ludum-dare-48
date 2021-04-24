using UnityEngine;

public class Thrusters : MonoBehaviour
{
	public ParticleSystem LF;
    public ParticleSystem LB;
    public ParticleSystem RF;
    public ParticleSystem RB;
    
    // Color initialThrusterColor;

    void Awake()
    {
        if (LF == null || LB == null || RF == null || RB == null)
            Debug.LogWarning("Thruster particle systems not found!");
        
        // initialThrusterColor = LF.main.startColor.color;
    }

    public void UpdateThrusters(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            // input = Player.rb.velocity;
            SetThrusterActive(RF, false);
            SetThrusterActive(LF, false);
            SetThrusterActive(LB, false);
            SetThrusterActive(RB, false);
            return;
        }

        Vector2 thrusterDirection = transform.parent.InverseTransformDirection(-input);
        float angle = Mathf.Atan2(thrusterDirection.y, thrusterDirection.x) * Mathf.Rad2Deg/* - 90f*/;
        // Debug.DrawRay(transform.position, transform.TransformDirection(thrusterDirection));

        SetThrusterActive(RF, Mathf.Abs(Mathf.DeltaAngle(45f, angle)) < 60f);
        SetThrusterActive(LF, Mathf.Abs(Mathf.DeltaAngle(135f, angle)) < 60f);
        SetThrusterActive(LB, Mathf.Abs(Mathf.DeltaAngle(225f, angle)) < 60f);
        SetThrusterActive(RB, Mathf.Abs(Mathf.DeltaAngle(315f, angle)) < 60f);
    }

    void SetThrusterActive (ParticleSystem thruster, bool emit)
    {
        var emission = thruster.emission;
        emission.enabled = emit;
        // emission.rateOverTime = new ParticleSystem.MinMaxCurve(emit ? 40f : 5f);

        // var main = thruster.main;
        // main.startColor = new ParticleSystem.MinMaxGradient(emit ? Color.cyan : initialThrusterColor);
    }

    public void DisableThrusters ()
    {
        RF.Stop();
        LF.Stop();
        LB.Stop();
        RB.Stop();
    }

    public void EnableThrusters ()
    {
        RF.Play();
        LF.Play();
        LB.Play();
        RB.Play();
    }
}