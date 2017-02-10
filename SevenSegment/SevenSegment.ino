/*
 Name:		SevenSegment.ino
 Created:	2/8/2017 7:32:33 PM
 Author:	Juraj
*/

int d[2][7] = {
	{ 11, 10, 2, A1, A0, A2, 12 },
	{ 8, 7, 6, 4, 3, 9, 5 }
};

int values[11][7] = {
	//  { a, b, c, d, e, f, g };
		{ 0, 0, 0, 0, 0, 0, 1 }, //0
		{ 1, 0, 0, 1, 1, 1, 1 }, //1
		{ 0, 0, 1, 0, 0, 1, 0 }, //2
		{ 0, 0, 0, 0, 1, 1, 0 }, //3
		{ 1, 0, 0, 1, 1, 0, 0 }, //4
		{ 0, 1, 0, 0, 1, 0, 0 }, //5
		{ 0, 1, 0, 0, 0, 0, 0 }, //6
		{ 0, 0, 0, 1, 1, 1, 1 }, //7
		{ 0, 0, 0, 0, 0, 0, 0 }, //8
		{ 0, 0, 0, 0, 1, 0, 0 }, //9
		{ 0, 1, 1, 0, 0, 0, 0 }  //E
};
int x = 0;
// the setup function runs once when you press reset or power the board
void setup() {
	for (int i = 0; i < 2; i++) {
		for (int j = 0; j < 7; j++) {
			pinMode(d[i][j], OUTPUT);
		}
	}
}

// the loop function runs over and over again until power down or reset
void loop() {
	x = analogRead(7) / 10;
	if (x / 10 < 10) {
		for (int i = 0; i < 7; i++) {
			if (values[x / 10][i] == 0)
				digitalWrite(d[0][i], LOW);
			else
				digitalWrite(d[0][i], HIGH);
		}
		for (int i = 0; i < 7; i++) {
			if (values[x % 10][i] == 0)
				digitalWrite(d[1][i], LOW);
			else
				digitalWrite(d[1][i], HIGH);
		}
	} else {
		for (int i = 0; i < 7; i++) {
			digitalWrite(d[0][i], HIGH);
		}
		for (int i = 0; i < 7; i++) {
			if (values[10][i] == 0)
				digitalWrite(d[1][i], LOW);
			else
				digitalWrite(d[1][i], HIGH);
		}
	}
	delay(100);
}
