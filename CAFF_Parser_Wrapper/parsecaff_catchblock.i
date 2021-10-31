%exception Parser::parseCaff{
	try {
		$action
	} catch(Invalid_Extension_Exception &_e) {
        SWIG_CSharpSetPending_Invalid_Extension_Exception((&_e)->what());
        return $null;
	}catch(Unable_To_Open_Exception &_e) {
        SWIG_CSharpSetPending_Unable_To_Open_Exception((&_e)->what());
        return $null;
	} catch(Multiple_Header_Block_Exception &_e) {
		SWIG_CSharpSetPending_Multiple_Header_Block_Exception((&_e)->what());
        return $null;
	} catch(Multiple_Credits_Block_Exception &_e) {
		SWIG_CSharpSetPending_Multiple_Credits_Block_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Caff_File_Size_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Caff_File_Size_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Data_Size_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Data_Size_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Caff_Magic_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Caff_Magic_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Ciff_Magic_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Ciff_Magic_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Block_Id_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Block_Id_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Block_Order_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Block_Order_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Header_Size_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Header_Size_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Caption_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Caption_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Tags_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Tags_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Year_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Year_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Month_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Month_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Day_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Day_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Hour_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Hour_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Min_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Min_Exception((&_e)->what());
        return $null;
	} catch(Invalid_Date_Exception &_e) {
		SWIG_CSharpSetPending_Invalid_Date_Exception((&_e)->what());
        return $null;
	}
}