#pragma once

#include <filesystem>
#include <fstream>
#include <string>
#include <memory>
#include <iostream>
#include <vector>

#define CAFF_EXTENSION ".caff"
#define HEADER_SIZE 20
#define CAFF_MAGIC {'C', 'A', 'F', 'F'}
#define CAFF_MAGIC_SIZE 4
#define CIFF_MAGIC {'C', 'I', 'F', 'F'}
#define CIFF_MAGIC_SIZE 4
#define MAX_VALID_YR 9999
#define MIN_VALID_YR 1970

typedef enum {
    CAFF_HEADER = 1,
    CAFF_CREDITS = 2,
    CAFF_ANIMATION = 3
} BlockName;

typedef struct {
    unsigned char magic[4];
    unsigned char header_size[8];
    unsigned char content_size[8];
    unsigned char width[8];
    unsigned char height[8];
    unsigned __int64 _caption_len;
    unsigned char* caption;
    unsigned __int64 _tags_len;
    unsigned char* tags;
} CiffHeader;

typedef struct {
    CiffHeader header;
    unsigned char* pixels;
} Ciff;

typedef struct {
    unsigned char duration[8]; //ms
    Ciff ciff;
} CaffAnimation;

typedef struct {
    unsigned char year[2];
    unsigned char month;
    unsigned char day;
    unsigned char hour;
    unsigned char minute;
    unsigned char creator_len[8];
    unsigned char* creator;
} CaffCredits;

typedef struct {
    unsigned char magic[4];
    unsigned char header_size[8];
    unsigned char num_anim[8];
} CaffHeader;

typedef struct {
    unsigned char id;
    unsigned char length[8];
} BlockHeader;