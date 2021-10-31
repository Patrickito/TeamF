#include "parser.h"

Parser::~Parser() {}

unsigned __int64 Parser::getFileSize(const std::string filename)
{
    std::ifstream in_file(filename, std::ios::binary);
    in_file.seekg(0, std::ios::end);
    auto returnValue = in_file.tellg();
    return returnValue;
}

void Parser::openFile(const std::string filename, FILE*& ptr)
{
    errno_t err = fopen_s(&ptr, filename.c_str(), "rb");
    if (err != 0)
    {
        throw Unable_To_Open_Exception();
    }
}

std::shared_ptr<BlockHeader> Parser::processBlockHeader(FILE*& ptr, const unsigned __int64 fileSize)
{
    std::shared_ptr<BlockHeader> block_header(new BlockHeader);

    if ((__int64)(fileSize - ftell(ptr) - sizeof(block_header->id)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&block_header->id, sizeof(block_header->id), 1, ptr);
    if (block_header->id != CAFF_HEADER &&
        block_header->id != CAFF_CREDITS &&
        block_header->id != CAFF_ANIMATION)
        throw Invalid_Block_Id_Exception();

    if ((__int64)(fileSize - ftell(ptr) - sizeof(block_header->length)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&block_header->length, sizeof(block_header->length), 1, ptr);
    __int64 length = ContvertToIntegerFrom8Byte(block_header->length);

    if ((__int64)(fileSize - ftell(ptr) - length) < 0)
        throw Invalid_Caff_File_Size_Exception();

    return block_header;
}

std::shared_ptr<CaffHeader> Parser::processCaffHeader(FILE*& ptr, const unsigned __int64 fileSize, const std::shared_ptr<BlockHeader> bh)
{
    std::shared_ptr<CaffHeader> header(new CaffHeader);
    auto length = ContvertToIntegerFrom8Byte(bh->length);

    unsigned char caff_magic[] = CAFF_MAGIC;
    unsigned __int64 default_header_size = HEADER_SIZE;

    if ((__int64)(fileSize - ftell(ptr) - sizeof(header->magic)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&header->magic, sizeof(header->magic), 1, ptr);
    if (!EqualArrays(header->magic, caff_magic, CAFF_MAGIC_SIZE))
        throw Invalid_Caff_Magic_Exception();
        
    if ((__int64)(fileSize - ftell(ptr) - sizeof(header->header_size)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&header->header_size, sizeof(header->header_size), 1, ptr);
    unsigned __int64 header_size = ContvertToIntegerFrom8Byte(header->header_size);
    if (header_size != length ||
        header_size != default_header_size)
        throw Invalid_Header_Size_Exception();

    fread(&header->num_anim, sizeof(header->num_anim), 1, ptr);

    return header;
}

std::shared_ptr<CaffCredits> Parser::processCaffCredits(FILE*& ptr, const unsigned __int64 fileSize, const std::shared_ptr<BlockHeader> bh)
{
    std::shared_ptr<CaffCredits> credits(new CaffCredits);
    __int64 length = ContvertToIntegerFrom8Byte(bh->length);

    if ((__int64)(fileSize - ftell(ptr) - sizeof(credits->year)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&credits->year, sizeof(credits->year), 1, ptr);
    auto year = ContvertToIntegerFrom2Byte(credits->year);
    if (year < MIN_VALID_YR || year > MAX_VALID_YR)
        throw Invalid_Year_Exception();

    if ((__int64)(fileSize - ftell(ptr) - sizeof(credits->month)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&credits->month, sizeof(credits->month), 1, ptr);
    if (credits->month < 1 || credits->month > 12)
        throw Invalid_Month_Exception();

    if ((__int64)(fileSize - ftell(ptr) - sizeof(credits->day)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&credits->day, sizeof(credits->day), 1, ptr);
    if (credits->day < 1 || credits->day > 31)
        throw Invalid_Day_Exception();

    if (!isValidDate(year, credits->month, credits->day))
        throw Invalid_Date_Exception();

    if ((__int64)(fileSize - ftell(ptr) - sizeof(credits->hour)) < 0)
         throw Invalid_Caff_File_Size_Exception();
    fread(&credits->hour, sizeof(credits->hour), 1, ptr);
    if (credits->hour > 23)
        throw Invalid_Hour_Exception();

    if ((__int64)(fileSize - ftell(ptr) - sizeof(credits->minute)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&credits->minute, sizeof(credits->minute), 1, ptr);
    if (credits->minute > 59)
        throw Invalid_Min_Exception();

    if ((__int64)(fileSize - ftell(ptr) - sizeof(credits->creator_len)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&credits->creator_len, sizeof(credits->creator_len), 1, ptr);
    auto creator_len = ContvertToIntegerFrom8Byte(credits->creator_len);
    __int64 header_lenght =
        sizeof(credits->year) +
        sizeof(credits->month) +
        sizeof(credits->day) +
        sizeof(credits->hour) +
        sizeof(credits->minute) +
        sizeof(credits->creator_len) +
        creator_len;
    if (header_lenght != length) {
        throw Invalid_Header_Size_Exception();
    }

    credits->creator = new unsigned char[(int)creator_len];
    fread(credits->creator, (size_t)creator_len, 1, ptr);

    return credits;
}

std::shared_ptr<CaffAnimation> Parser::processCaffAnimation(FILE*& ptr, const unsigned __int64 fileSize, const std::shared_ptr<BlockHeader> bh)
{
    std::shared_ptr<CaffAnimation> anim(new CaffAnimation);
    __int64 length = ContvertToIntegerFrom8Byte(bh->length);

    unsigned char ciff_magic[] = CIFF_MAGIC;

    if ((__int64)(fileSize - ftell(ptr) - sizeof(anim->duration)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&anim->duration, sizeof(anim->duration), 1, ptr);

    auto ciffStartByte = (unsigned __int64)ftell(ptr);

    auto& header = anim->ciff.header;
    if ((__int64)(fileSize - ftell(ptr) - sizeof(header.magic)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&header.magic, sizeof(header.magic), 1, ptr);
    if (!EqualArrays(header.magic, ciff_magic, CIFF_MAGIC_SIZE))
        throw Invalid_Ciff_Magic_Exception();

    if ((__int64)(fileSize - ftell(ptr) - sizeof(header.header_size)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&header.header_size, sizeof(header.header_size), 1, ptr);
    auto header_size = ContvertToIntegerFrom8Byte(header.header_size);

    if ((__int64)(fileSize - ftell(ptr) - sizeof(header.content_size)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&header.content_size, sizeof(header.content_size), 1, ptr);
    auto content_size = ContvertToIntegerFrom8Byte(header.content_size);

    if ((__int64)(fileSize - ftell(ptr) - sizeof(header.width)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&header.width, sizeof(header.width), 1, ptr);

    if ((__int64)(fileSize - ftell(ptr) - sizeof(header.height)) < 0)
        throw Invalid_Caff_File_Size_Exception();
    fread(&header.height, sizeof(header.height), 1, ptr);

	unsigned __int64 startByte = (unsigned __int64)ftell(ptr);
    unsigned char temp = '\0';
    while (temp != '\n') {
        if ((unsigned __int64)ftell(ptr) - ciffStartByte >= header_size) {
            throw Invalid_Caption_Exception();
        }
        fread(&temp, 1, 1, ptr);
    }
	unsigned __int64 caption_size = (unsigned __int64)ftell(ptr) - startByte;
    header.caption = new unsigned char[(unsigned int)caption_size];
    fseek(ptr, (long)startByte, SEEK_SET);
    fread(header.caption, (unsigned int)caption_size, 1, ptr);

    header._caption_len = caption_size;

    auto tags_size = header_size - caption_size - 36;
    header.tags = new unsigned char[(unsigned int)tags_size];
    fread(header.tags, (unsigned int)tags_size, 1, ptr);

    header._tags_len = tags_size;

    if (tags_size > 0)
        if(header.tags[tags_size - 1] != '\0')
            throw Invalid_Tags_Exception();

    __int64 header_lenght =
        sizeof(anim->duration) +
        sizeof(header.magic) +
        sizeof(header.header_size) +
        sizeof(header.content_size) +
        sizeof(header.width) +
        sizeof(header.height) +
        header._caption_len +
        header._tags_len +
        content_size;
    if (header_lenght != length) {
        throw Invalid_Header_Size_Exception();
    }

    auto& pixels = anim->ciff.pixels;
    pixels = new unsigned char[(unsigned int)content_size];
    fread(pixels, (unsigned int)content_size, 1, ptr);

    return anim;
}

Caff Parser::parseCaff(std::string filename)
{
    std::string extension = std::filesystem::path(filename).extension().string();
    if (extension.compare(CAFF_EXTENSION) != 0) {
        throw Invalid_Extension_Exception();
    }
    auto file_size = getFileSize(filename);
    FILE* ptr = NULL;
    openFile(filename, ptr);
    std::shared_ptr<CaffNative> caff_n(new CaffNative());
    caff_n->setFilePointer(ptr);
    bool animDone = false;
    while (caff_n->getHeader() == NULL || caff_n->getCredits() == NULL || !animDone) {
        auto block_header = processBlockHeader(ptr, file_size);
		switch (block_header->id)
		{
		case CAFF_HEADER:
		{
			if (caff_n->getHeader() == NULL) {
				auto header = processCaffHeader(ptr, file_size, block_header);
				caff_n->setHeader(header);
				caff_n->initAnimations();
			}
			else
				throw Multiple_Header_Block_Exception();
			break;
		}
		case CAFF_CREDITS:
		{
			if (caff_n->getCredits() == NULL) {
				auto credits = processCaffCredits(ptr, file_size, block_header);
				caff_n->setCredits(credits);
			}
			else
				throw Multiple_Credits_Block_Exception();
			break;
		}
		case CAFF_ANIMATION:
		{
			if (caff_n->getAnimationList() != NULL) {
				auto anim = processCaffAnimation(ptr, file_size, block_header);
				caff_n->addAnimation(anim);
				if (caff_n->getAnimationNum() == caff_n->getAnimationCnt()) {
					animDone = true;
				}
			}
			else
				throw Invalid_Block_Order_Exception();

			break;
		}
		default:
			break;
		}
    }
    if ((__int64)(file_size - ftell(ptr)) > 0)
        throw Invalid_Data_Size_Exception();
    return Caff(caff_n);
}
