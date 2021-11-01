using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamF_Api;
using static TeamF_Api.Caff;

namespace Parser_Tests
{
    [TestClass]
    public class Function_Unit_Tests
    {
        Parser parser = new Parser();
        string pathBase = "../../../../../CAFF_Parser/testFiles/";

        [TestMethod]
        public void Caff_getAnimationNumber()
        {
            ulong expected = 2;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var actual = caff.getAnimationNumber();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Caff_getYear()
        {
            ushort expected = 2020;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var actual = caff.getYear();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Caff_getMonth()
        {
            byte expected = 7;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var actual = caff.getMonth();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Caff_getDay()
        {
            byte expected = 2;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var actual = caff.getDay();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Caff_getHour()
        {
            byte expected = 14;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var actual = caff.getHour();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Caff_getMinute()
        {
            byte expected = 50;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var actual = caff.getMinute();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Caff_getCreator()
        {
            string expected = "Test Creator";
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var actual = string.Join("", caff.getCreator());
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Ciff_getWidth()
        {
            ulong expected = 1000;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var ciff = caff.getCaffAnimation(0);
                var actual = ciff.getWidth();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Ciff_getHeight()
        {
            ulong expected = 667;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var ciff = caff.getCaffAnimation(0);
                var actual = ciff.getHeight();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Ciff_getCaption()
        {
            string expected = "Beautiful scenery";
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var ciff = caff.getCaffAnimation(0);
                var actual = string.Join("", ciff.getCaption());
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Ciff_getTags()
        {
            string[] expected = { "landscape", "sunset", "mountains" };
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var ciff = caff.getCaffAnimation(0);
                var actual = string.Join("", ciff.getTags()).Split('\0')[..^1];
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Ciff_getDuration()
        {
            ulong expected = 1000;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var ciff = caff.getCaffAnimation(0);
                var actual = ciff.getDuration();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Ciff_getPixelAt()
        {
            Pixel expected = new Pixel();
            expected.r = 45;
            expected.g = 52;
            expected.b = 60;
            string filepath = pathBase + "1.caff";
            using (var caff = parser.parseCaff(filepath))
            {
                var ciff = caff.getCaffAnimation(0);
                var actual = ciff.getPixelAt(0, 0);
                Assert.AreEqual(expected.r, actual.r);
                Assert.AreEqual(expected.g, actual.g);
                Assert.AreEqual(expected.b, actual.b);
            }
        }
    }

    [TestClass]
    public class Exception_Unit_Tests
    {
        Parser parser = new Parser();
        string pathBase = "../../../../../CAFF_Parser/testFiles/";

        [TestMethod]
        public void Test_Invalid_Extension_Exception()
        {
            string filepath = pathBase + "dummy.cafff";
            Assert.ThrowsException<Invalid_Extension_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Test_Unable_To_Open_Exception()
        {
            string filepath = pathBase + "dummy.caff";
            Assert.ThrowsException<Unable_To_Open_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Test_Multiple_Header_Block_Exception()
        {
            string filepath = pathBase + "Multiple_Header_Block_Exception.caff";
            Assert.ThrowsException<Multiple_Header_Block_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Multiple_Credits_Block_Exception()
        {
            string filepath = pathBase + "Multiple_Credits_Block_Exception.caff";
            Assert.ThrowsException<Multiple_Credits_Block_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_bh_id()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception1.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_bh_lenght()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception2.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_bh_out_of_lenght()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception3.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_ch_magic()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception4.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_ch_header_size()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception5.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_cc_year()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception6.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_cc_month()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception7.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_cc_day()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception8.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_cc_hour()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception9.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_cc_minute()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception10.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_cc_creator_len()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception11.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_ca_duration()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception12.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_ca_magic()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception13.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_ca_header_size()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception14.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_ca_content_size()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception15.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_ca_width()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception16.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_File_Size_Exception_ca_height()
        {
            string filepath = pathBase + "Invalid_Caff_File_Size_Exception17.caff";
            Assert.ThrowsException<Invalid_Caff_File_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Data_Size_Exception()
        {
            string filepath = pathBase + "Invalid_Data_Size_Exception.caff";
            Assert.ThrowsException<Invalid_Data_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caff_Magic_Exception()
        {
            string filepath = pathBase + "Invalid_Caff_Magic_Exception.caff";
            Assert.ThrowsException<Invalid_Caff_Magic_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Ciff_Magic_Exception()
        {
            string filepath = pathBase + "Invalid_Ciff_Magic_Exception.caff";
            Assert.ThrowsException<Invalid_Ciff_Magic_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Block_Id_Exception()
        {
            string filepath = pathBase + "Invalid_Block_Id_Exception.caff";
            Assert.ThrowsException<Invalid_Block_Id_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Block_Order_Exception()
        {
            string filepath = pathBase + "Invalid_Block_Order_Exception.caff";
            Assert.ThrowsException<Invalid_Block_Order_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Header_Size_Exception_ch_not_match()
        {
            string filepath = pathBase + "Invalid_Header_Size_Exception2.caff";
            Assert.ThrowsException<Invalid_Header_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Header_Size_Exception_ch_not_default()
        {
            string filepath = pathBase + "Invalid_Header_Size_Exception2.caff";
            Assert.ThrowsException<Invalid_Header_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Header_Size_Exception_cc_not_match()
        {
            string filepath = pathBase + "Invalid_Header_Size_Exception3.caff";
            Assert.ThrowsException<Invalid_Header_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Header_Size_Exception_ca_not_match()
        {
            string filepath = pathBase + "Invalid_Header_Size_Exception4.caff";
            Assert.ThrowsException<Invalid_Header_Size_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Caption_Exception()
        {
            string filepath = pathBase + "Invalid_Caption_Exception.caff";
            Assert.ThrowsException<Invalid_Caption_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Tags_Exception()
        {
            string filepath = pathBase + "Invalid_Tags_Exception.caff";
            Assert.ThrowsException<Invalid_Tags_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Year_Exception_under_min()
        {
            string filepath = pathBase + "Invalid_Year_Exception1.caff";
            Assert.ThrowsException<Invalid_Year_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Year_Exception_above_max()
        {
            string filepath = pathBase + "Invalid_Year_Exception2.caff";
            Assert.ThrowsException<Invalid_Year_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Month_Exception_under_min()
        {
            string filepath = pathBase + "Invalid_Month_Exception1.caff";
            Assert.ThrowsException<Invalid_Month_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Month_Exception_above_max()
        {
            string filepath = pathBase + "Invalid_Month_Exception2.caff";
            Assert.ThrowsException<Invalid_Month_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Day_Exception_under_min()
        {
            string filepath = pathBase + "Invalid_Day_Exception1.caff";
            Assert.ThrowsException<Invalid_Day_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Day_Exception_above_max()
        {
            string filepath = pathBase + "Invalid_Day_Exception2.caff";
            Assert.ThrowsException<Invalid_Day_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Hour_Exception()
        {
            string filepath = pathBase + "Invalid_Hour_Exception.caff";
            Assert.ThrowsException<Invalid_Hour_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Test_Invalid_Min_Exception()
        {
            string filepath = pathBase + "Invalid_Min_Exception.caff";
            Assert.ThrowsException<Invalid_Min_Exception>(() => parser.parseCaff(filepath));
        }

        [TestMethod]
        public void Invalid_Date_Exception()
        {
            string filepath = pathBase + "Invalid_Date_Exception.caff";
            Assert.ThrowsException<Invalid_Date_Exception>(() => parser.parseCaff(filepath));
        }
    }
}

