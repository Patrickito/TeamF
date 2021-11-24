using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace TeamF_Api
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Parser parser = new Parser();
				for (int a = 0; a < 10; a++)
				{
					var sw = new Stopwatch();
					sw.Start();
					Caff caff;
					string path = "../../../../../CAFF_Parser/testFiles/1.caff";
					using (caff = parser.parseCaff(path))
					{
						int cnt = 0;
						for (uint k = 0; k < caff.getAnimationNumber(); k++)
						{
							var item = caff.getCaffAnimation(k);
							string caption = string.Join("", item.getCaption());
							string[] tags = string.Join("", item.getTags()).Split('\0')[..^1];



							var Width = item.getWidth();
							var Height = item.getHeight();
							Bitmap bitmap = new Bitmap((int)Width, (int)Height);
							for (ulong i = 0; i < Width; i++)
							{
								for (ulong j = 0; j < Height; j++)
								{
									using (var p = item.getPixelAt(i, j))
									{
										Color color = Color.FromArgb(p.r, p.g, p.b);
										bitmap.SetPixel((int)i, (int)j, color);
									}
								}
							}
							bitmap.Save("_caffTest" + cnt + ".jpg", ImageFormat.Jpeg);
							cnt++;
						}

						sw.Stop();
						TimeSpan timeTaken = sw.Elapsed;
						string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");
						Console.WriteLine(foo);
					}
					caff.Dispose();
				}
				parser.Dispose();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}

			Console.ReadKey();
		}
	}
}

