using System;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class GyroSensorCube : MonoBehaviour {

	SerialPort arduino;

	void Start() {
		string[] cmdargs = Environment.GetCommandLineArgs();
		string port = "com4";
		if (Array.IndexOf(cmdargs, "-port") > 0) {
			port = cmdargs[Array.IndexOf(cmdargs, "-port") + 1];
		}
		int baudRate = 9600;
		if (Array.IndexOf(cmdargs, "-baud") > 0) {
			baudRate = int.Parse(cmdargs[Array.IndexOf(cmdargs, "-baud") + 1]);
		}
		if (port != "") {
			arduino = new SerialPort(port, baudRate);
			arduino.Open();
			Debug.Log("opened");
		}
	}

	private void Update() {
		if (arduino != null) {
			float[] rots = Array.ConvertAll(arduino.ReadLine().Split('|'), float.Parse);
			transform.rotation = Quaternion.Euler(new Vector3(rots[0], rots[1], rots[2]));
			Text txt = FindObjectOfType<Text>();
			txt.text = "<color=red>X: " + rots[0] + "</color>\n" + "<color=green>Y: " + rots[1] + "</color>\n" + "<color=blue>Z: " + rots[2] + "</color>";
		}
	}
}
