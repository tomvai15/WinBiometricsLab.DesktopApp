//using System.Runtime.InteropServices;
//using System.Security.Principal;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using WinBiometricDotNet;
//using WinBiometricDotNet.Interop;

//namespace Biometrics;

//public class BiometricIdentityConverter : JsonConverter
//{
//    public override bool CanConvert(Type objectType)
//    {
//        return objectType == typeof(BiometricIdentity);
//    }

//    // Keep the current WriteJson functionality
//    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//    {
//        var biometricIdentity = (BiometricIdentity)value;

//        writer.WriteStartObject();
//        writer.WritePropertyName("Type");
//        writer.WriteValue(biometricIdentity.Type.ToString());

//        // Serialize Sid if the type is Sid
//        if (biometricIdentity.Type == IdentityType.Sid && biometricIdentity.Sid != null)
//        {
//            writer.WritePropertyName("Sid");
//            writer.WriteValue(biometricIdentity.Sid.Value); // Serialize Sid as string
//        }
//        // Serialize TemplateGuid if the type is Guid
//        else if (biometricIdentity.Type == IdentityType.Guid)
//        {
//            writer.WritePropertyName("TemplateGuid");
//            writer.WriteValue(biometricIdentity.TemplateGuid.ToString()); // Serialize Guid as string
//        }

//        writer.WriteEndObject();
//    }

//    // Updated ReadJson method to handle serialization safely
//    public override object ReadJson(
//        JsonReader reader,
//        Type objectType,
//        object existingValue,
//        JsonSerializer serializer
//    )
//    {
//        unsafe
//        {
//            var jsonObject = JObject.Load(reader);
//            var identityType = (IdentityType)
//                Enum.Parse(typeof(IdentityType), jsonObject["Type"].ToString());

//            // Create a new WINBIO_IDENTITY structure
//            SafeNativeMethods.WINBIO_IDENTITY identity = new SafeNativeMethods.WINBIO_IDENTITY();

//            switch (identityType)
//            {
//                case IdentityType.Guid:
//                    var guid = Guid.Parse(jsonObject["TemplateGuid"].ToString());
//                    var guidBytes = guid.ToByteArray();

//                    // Assign values to the WINBIO_IDENTITY.TemplateGuid
//                    identity.Value.TemplateGuid.Data1 = BitConverter.ToUInt32(guidBytes, 0);
//                    identity.Value.TemplateGuid.Data2 = BitConverter.ToUInt16(guidBytes, 4);
//                    identity.Value.TemplateGuid.Data3 = BitConverter.ToUInt16(guidBytes, 6);

//                    for (int i = 0; i < 8; i++)
//                    {
//                        identity.Value.TemplateGuid.Data4[i] = guidBytes[8 + i];
//                    }
//                    break;

//                case IdentityType.Sid:
//                    var sidValue = jsonObject["Sid"].ToString();
//                    var sid = new SecurityIdentifier(sidValue);

//                    // Get the binary form of the SID
//                    var sidBinaryForm = new byte[sid.BinaryLength];
//                    sid.GetBinaryForm(sidBinaryForm, 0);

//                    // Set the size of the AccountSid and copy the binary SID data
//                    identity.Value.AccountSid.Size = (uint)sidBinaryForm.Length;

//                    // Ensure we copy the binary form to the AccountSid.Data
//                    Marshal.Copy(
//                        sidBinaryForm,
//                        0,
//                        (IntPtr)identity.Value.AccountSid.Data,
//                        sidBinaryForm.Length
//                    );
//                    break;
//            }

//            // Return a BiometricIdentity using the internal constructor


//            var biometric = new BiometricIdentity(&identity);
//            biometric.Sid = new SecurityIdentifier(jsonObject["Sid"].ToString());
//            biometric.Type = identityType;

//            return biometric;
//        }
//    }
//}
