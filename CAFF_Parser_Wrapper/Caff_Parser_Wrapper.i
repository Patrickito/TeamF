%module(directors="1", allprotected="1") Caff_Parser_Wrapper

%include exception.i
%include <arrays_csharp.i>
%include <std_array.i>
%include <std_string.i>
%include std_vector.i

%typemap(csclassmodifiers) SWIGTYPE "public partial class"
%template(CharVector) std::vector<char>;

%{
#include "parser.h"
#include "caff.h"
%}

%feature("director") Caff;
%feature("director") Parser;
%feature("director") Pixel;

%apply unsigned long long { unsigned __int64 }
%apply unsigned short { unsigned __int16 }
%apply unsigned char { unsigned __int8 }

%include parsecaff_catchblock.i

%include "..\CAFF_Parser\parser.h"
%include "..\CAFF_Parser\caff.h"

%include csharp_moduleclassmodifiers.i
%include custom_exception_helper.i