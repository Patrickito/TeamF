%insert(runtime) %{
typedef void (SWIGSTDCALL* CSharpExceptionCallback_t)(const char *);
%}

%define CustomExceptionCallback(name)
%insert(runtime) %{
CSharpExceptionCallback_t name ## Callback = NULL;

extern "C" SWIGEXPORT
void SWIGSTDCALL name ## RegisterCallback(CSharpExceptionCallback_t customCallback) {
  name ## Callback = customCallback;
}

static void SWIG_CSharpSetPending_ ## name ## (const char *msg) {
  name ## Callback(msg);
}
%}
%enddef

%define CustomExceptionHelper(name)
%pragma(csharp) imclasscode=%{

class name ## Helper {
    public delegate void CustomExceptionDelegate(string message);
    static CustomExceptionDelegate customDelegate = new CustomExceptionDelegate(SetPendingCustomException);

    [global::System.Runtime.InteropServices.DllImport("$dllimport", EntryPoint="name" + "RegisterCallback")]
    public static extern void name ## RegisterCallback(CustomExceptionDelegate customCallback);

    static void SetPendingCustomException(string message) {
        SWIGPendingException.Set(new name(message));
    }

    static name ## Helper() {
        name ## RegisterCallback(customDelegate);
    }
}
static name ## Helper _ ## name ## Helper = new name ## Helper();
%}
%enddef

CustomExceptionCallback(Invalid_Extension_Exception);
CustomExceptionCallback(Unable_To_Open_Exception);
CustomExceptionCallback(Multiple_Header_Block_Exception);
CustomExceptionCallback(Multiple_Credits_Block_Exception);
CustomExceptionCallback(Invalid_Caff_File_Size_Exception);
CustomExceptionCallback(Invalid_Data_Size_Exception);
CustomExceptionCallback(Invalid_Caff_Magic_Exception);
CustomExceptionCallback(Invalid_Ciff_Magic_Exception);
CustomExceptionCallback(Invalid_Block_Id_Exception);
CustomExceptionCallback(Invalid_Block_Order_Exception);
CustomExceptionCallback(Invalid_Header_Size_Exception);
CustomExceptionCallback(Invalid_Caption_Exception);
CustomExceptionCallback(Invalid_Tags_Exception);
CustomExceptionCallback(Invalid_Year_Exception);
CustomExceptionCallback(Invalid_Month_Exception);
CustomExceptionCallback(Invalid_Day_Exception);
CustomExceptionCallback(Invalid_Hour_Exception);
CustomExceptionCallback(Invalid_Min_Exception);
CustomExceptionCallback(Invalid_Date_Exception);

CustomExceptionHelper(Invalid_Extension_Exception);
CustomExceptionHelper(Unable_To_Open_Exception);
CustomExceptionHelper(Multiple_Header_Block_Exception);
CustomExceptionHelper(Multiple_Credits_Block_Exception);
CustomExceptionHelper(Invalid_Caff_File_Size_Exception);
CustomExceptionHelper(Invalid_Data_Size_Exception);
CustomExceptionHelper(Invalid_Caff_Magic_Exception);
CustomExceptionHelper(Invalid_Ciff_Magic_Exception);
CustomExceptionHelper(Invalid_Block_Id_Exception);
CustomExceptionHelper(Invalid_Block_Order_Exception);
CustomExceptionHelper(Invalid_Header_Size_Exception);
CustomExceptionHelper(Invalid_Caption_Exception);
CustomExceptionHelper(Invalid_Tags_Exception);
CustomExceptionHelper(Invalid_Year_Exception);
CustomExceptionHelper(Invalid_Month_Exception);
CustomExceptionHelper(Invalid_Day_Exception);
CustomExceptionHelper(Invalid_Hour_Exception);
CustomExceptionHelper(Invalid_Min_Exception);
CustomExceptionHelper(Invalid_Date_Exception);