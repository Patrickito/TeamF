#pragma once

#include "caff_native.h"

class Caff {
	std::shared_ptr<CaffNative> caff;
public:
	typedef struct {
		unsigned char r;
		unsigned char g;
		unsigned char b;
	} Pixel;
	class Ciff {
		std::shared_ptr<CaffAnimation> anim;
	public:
		Ciff(std::shared_ptr<CaffAnimation> anim);
		virtual unsigned __int64 getWidth();
		virtual unsigned __int64 getHeight();
		virtual std::vector<char> getCaption();
		virtual std::vector<char> getTags();
		virtual unsigned __int64 getDuration();
		virtual Pixel getPixelAt(const unsigned __int64 col, const unsigned __int64 row);
	};

	Caff(std::shared_ptr<CaffNative> caff);
	virtual ~Caff();
	virtual unsigned __int64 getAnimationNumber();
	virtual unsigned __int16 getYear();
	virtual unsigned __int8 getMonth();
	virtual unsigned __int8 getDay();
	virtual unsigned __int8 getHour();
	virtual unsigned __int8 getMinute();
	virtual std::vector<char> getCreator();
	virtual Caff::Ciff getCaffAnimation(const unsigned int ix);
};