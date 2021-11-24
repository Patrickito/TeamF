#include <exception>

struct Invalid_Extension_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid file extenison.";
    }
};

struct Unable_To_Open_Exception : public std::exception {
    const char* what() const throw () {
        return "Unable to open CAFF file.";
    }
};

struct Unable_To_Close_Exception : public std::exception {
    const char* what() const throw () {
        return "Unable to close CAFF file.";
    }
};

struct Multiple_Header_Block_Exception : public std::exception {
    const char* what() const throw () {
        return "Multiple CAFF header block.";
    }
};

struct Multiple_Credits_Block_Exception : public std::exception {
    const char* what() const throw () {
        return "Multiple CAFF credits block.";
    }
};

struct Invalid_Caff_File_Size_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid Caff file size.";
    }
};

struct Invalid_Data_Size_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid data size.";
    }
};

struct Invalid_Caff_Magic_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid CAFF magic.";
    }
};

struct Invalid_Ciff_Magic_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid CIFF magic.";
    }
};

struct Invalid_Block_Id_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid block ID.";
    }
};

struct Invalid_Block_Order_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid block order.";
    }
};

struct Invalid_Header_Size_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid Header size.";
    }
};

struct Invalid_Caption_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid caption, no end character.";
    }
};

struct Invalid_Tags_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid tags, invalid end character.";
    }
};

struct Invalid_Year_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid year value.";
    }
};

struct Invalid_Month_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid month value.";
    }
};

struct Invalid_Day_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid day value.";
    }
};

struct Invalid_Hour_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid hour value.";
    }
};

struct Invalid_Min_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid minute value.";
    }
};

struct Invalid_Date_Exception : public std::exception {
    const char* what() const throw () {
        return "Invalid date.";
    }
};