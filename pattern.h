#ifndef __PATTERN_H__
#define __PATTERN_H__

// Includes
//---------------------------------
#include <stdint.h>        // Use uint_t
#include <avr/pgmspace.h>  // Store patterns in program memory

// Pattern that LED cube will display
//--------------------------------- 
const PROGMEM uint16_t pattern_table[][5] = {
//   Plane1  Plane2  Plane3  Plane4  Time[ms]

    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x7FFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x3FFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x1FFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0FFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0EFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0CFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x08FF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x00FF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x007F, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x003F, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x001F, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x000F, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x000E, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x000C, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0008, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0008, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x000C, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x000E, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x000F, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x008F, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x00CF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x00EF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x00FF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x08FF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0CFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0EFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x0FFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x1FFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x3FFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0x7FFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
};
#endif
