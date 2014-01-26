//ybzuo-ro
using UnityEngine;
public class EditCamerar:MonoBehaviour
{
    static EditCamerar m_single = null;
    public static EditCamerar get_single()
    {
        if (m_single == null)
        {
            m_single = new EditCamerar();
        }
        return m_single;
    }
    void Update()
    {
		normal_op();
    }
    void normal_op()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationX = Camera.mainCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            Camera.mainCamera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        float _input_value = Input.GetAxis("Horizontal");
        if (_input_value != 0.0f)
        {
            Camera.mainCamera.transform.Translate(_input_value*m_speed, 0.0f, 0.0f, Space.Self);
        }
        _input_value = Input.GetAxis("Vertical");
        if (_input_value != 0.0f)
        {
            Camera.mainCamera.transform.Translate(0.0f, 0.0f, _input_value*m_speed, Space.Self);
        }
    }
    float sensitivityX = 1F;
    float sensitivityY = 1F;
    float minimumY = -60F;
    float maximumY = 60F;
    float rotationY = 0F;
	
	float m_speed=0.1f;
}
