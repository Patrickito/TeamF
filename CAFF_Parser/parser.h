#pragma once


#include "caff.h"

class Parser {
	void openFile(const std::string filename, FILE*& ptr);
	unsigned __int64 getFileSize(std::string filename);
	std::shared_ptr<BlockHeader> processBlockHeader(FILE*& ptr, const unsigned __int64 fileSize);
	std::shared_ptr<CaffHeader> processCaffHeader(FILE*& ptr, const unsigned __int64 fileSize, const std::shared_ptr<BlockHeader> bh);
	std::shared_ptr<CaffCredits> processCaffCredits(FILE*& ptr, const unsigned __int64 fileSize, const std::shared_ptr<BlockHeader> bh);
	std::shared_ptr<CaffAnimation> processCaffAnimation(FILE*& ptr, const unsigned __int64 fileSize, const std::shared_ptr<BlockHeader> bh);
public:
	virtual Caff parseCaff(const std::string filename);
	virtual ~Parser();
};