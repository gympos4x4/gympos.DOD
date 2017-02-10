using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Threading;

namespace gympos.DOD {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		public string log = "";

		public MainWindow() {
			InitializeComponent();
		}

		SerialPort arduino;

		private void start_Click(object sender, RoutedEventArgs e) {
			if (arduino == null) {
				log = "";
				arduino = new SerialPort(comPort.Text, int.Parse(baudRateTxt.Text));
				arduino.DataReceived += Arduino_DataReceived;
				arduino.Open();
				start.Content = "Stop";
			} else {
				arduino.Close();
				arduino = null;
				start.Content = "Start";
			}
		}

		private void Arduino_DataReceived(object sender, SerialDataReceivedEventArgs e) {
			SerialPort senderSP = (SerialPort)sender;
			string data = senderSP.ReadExisting();
			log +=  data;
			logTxt.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
			{
				logTxt.Text = log;
				logTxt.ScrollToEnd();
			}));
			string[] lines = log.Split('\n');
			lastLine.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => {
				lastLine.Text = lines[lines.Length - 2];
			}));
		}
	}
}
