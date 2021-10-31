#include "caff_native.h"

CaffNative::CaffNative() {
    fileName = "";
    filePtr = NULL;
}

CaffNative::~CaffNative()
{
    if (credits != NULL) {
        if (credits->creator != NULL) {
            delete[] credits->creator;
        }
    }
    if(animations != NULL){
        std::vector<std::shared_ptr<CaffAnimation>> v = *animations.get();
        for (auto& anim : v)
        {
            if (anim != NULL) {
                if (anim->ciff.header.caption != NULL) {
                    delete[]anim->ciff.header.caption;
                }
                if (anim->ciff.header.caption != NULL) {
                    delete[]anim->ciff.header.tags;
                }
                if (anim->ciff.header.caption != NULL) {
                    delete[]anim->ciff.pixels;
                }
            }
        }
    }
}

FILE* CaffNative::getFilePointer() {
    return filePtr;
}
void CaffNative::setFilePointer(FILE* ptr) {
    filePtr = ptr;
}
std::shared_ptr<CaffHeader> CaffNative::getHeader() {
    return header;
}
void CaffNative::setHeader(std::shared_ptr<CaffHeader> header) {
    this->header = header;
}
std::shared_ptr<CaffCredits> CaffNative::getCredits() {
    return credits;
}
void CaffNative::setCredits(std::shared_ptr<CaffCredits> credits) {
    this->credits = credits;
}
void CaffNative::initAnimations()
{
    animations = std::shared_ptr<std::vector<std::shared_ptr<CaffAnimation>>>(new std::vector<std::shared_ptr<CaffAnimation>>);
}
unsigned __int64 CaffNative::getAnimationNum() {
    if(header != NULL)
        return ContvertToIntegerFrom8Byte(header->num_anim);
    return 0;
}
std::shared_ptr<std::vector<std::shared_ptr<CaffAnimation>>> CaffNative::getAnimationList() {
    return animations;
}
std::shared_ptr<CaffAnimation> CaffNative::getAnimation(const unsigned ix)
{
    if (animations != NULL) {
        if (ix < getAnimationNum()) {
            if (getAnimationCnt() > 0) {
                return (*animations.get())[ix];
            }
        }
    }
    return NULL;
}
unsigned __int64 CaffNative::getAnimationCnt()
{
    if (animations != NULL) {
        return animations.get()->size();
    }
    return 0;
}
void CaffNative::addAnimation(std::shared_ptr<CaffAnimation> animation) {
    if (animations != NULL) {
        if (animations.get()->size() < (unsigned)getAnimationNum())
            animations.get()->push_back(animation);
    }
    
}