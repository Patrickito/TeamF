#include "utility.h"

unsigned __int64 ContvertToIntegerFrom8Byte(const unsigned char a[8]) {
    return  (static_cast<unsigned __int64>(a[0])) |
        (static_cast<unsigned __int64>(a[1]) << 8) |
        (static_cast<unsigned __int64>(a[2]) << 16) |
        (static_cast<unsigned __int64>(a[3]) << 24) |
        (static_cast<unsigned __int64>(a[4]) << 32) |
        (static_cast<unsigned __int64>(a[5]) << 40) |
        (static_cast<unsigned __int64>(a[6]) << 48) |
        (static_cast<unsigned __int64>(a[7]) << 56);
}

unsigned __int16 ContvertToIntegerFrom2Byte(const unsigned char a[2]) {
    return  (static_cast<unsigned __int16>(a[0])) |
        (static_cast<unsigned __int16>(a[1]) << 8);
}

bool EqualArrays(const unsigned char a[], const unsigned char b[], const int n) {
    for (int i = 0; i < n; i++) {
        if (a[i] != b[i]) return false;
    }
    return true;
}

bool isLeap(const __int16 year)
{
    return (((year % 4 == 0) &&
        (year % 100 != 0)) ||
        (year % 400 == 0));
}

bool isValidDate(const __int16 y, const char m, const char d)
{
    if (y > MAX_VALID_YR ||
        y < MIN_VALID_YR)
        return false;
    if (m < 1 || m > 12)
        return false;
    if (d < 1 || d > 31)
        return false;

    if (m == 2)
    {
        if (((y % 4 == 0) && (y % 100 != 0)) || (y % 400 == 0))
            return (d <= 29);
        else
            return (d <= 28);
    }
    if (m == 4 || m == 6 ||
        m == 9 || m == 11)
        return (d <= 30);

    return true;
}

std::ifstream::pos_type fileSize(const char* filename)
{
    std::ifstream in(filename, std::ifstream::ate | std::ifstream::binary);
    return in.tellg();
}

const std::vector<char> ConvertToString(const unsigned char* a, const __int64 size)
{
    std::vector<char> s;
    for (int i = 0; i < size; i++) {
        s.push_back(a[i]);
    }
    return s;
}
