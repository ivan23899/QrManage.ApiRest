using QRCoder;
using System.Drawing;

namespace RR.QrManage.Framework.Codes
{
    public class Qr
    {
        public static string Generate(string key)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(key, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(40, Color.BlueViolet, Color.White, (Bitmap)Bitmap.FromFile("D:\\48. PROYECTOS RANDOM\\TestQR.WebAPI\\Resources\\facebook.png"), 15, 20, true, null);

            ImageConverter converter = new();
            var qrByte = (byte[])converter.ConvertTo(qrCodeImage, typeof(byte[]))!;
            var qrBase64 = Convert.ToBase64String(qrByte);
            return qrBase64;                                                                                                                                                                                                    
        }
    }
}
