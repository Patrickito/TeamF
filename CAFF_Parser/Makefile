#
# Compiler flags
#
CL = cl.exe
LIB = lib.exe
CFLAGS = /external:W3 /Gd /TP /FC /errorReport:queue

#
# Project files
#
SRCS = utility.cpp caff_native.cpp caff.cpp parser.cpp
OBJS = $(SRCS:.cpp=.obj)
TRGT  = CAFF_Parser.lib
PDB  = CAFF_Parser.pdb

#
# Debug build settings
#
DBGDIR = Debug
DBGLIB = $(DBGDIR)/$(TRGT)
DBGPDB = $(DBGDIR)/$(PDB)
DBGOBJS = $(addprefix $(DBGDIR)/, $(OBJS))
DBGCFLAGS = /ZI /JMC /nologo /W3 /WX- /diagnostics:column /sdl /Od /D _DEBUG /D _CONSOLE /D _UNICODE /D UNICODE /Gm- /EHsc /RTC1 /MDd /GS /fp:precise /permissive- /Zc:wchar_t /Zc:forScope /Zc:inline /std:c++20
DBGLFLAGS = /NOLOGO /MACHINE:X64

#
# Release build settings
#
RELDIR = Release
RELLIB = $(RELDIR)/$(TRGT)
RELPDB = $(RELDIR)/$(PDB)
RELOBJS = $(addprefix $(RELDIR)/, $(OBJS))
RELCFLAGS = /Zi /JMC /nologo /W3 /WX- /diagnostics:column /sdl /O2 /Oi /GL /D NDEBUG /D _CONSOLE /D_UNICODE /D UNICODE /Gm- /EHsc /MT /GS /Gy /fp:precise /permissive- /Zc:wchar_t /Zc:forScope /Zc:inline /std:c++20
RELLFLAGS = /NOLOGO /MACHINE:X64 /LTCG

.PHONY: all clean debug dbg_prep release rel_prep

# Default build
all: release debug

#
# Debug rules
#
debug: dbg_prep $(DBGLIB)

$(DBGLIB): $(DBGOBJS)
	@$(LIB) /OUT:$(DBGLIB) $(DBGLFLAGS) $^

$(DBGDIR)/%.obj: %.cpp
	@$(CL) /c $(CFLAGS) $(DBGCFLAGS) /Fo$@ /Fd$(DBGPDB) $<

#
# Release rules
#
release: rel_prep $(RELLIB)

$(RELLIB): $(RELOBJS)
	@$(LIB) /OUT:$(RELLIB) $(RELLFLAGS)  $^

$(RELDIR)/%.obj: %.cpp
	@$(CL) /c $(CFLAGS) $(RELCFLAGS) /Fo$@ /Fd$(RELPDB) $<

#
# Other rules
#
dbg_prep:
	@if not exist $(DBGDIR) mkdir $(DBGDIR)

rel_prep:
	@if not exist $(RELDIR) mkdir $(RELDIR)

clean: dbg_clean rel_clean

dbg_clean:
	@if exist $(DBGDIR) RD /S /Q $(DBGDIR)

rel_clean:
	@if exist $(RELDIR) RD /S /Q $(RELDIR)