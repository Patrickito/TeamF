#pragma once

#include "constans.h"

unsigned __int64 ContvertToIntegerFrom8Byte(const unsigned char a[8]);
unsigned __int16 ContvertToIntegerFrom2Byte(const unsigned char a[2]);
bool EqualArrays(const unsigned char a[], const unsigned char b[], int n);
bool isLeap(const __int16 year);
bool isValidDate(const __int16 y, const char m, const char d);
std::ifstream::pos_type fileSize(const char* filename);
const std::vector<char> ConvertToString(const unsigned char* a, const __int64 size);