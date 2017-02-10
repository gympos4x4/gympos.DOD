/*
 Name:		Gyro.ino
 Created:	2/9/2017 8:20:11 PM
 Author:	Juraj
*/

#include "Wire.h"

#define MPU_addr 0x68  // I2C address of the MPU-6050

int16_t AcX, AcY, AcZ, Tmp, GyX, GyY, GyZ;
int rotX, rotY, rotZ;
void setup() {
	Wire.begin();
	Wire.beginTransmission(MPU_addr);
	Wire.write(0x6B);  // PWR_MGMT_1 register
	Wire.write(0);     // set to zero (wakes up the MPU-6050)
	Wire.endTransmission(true);
	Serial.begin(9600);
}
void loop() {
	Wire.beginTransmission(MPU_addr);
	Wire.write(0x3B);  // starting with register 0x43 (GYRO_XOUT_H)
	Wire.endTransmission(false);
	Wire.requestFrom(MPU_addr, 6, true);  // request a total of 14 registers
	AcX = Wire.read() << 8 | Wire.read();  // 0x3B (ACCEL_XOUT_H) & 0x3C (ACCEL_XOUT_L)
	AcY = Wire.read() << 8 | Wire.read();  // 0x3D (ACCEL_YOUT_H) & 0x3E (ACCEL_YOUT_L)
	AcZ = Wire.read() << 8 | Wire.read();  // 0x3F (ACCEL_ZOUT_H) & 0x40 (ACCEL_ZOUT_L)
	rotX = getAngle(AcX, AcZ);
	rotY = getAngle(AcY, AcX);
	rotZ = getAngle(AcY, AcZ);
	Serial.print(rotX);
	Serial.print("|");
	Serial.print(rotY);
	Serial.print("|");
	Serial.println(rotZ);
	delay(100);
}

int getAngle(int x, int z) {
	return degrees(atan2(z, x));
}