#include "caff.h"

Caff::Caff(std::shared_ptr<CaffNative> caff)
{
	this->caff = caff;
}

Caff::~Caff() {}

unsigned __int64 Caff::getAnimationNumber()
{
	return ContvertToIntegerFrom8Byte(caff->getHeader()->num_anim);
}

unsigned __int16 Caff::getYear()
{
	return ContvertToIntegerFrom2Byte(caff->getCredits()->year);
}

unsigned __int8 Caff::getMonth()
{
	return caff->getCredits()->month;
}

unsigned __int8 Caff::getDay()
{
	return caff->getCredits()->day;
}

unsigned __int8 Caff::getHour()
{
	return caff->getCredits()->hour;
}

unsigned __int8 Caff::getMinute()
{
	return caff->getCredits()->minute;
}

std::vector<char> Caff::getCreator()
{
	return ConvertToString(caff->getCredits()->creator, ContvertToIntegerFrom8Byte(caff->getCredits()->creator_len));
}

Caff::Ciff Caff::getCaffAnimation(unsigned int ix)
{
	return Caff::Ciff(std::shared_ptr<CaffAnimation>(caff->getAnimation(ix)));
}

unsigned __int64 Caff::Ciff::getWidth()
{
	return ContvertToIntegerFrom8Byte(anim->ciff.header.width);
}

unsigned __int64 Caff::Ciff::getHeight()
{
	return ContvertToIntegerFrom8Byte(anim->ciff.header.height);
}

std::vector<char> Caff::Ciff::getCaption()
{
	return ConvertToString(anim->ciff.header.caption, anim->ciff.header._caption_len - 1);
}

std::vector<char> Caff::Ciff::getTags()
{
	return ConvertToString(anim->ciff.header.tags, anim->ciff.header._tags_len);
}

unsigned __int64 Caff::Ciff::getDuration()
{
	return ContvertToIntegerFrom8Byte(anim->duration);
}

Caff::Pixel Caff::Ciff::getPixelAt(const unsigned __int64 col, const unsigned __int64 row)
{
	unsigned __int64 w = getWidth();
	unsigned __int64 h = getHeight();
	if (col < w && row < h && (col * row * 3) < ContvertToIntegerFrom8Byte(anim->ciff.header.content_size)) {
		unsigned char r = anim->ciff.pixels[(row * w + col) * 3];
		unsigned char g = anim->ciff.pixels[(row * w + col) * 3 + 1];
		unsigned char b = anim->ciff.pixels[(row * w + col) * 3 + 2];
		return {r,g,b};
	}
	return {0,0,0};
}

Caff::Ciff::Ciff(std::shared_ptr<CaffAnimation> anim)
{
	this->anim = anim;
}


