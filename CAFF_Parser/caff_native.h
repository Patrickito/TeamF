#pragma once

#include "utility.h"
#include "exceptions.h"

class CaffNative {
    std::string fileName;
    FILE* filePtr;
    std::shared_ptr<CaffHeader> header;
    std::shared_ptr<CaffCredits> credits;
    std::shared_ptr<std::vector<std::shared_ptr<CaffAnimation>>> animations;
public:
    CaffNative();
    ~CaffNative();
    FILE* getFilePointer();
    void setFilePointer(FILE* ptr);
    std::shared_ptr<CaffHeader> getHeader();
    void setHeader(std::shared_ptr<CaffHeader> header);
    std::shared_ptr<CaffCredits> getCredits();
    void setCredits(std::shared_ptr<CaffCredits> credits);
    void initAnimations();
    unsigned __int64 getAnimationNum();
    unsigned __int64 getAnimationCnt();
    std::shared_ptr<std::vector<std::shared_ptr<CaffAnimation>>> getAnimationList();
    std::shared_ptr<CaffAnimation> getAnimation(const unsigned ix);
    void addAnimation(std::shared_ptr<CaffAnimation> animation);
};